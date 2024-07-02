using UnityEngine;

public class AnimationsPlayerSwitcher
{
    public readonly static int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    public readonly static int Speed = Animator.StringToHash(nameof(Speed));
    public readonly static int Punch = Animator.StringToHash(nameof(Punch));
    public readonly static int Die = Animator.StringToHash(nameof(Die));

    private readonly Animator _animator;

    public AnimationsPlayerSwitcher(Animator animator) =>
        _animator = animator;

    public void SetSpeed(float speed) =>
        _animator.SetFloat(Speed, Mathf.Abs(speed));

    public void SetGrounded(bool state) =>
        _animator.SetBool(IsGrounded, state);

    public void SetPunch() => 
        _animator.SetTrigger(Punch);

    public void SetDie() => 
        _animator.SetTrigger(Die);
}