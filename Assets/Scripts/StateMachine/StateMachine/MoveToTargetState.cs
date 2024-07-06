using System;
using System.Threading;
using UnityEngine;

public class MoveToTargetState : IState
{
    private readonly EnemyMover _mover;

    private CancellationTokenSource _tokenSource;

    public MoveToTargetState(EnemyMover mover) => 
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));

    public void Enter()
    {
        _tokenSource = new();

        _mover.MoveToAsync(_mover.GetTargetTransform(), _tokenSource.Token).HideWarning();
    }

    public void Exit() => 
        _tokenSource.Cancel();
}