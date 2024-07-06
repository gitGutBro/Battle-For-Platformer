using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class AttackState : IState
{
    private const float MaxDelay = 0.5f;
    private readonly static int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    private readonly Damager _damager;
    private readonly AttackArea _attackArea;
    private readonly Animator _animator;

    private CancellationTokenSource _tokenSource;

    public AttackState(Damager damager, AttackArea attackArea, Animator animator)
    {
        _damager = damager ?? throw new ArgumentNullException(nameof(damager));
        _attackArea = attackArea;
        _animator = animator;
    }

    public void Enter() 
    {
        _tokenSource = new();
        _animator.SetBool(IsAttacking, true);

        AttackAsync(_tokenSource.Token).HideWarning();
    }

    public void Exit()
    {
        _tokenSource.Cancel();
        _animator.SetBool(IsAttacking, false);
    }

    private async UniTask AttackAsync(CancellationToken token)
    {
        while (token.IsCancellationRequested == false)
        {
            _damager.Attack(_attackArea.Area);

            await UniTask.Delay(TimeSpan.FromSeconds(MaxDelay));
        }
    }
}