using System;
using System.Threading;

public class PatrolState : IState
{
    private readonly EnemyMover _mover;

    private CancellationTokenSource _tokenSource;

    public PatrolState(EnemyMover mover) => 
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));

    public void Enter()
    {
        _tokenSource = new();
        _mover.PatrolAsync(_tokenSource.Token).HideWarning();
    }

    public void Exit() => 
        _tokenSource.Cancel();
}