using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IDamager
{
    [SerializeField] private EnemyAttackArea _enemyAttackArea;
    [Space]
    [Header("Target Settings")]
    [SerializeField][Min(1f)] private float _detectionDistance;
    [SerializeField] private LayerMask _targetMask;
    [Space]
    [SerializeField] private EnemyMover _mover;

    private bool _isAttacking;
    private StateMachine _stateMachine;

    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Damager Damager { get; private set; }

    private RaycastHit2D TargetHit => TryFindPlayer();

    private void OnEnable()
    {
        _mover.TargetHit += TryFindPlayer;

        _enemyAttackArea.CharacterAttacking += OnAttack;
        _enemyAttackArea.CharacterNotAttacking += OnStopAttack;

        Health.Died += OnDied;
    }

    private void Awake() => 
        Init();

    private void OnDisable()
    {
        _mover.TargetHit -= TryFindPlayer;

        _enemyAttackArea.CharacterAttacking -= OnAttack;
        _enemyAttackArea.CharacterNotAttacking -= OnStopAttack;

        Health.Died -= OnDied;
    }

    private void Update()
    {
        if (_isAttacking)
            return;

        if (TargetHit == false)
        {
            _stateMachine.ChangeState(typeof(PatrolState));
            return;
        }

        if (TargetHit)
            _stateMachine.ChangeState(typeof(MoveToTargetState));
    }

    private void OnAttack()
    {
        _isAttacking = true;

        _stateMachine.ChangeState(typeof(AttackState));
    }

    private void OnStopAttack() =>
        _isAttacking = false;

    private void OnDied()
    {
        _isAttacking = false;

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

        _stateMachine = new(_mover, Damager, _enemyAttackArea, GetComponent<Animator>());
    }
}