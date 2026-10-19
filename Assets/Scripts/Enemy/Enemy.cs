using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    public EnemyAttackManager AttackManager;

    private PositionObserv posObserver;
    private LayersManager layersManager;

    public MoveAnimate moveAnimator;
    public AttackAnimate attackAnimator;

    private EnemyState currentState;
    public EnemyWalkState walkState;
    public EnemyIdleState idleState;
    public EnemyAttackState attackState;

    public GameObject player;
    private MovingController mover;

    [SerializeField] private float speed = 2;
    [SerializeField] private float distatceToPlayer;
    [SerializeField] private float distatceToAttack;

    private Vector2 moveDirection;

    public void Awake()
    {
        moveAnimator = new MoveAnimate(GetComponent<Animator>(), "MoveX", "MoveY");
        attackAnimator = new AttackAnimate(GetComponent<Animator>(), "isAttack", "AttackX", "AttackY");

        AttackManager = GetComponent<EnemyAttackManager>();

        mover = new MovingController(GetComponent<Rigidbody2D>(), speed);
        posObserver = GetComponent<PositionObserv>();

        idleState = new EnemyIdleState(this);
        walkState = new EnemyWalkState(this);
        attackState = new EnemyAttackState(this);

        SetState(idleState);

    }
    private void Start()
    {
        player = FindAnyObjectByType<Character>().gameObject;
        layersManager = FindAnyObjectByType<LayersManager>();
        layersManager.RegisterSubject(transform, posObserver);

        AttackManager.onAttackPerform += attackAnimator.StartAttack;
        AttackManager.onAttackEnd += attackAnimator.EndAttack;
    }

    private void OnDisable()
    {
        AttackManager.onAttackPerform -= attackAnimator.StartAttack;
        AttackManager.onAttackEnd -= attackAnimator.EndAttack;
    }

    private void Update()
    {
        currentState?.Update();
    }

    public Vector2 GetMoveDirection() => moveDirection;

    public void MoveToPlayer()
    {
        moveDirection = (player.transform.position - transform.position).normalized;
        Move();
    }
    public void StopMove()
    {
        moveDirection = Vector2.zero;
        moveAnimator.AnimateDirection(GetAttackDirection());
        mover.SetMove(Vector2.zero);
        mover.Update();
    }

    private void Move()
    {
        moveAnimator.AnimateDirection(DirCheck(moveDirection));
        mover.SetMove(moveDirection);
        mover.Update();
    }

    public Vector2 GetAttackDirection()
    {
        if (moveDirection != Vector2.zero)
            return DirCheck(moveDirection);
        else
            return DirCheck((player.transform.position - transform.position).normalized);
    }

    public Vector2 DirCheck(Vector2 dir)
    {
        Vector2 dirValue;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            dirValue = new Vector2(Mathf.Sign(dir.x), 0);
        }
        else
        {
            dirValue = new Vector2(0, Mathf.Sign(dir.y));
        }

        return dirValue;
    }

    public void SetState(EnemyState newState)
    {
        if (currentState != null && currentState == newState) return;
        currentState = newState;
    }
}
