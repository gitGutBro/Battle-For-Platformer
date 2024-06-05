using UnityEngine;

public class AnimationsSwitcher
{
    public readonly static int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    public readonly static int Speed = Animator.StringToHash(nameof(Speed));
    public readonly static int Punch = Animator.StringToHash(nameof(Punch));

    private readonly Animator _animator;

    public AnimationsSwitcher(Animator animator) =>
        _animator = animator;

    public void SetSpeed(float speed) =>
        _animator.SetFloat(Speed, Mathf.Abs(speed));

    public void SetGrounded(bool state) =>
        _animator.SetBool(IsGrounded, state);

    public void ToPunch() => 
        _animator.SetTrigger(Punch);
}