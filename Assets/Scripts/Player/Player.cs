using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("Moving Settings")]
    [SerializeField] private Mover _mover;

    private AnimationsSwitcher _animationsSwitcher;
    private InputService _inputService;

    private void Awake() =>
        Init();

    private void Update()
    {
        _mover.Update();
        _inputService.Update();

        _mover.Move(_inputService.AxisHorizontal);
        _animationsSwitcher.SetSpeed(_inputService.AxisHorizontal);

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