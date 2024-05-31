using System;
using UnityEngine;

public class ToMoveTransition : Transition
{
    private readonly INotifyVelocityChanged _mover;

    public ToMoveTransition(State nextState, INotifyVelocityChanged mover) : base(nextState) => 
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));

    public override void Disable() => 
        _mover.VelocityChanged -= OnVelocityChanged;

    public override void Enable() => 
        _mover.VelocityChanged += OnVelocityChanged;

    private void OnVelocityChanged(Vector2 velocity)
    {
        if (velocity.magnitude >= Constants.Epsilon)
            Transit();
    }
}