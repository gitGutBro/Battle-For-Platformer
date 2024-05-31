using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("Moving Settings")]
    [SerializeField] private Mover _mover;

    [SerializeField] private CharacterAnimations _animations;
    [SerializeField] private Animation _animation;

    private AnimationsSwitcher _animator;
    private StateMachine _stateMachine;

    private void Awake() =>
        Init();

    private void OnEnable()
    {
        _stateMachine.Run();
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
        _mover.JumpByKey();
    }

    private void FixedUpdate()
    {
        _mover.Move(transform);
    }

    private void Init()
    {
        _mover.Init(GetComponent<Rigidbody2D>());
        _animator = new(GetComponent<Animator>());
        _stateMachine = new CharacterStateMachineFactory().Create(_mover, _animations, _animation);
    }
}