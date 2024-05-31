using UnityEngine;

public class CharacterStateMachineFactory
{
    public StateMachine Create(INotifyVelocityChanged mover, CharacterAnimations animations, Animation animation)
    {
        AnimationState idleState = new(animations.Idle, animation);
        AnimationState moveState = new(animations.Move, animation);

        ToIdleTransition toIdleTransition = new(idleState, mover);
        ToMoveTransition toMoveTransition = new(moveState, mover);

        idleState.Add(toMoveTransition);
        moveState.Add(toIdleTransition);

        return new StateMachine(idleState);
    }
}