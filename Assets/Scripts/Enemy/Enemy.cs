using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Character
{
    [Header("Target Settings")]
    [SerializeField][Min(1f)] private float _detectionDistance;
    [SerializeField] private LayerMask _targetMask;
    [Space]
    [SerializeField] private EnemyMover _mover;

    private RaycastHit2D _hit;
    private EnemyStateMachine _stateMachine;

    private void Awake() => 
        Init();

    private void Update()
    {
        if (_hit == false)
        {
            _stateMachine.ChangeState(typeof(PatrolEnemyState));
            return;
        }

    }

    private void FixedUpdate()
    {
        _hit = TryFindPlayer();
    }

    private RaycastHit2D TryFindPlayer() =>
        Physics2D.Raycast(transform.position, 
                          Vector2.left * transform.localScale.x, 
                          _detectionDistance, 
                          _targetMask);

    private void Init()
    {
        _mover.Init(GetComponent<Rigidbody2D>());

        _stateMachine = new(_mover);
    }
}