using UnityEngine;

public class AnimationState : State
{
    private readonly Animation _animation;
    private readonly AnimationClip _clip;

    public AnimationState(AnimationClip clip, Animation animation)
    {
        _clip = clip;
        _animation = animation;
    }

    protected override void OnEnter()
    {
        _animation.clip = _clip;
        _animation.Play();
    }

    protected override void OnExit()
    {
        _animation.Stop();
        _animation.clip = null;
    }
}