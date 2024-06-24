using System;
using System.Threading;

public class PatrolEnemyState : IEnemyState
{
    private EnemyMover _mover;
    private CancellationTokenSource _tokenSource;

    public PatrolEnemyState(EnemyMover mover) => 
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));

    public void Enter()
    {
        _tokenSource = new();
        _mover.PatrolAsync(_tokenSource.Token);
    }

    public void Exit()
    {
        _tokenSource.Cancel();
    }
}