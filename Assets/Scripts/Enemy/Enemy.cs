using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : Character
{
    private const float MaxDelay = 0.5f;
    private readonly static int IsAttacking = Animator.StringToHash(nameof(IsAttacking));

    [SerializeField] private EnemyAttackArea _enemyAttackArea;
    [Space]
    [Header("Target Settings")]
    [SerializeField][Min(1f)] private float _detectionDistance;
    [SerializeField] private LayerMask _targetMask;
    [Space]
    [SerializeField] private EnemyMover _mover;

    private bool _isAttacking;
    private float _eventDelay;
    private EnemyStateMachine _stateMachine;
    private Animator _animator;

    private void OnEnable()
    {
        _enemyAttackArea.CharacterAttacking += OnAttack;
        _enemyAttackArea.CharacterNotAttacking += OnStopAttack;
    }

    private void Awake() => 
        Init();

    private void OnDisable()
    {
        _enemyAttackArea.CharacterAttacking -= OnAttack;
        _enemyAttackArea.CharacterNotAttacking -= OnStopAttack;
    }

    private void Update()
    {
        if (_eventDelay < MaxDelay)
            _eventDelay += Time.deltaTime;

        if (_isAttacking)
            return;

        _animator.SetBool(IsAttacking, _isAttacking);

        if (_mover.TargetHit == false)
        {
            _stateMachine.ChangeState(typeof(PatrolEnemyState));
            return;
        }

        if (_mover.TargetHit)
            _stateMachine.ChangeState(typeof(MoveToTargetState));
    }

    private void FixedUpdate() => 
        _mover.TakeTargetHit(TryFindPlayer());
    
    private void OnAttack()
    {
        if (_eventDelay < MaxDelay)
            return;

        _eventDelay = 0;
        Damager.Attack(_mover.TargetHit.collider);
        _isAttacking = true;
        _animator.SetBool(IsAttacking, _isAttacking);
        _stateMachine.ChangeState(typeof(EmptyState));
    }

    private void OnStopAttack() =>
        _isAttacking = false;

    private RaycastHit2D TryFindPlayer() =>
        Physics2D.Raycast(transform.position, 
                          -transform.right, 
                          _detectionDistance, 
                          _targetMask);

    private void Init()
    {
        _mover.Init(GetComponent<Rigidbody2D>());
        _animator = GetComponent<Animator>();

        _eventDelay = MaxDelay;
        _stateMachine = new(_mover);
    }
}