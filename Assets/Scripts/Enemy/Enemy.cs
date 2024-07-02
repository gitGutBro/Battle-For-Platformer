using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IDamager
{
    private readonly static int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    private readonly static int Die = Animator.StringToHash(nameof(Die));

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EnemyAttackArea _enemyAttackArea;
    [Space]
    [Header("Target Settings")]
    [SerializeField][Min(1f)] private float _detectionDistance;
    [SerializeField] private LayerMask _targetMask;
    [Space]
    [SerializeField] private EnemyMover _mover;

    private bool _isAttacking;
    private StateMachine _stateMachine;
    private Animator _animator;

    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Damager Damager { get; private set; }

    private void OnEnable()
    {
        _enemyAttackArea.CharacterAttacking += OnAttack;
        _enemyAttackArea.CharacterNotAttacking += OnStopAttack;

        Health.Died += OnDied;
        Health.Changed += _healthBar.Set;
    }

    private void Awake() => 
        Init();

    private void OnDisable()
    {
        _enemyAttackArea.CharacterAttacking -= OnAttack;
        _enemyAttackArea.CharacterNotAttacking -= OnStopAttack;

        Health.Died -= OnDied;
        Health.Changed -= _healthBar.Set;
    }

    private void Update()
    {
        if (_isAttacking)
            return;

        _animator.SetBool(IsAttacking, _isAttacking);

        if (_mover.TargetHit == false)
        {
            _stateMachine.ChangeState(typeof(PatrolState));
            return;
        }

        if (_mover.TargetHit)
            _stateMachine.ChangeState(typeof(MoveToTargetState));
    }

    private void FixedUpdate() => 
        _mover.TakeTargetHit(TryFindPlayer());
    
    private void OnAttack()
    {
        _isAttacking = true;
        _animator.SetBool(IsAttacking, _isAttacking);

        _stateMachine.ChangeState(typeof(AttackState));
    }

    private void OnStopAttack() =>
        _isAttacking = false;

    private void OnDied()
    {
        _isAttacking = false;
        _animator.SetTrigger(Die);

        _stateMachine.ChangeState(typeof(DieState));
        gameObject.SetActive(false);
    }

    private RaycastHit2D TryFindPlayer() =>
        Physics2D.Raycast(transform.position, 
                          -transform.right, 
                          _detectionDistance, 
                          _targetMask);

    private void Init()
    {
        _mover.Init(GetComponent<Rigidbody2D>());
        _animator = GetComponent<Animator>();

        _healthBar.Set(Health.Current);
        _stateMachine = new(_mover, Damager, _enemyAttackArea);
    }
}