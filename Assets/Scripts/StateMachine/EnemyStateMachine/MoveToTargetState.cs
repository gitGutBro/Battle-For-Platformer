using System;
using System.Threading;
using UnityEngine;

public class MoveToTargetState : IEnemyState
{
    private readonly EnemyMover _mover;

    private CancellationTokenSource _tokenSource;
    private RaycastHit2D _target => _mover.TargetHit;

    public MoveToTargetState(EnemyMover mover) => 
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));

    public void Enter()
    {
        _tokenSource = new();
        _mover.MoveToAsync(_target.transform, _tokenSource.Token);
    }

    public void Exit() => 
        _tokenSource.Cancel();
}