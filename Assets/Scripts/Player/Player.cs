using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IDamager, IItemPicker
{
    private const float MaxDelay = 0.5f;

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private AttackArea _attackArea;

    [Header("Moving Settings")]
    [SerializeField] private PlayerMover _mover;

    private float _attackDelay;
    private AnimationsPlayerSwitcher _animationsSwitcher;
    private InputService _inputService;

    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Damager Damager { get; private set; }

    public Wallet Wallet { get; private set; }

    private void OnEnable()
    {
        Health.Died += OnDie;
        Health.Changed += _healthBar.Set;
    }

    private void Awake() =>
        Init();

    private void OnDisable()
    {
        Health.Died -= OnDie;
        Health.Changed -= _healthBar.Set;
    }

    private void Update()
    {
        if (_attackDelay < MaxDelay)
            _attackDelay += Time.deltaTime;

        _inputService.Update();

        Move();
        Attack();
        Jump();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IItem item))
            item.Use(this);
    }

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

    private void Attack()
    {
        if (_inputService.IsAttacking && _mover.IsGrounded && _attackDelay >= MaxDelay)
        {
            Damager.Attack(_attackArea.Area);
            _animationsSwitcher.SetPunch();
            _attackDelay = 0;
        }
    }

    private void OnDie()
    {
        _animationsSwitcher.SetDie();
        gameObject.SetActive(false);
    }

    private void Init()
    {
        _inputService = new();
        Wallet = new();
        _attackDelay = MaxDelay;

        _mover.Init(GetComponent<Rigidbody2D>());
        _healthBar.Set(Health.Current);

        _animationsSwitcher = new(GetComponent<Animator>());
    }
}