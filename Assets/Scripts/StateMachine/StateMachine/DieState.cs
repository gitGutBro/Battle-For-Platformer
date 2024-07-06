using UnityEngine;

public class DieState : IState
{
    private readonly static int Die = Animator.StringToHash(nameof(Die));
    private readonly Animator _animator;

    public DieState(Animator animator) => 
        _animator = animator;

    public void Enter() => 
        _animator.SetTrigger(Die);

    public void Exit() { }
}