using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneController : MonoBehaviour
{
    private DroneAttackManager droneAttackManager;
    private MovingController mover;
    public Rotator rotator;
    private Character player;
    private PlayerHealthManager playerHealthManager;

    [SerializeField] private float speed;
    [SerializeField] private float minDistanceToPlayer;
    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] private float randomRadius;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float idleWaitTimeMin = 1f;
    [SerializeField] private float idleWaitTimeMax = 5f;

    private Vector2 moveDirection;
    private Vector2 randomPatrolTarget;
    private Coroutine currentCoroutine;
    private DroneState currentState;

    private void Awake()
    {
        player = FindAnyObjectByType<Character>();
        playerHealthManager = player.GetComponent<PlayerHealthManager>();
        droneAttackManager = GetComponent<DroneAttackManager>();
        rotator = GetComponent<Rotator>();
        mover = new MovingController(GetComponent<Rigidbody2D>(), speed);
    }

    private void Start()
    {
        SetState(DroneState.Patrol);
    }

    private void OnEnable()
    {
        playerHealthManager.onHealthChanged += ForceChaseState;
    }

    private void OnDisable()
    {
        playerHealthManager.onHealthChanged -= ForceChaseState;
    }

    private void Update()
    {
        UpdateStateTransitions();
        UpdateCurrentStateLogic();
        ApplyMovementAndRotation();
    }

    private void UpdateStateTransitions()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // If the drone is currently attacking, it should not change state
        if (droneAttackManager.isAttacking)
        {
            SetState(DroneState.Attack);
            return;
        }

        // If the drone was just attacking and has finished, go to Idle
        if (currentState == DroneState.Attack && !droneAttackManager.isAttacking)
        {
            SetState(DroneState.Idle);
            return;
        }

        // If the player is in the attack hitbox, and the drone is not on cooldown, attack
        if (droneAttackManager.hitbox.inHitBox && !droneAttackManager.onCooldown)
        {
            SetState(DroneState.Attack);
        }
        // If the player is far away, chase them
        else if (distanceToPlayer > maxDistanceToPlayer)
        {
            SetState(DroneState.Chase);
        }
        // If the player is too close, idle
        else if (distanceToPlayer < minDistanceToPlayer)
        {
            SetState(DroneState.Idle);
        }
        // If the drone isn't doing anything else, it should patrol
        else
        {
            if (currentState != DroneState.Patrol && currentState != DroneState.Idle)
            {
                SetState(DroneState.Patrol);
            }
        }
    }

    private void UpdateCurrentStateLogic()
    {
        switch (currentState)
        {
            case DroneState.Idle:
                moveDirection = Vector2.zero;
                break;

            case DroneState.Patrol:
                // Check if we reached the patrol target before setting a new one
                if (Vector2.Distance(transform.position, randomPatrolTarget) < 1f)
                {
                    SetState(DroneState.Idle);
                }
                else
                {
                    moveDirection = (randomPatrolTarget - (Vector2)transform.position).normalized;
                }
                break;

            case DroneState.Chase:
                moveDirection = (player.transform.position - transform.position).normalized;
                break;

            case DroneState.Attack:
                moveDirection = Vector2.zero;
                break;
        }
    }

    private void SetState(DroneState newState)
    {
        if (newState == currentState) return;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        currentState = newState;

        switch (currentState)
        {
            case DroneState.Idle:
  
                currentCoroutine = StartCoroutine(WaitAndPatrol());
                break;

            case DroneState.Patrol:
                SetNewPatrolTarget();
                break;

            case DroneState.Chase:
                break;

            case DroneState.Attack:
                break;
        }
    }

    private void ForceChaseState()
    {
        SetState(DroneState.Chase);
    }

    private void ApplyMovementAndRotation()
    {
        mover.SetMove(moveDirection);
        mover.Update();

        if (droneAttackManager.isAttacking)
        {
            rotator.SetRotate(droneAttackManager.direction, rotateSpeed);
        }
        else if (moveDirection != Vector2.zero)
        {
            rotator.SetRotate(moveDirection, rotateSpeed);
        }
    }

    private void SetNewPatrolTarget()
    {
        randomPatrolTarget = (Vector2)transform.position + Random.insideUnitCircle * randomRadius;
    }

    private IEnumerator WaitAndPatrol()
    {
        yield return new WaitForSeconds(Random.Range(idleWaitTimeMin, idleWaitTimeMax));
        if (currentState == DroneState.Idle)
        {
            SetState(DroneState.Patrol);
        }
    }
}