using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : Character
{
    private const float MaxDelay = 0.5f;

    [SerializeField] private AttackArea _attackArea;

    [Header("Moving Settings")]
    [SerializeField] private PlayerMover _mover;

    private float _eventDelay;
    private AnimationsPlayerSwitcher _animationsSwitcher;
    private InputService _inputService;

    private void OnEnable() => 
        _attackArea.CharacterAttacking += OnAttack;

    private void Awake() =>
        Init();

    private void OnDisable() => 
        _attackArea.CharacterAttacking -= OnAttack;

    private void Update()
    {
        if (_eventDelay < MaxDelay)
            _eventDelay += Time.deltaTime;

        _inputService.Update();

        OnAttack();
        Jump();
    }

    private void FixedUpdate() => 
        Move();

    private void Move()
    {
        _mover.Move(_inputService.AxisHorizontal);
        _animationsSwitcher.SetSpeed(_inputService.AxisHorizontal);
    }

    private void Jump()
    {
        if (_inputService.IsJumped)
            _mover.Jump();

        _animationsSwitcher.SetGrounded(_mover.IsGrounded);
    }

    private void OnAttack()
    {
        if (_inputService.IsAttacking && _mover.IsGrounded && _eventDelay >= MaxDelay)
        {
            Damager.Attack(_attackArea.Area);
            _animationsSwitcher.ToPunch();
            _eventDelay = 0;
        }
    }

    private void Init()
    {
        _inputService = new();
        _eventDelay = MaxDelay;

        _mover.Init(GetComponent<Rigidbody2D>());
        _animationsSwitcher = new(GetComponent<Animator>());
    }
}