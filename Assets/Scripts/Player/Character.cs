using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public PlayerData playerData;

    public MoveAnimate Animator;

    private PlayerState currentState;
    private PlayerIdleState idleState;
    private PlayerWalkState walkState;

    private MovingController mover;

    private PositionObserv posObserver;
    private LayersManager layersManager;

    [SerializeField] private float speed;

    private Vector2 moveDirection;

    public void Awake()
    {
        Animator = new MoveAnimate(GetComponent<Animator>(), "MoveX", "MoveY");
        mover = new MovingController(GetComponent<Rigidbody2D>(), speed);

        posObserver = GetComponent<PositionObserv>();
        layersManager = FindAnyObjectByType<LayersManager>();
        layersManager.RegisterSubject(transform, posObserver);

        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);
        currentState = idleState;
    }

    private void Update()
    {
        mover.Update();

        if (moveDirection.magnitude > 0.1f)
        {
            SetState(walkState);
            if (!AudioManager.Instance.PlayerSFX.isPlaying)
            {
                AudioManager.Instance.PlaySound(AudioManager.Instance.PlayerSFX, AudioManager.Instance.Walk, AudioManager.Instance.sfxVolume);
            }
        }
        else
        {
            SetState(idleState);
            if (AudioManager.Instance.PlayerSFX.isPlaying)
                AudioManager.Instance.PlayerSFX.Stop();
        }

        currentState.Update();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

        mover.SetMove(moveDirection);
    }

    public Vector2 GetMoveDirection() => moveDirection;

    public void SetState(PlayerState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
    }

}
