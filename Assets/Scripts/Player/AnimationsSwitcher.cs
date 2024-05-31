using UnityEngine;

public class AnimationsSwitcher
{
    private const string Move = nameof(Move);
    private const string Idle = nameof(Idle);

    private readonly Animator _animator;

    public AnimationsSwitcher(Animator animator) =>
        _animator = animator;
    
    public void PlayMove()
    {
        _animator.Play(Move);
    }
}