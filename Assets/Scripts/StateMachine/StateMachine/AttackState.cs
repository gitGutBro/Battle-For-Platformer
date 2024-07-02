using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class AttackState : IState
{
    private const float MaxDelay = 0.5f;
    private readonly Damager _damager;
    private readonly AttackArea _attackArea;

    private CancellationTokenSource _tokenSource;

    public AttackState(Damager damager, AttackArea attackArea)
    {
        _damager = damager ?? throw new ArgumentNullException(nameof(damager));
        _attackArea = attackArea;
    }

    public void Enter() 
    {
        _tokenSource = new();
        AttackAsync(_tokenSource.Token).HideWarning();
    }

    public void Exit() => 
        _tokenSource.Cancel();

    private async UniTask AttackAsync(CancellationToken token)
    {
        while (token.IsCancellationRequested == false)
        {
            _damager.Attack(_attackArea.Area);

            await UniTask.Delay(TimeSpan.FromSeconds(MaxDelay));
        }
    }
}