using UnityEngine;

public class AnimationsSwitcher
{
    public readonly static int Jump = Animator.StringToHash(nameof(Jump));
    public readonly static int Land = Animator.StringToHash(nameof(Land));
    public readonly static int Speed = Animator.StringToHash(nameof(Speed));
    public readonly static int Punch = Animator.StringToHash(nameof(Punch));

    private readonly Animator _animator;

    public AnimationsSwitcher(Animator animator) =>
        _animator = animator;

    public void SetSpeed(float speed) =>
        _animator.SetFloat(Speed, Mathf.Abs(speed));

    public void ToLand() =>
        _animator.SetTrigger(Land);

    public void ToJump() => 
        _animator.SetTrigger(Jump);

    public void ToPunch() => 
        _animator.SetTrigger(Punch);
}