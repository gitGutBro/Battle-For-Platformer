using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : Character
{
    [Header("Moving Settings")]
    [SerializeField] private PlayerMover _mover;

    private AnimationsSwitcher _animationsSwitcher;
    private InputService _inputService;

    private void Awake() =>
        Init();

    private void Update()
    {
        _inputService.Update();

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

    private void Init()
    {
        _inputService = new();
        _mover.Init(GetComponent<Rigidbody2D>());
        _animationsSwitcher = new(GetComponent<Animator>());
    }
}