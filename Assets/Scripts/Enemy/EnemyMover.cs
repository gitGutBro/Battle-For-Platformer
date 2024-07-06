using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[Serializable]
public class EnemyMover
{
    [SerializeField][Min(1.5f)] private float _speed;
    [SerializeField] private Transform[] _patrolPoints;

    private int _currentPoint;
    private Rigidbody2D _rigidbody2D;

    public event Func<RaycastHit2D> TargetHit;

    public Transform GetTargetTransform() => 
        TargetHit.Invoke().transform;

    public async UniTask PatrolAsync(CancellationToken token)
    {
        while (token.IsCancellationRequested == false)
        {
            _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;

            await MoveToAsync(_patrolPoints[_currentPoint], token);
        }
    }

    public async UniTask MoveToAsync(Transform point, CancellationToken token)
    {
        while (IsPointDetected(point) && token.IsCancellationRequested == false)
        {
            _rigidbody2D.transform.position = MovingTowards(point);
            _rigidbody2D.transform.Flip(_rigidbody2D.transform.position.x - point.position.x);

            await UniTask.Yield();
        }
    }

    public void Init(Rigidbody2D rigidbody2D) =>
        _rigidbody2D = rigidbody2D;

    private bool IsPointDetected(Transform point)
    {
        const float RangePointDetecting = 0.5f;

        return Vector2.Distance
            (_rigidbody2D.transform.position, point.position) > RangePointDetecting;
    }

    private Vector2 MovingTowards(Transform point) =>
        Vector2.MoveTowards
        (_rigidbody2D.transform.position, 
        point.position, 
        _speed * Time.deltaTime);
}