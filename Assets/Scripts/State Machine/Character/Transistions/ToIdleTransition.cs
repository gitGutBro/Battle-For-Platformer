using System;
using UnityEngine;

public class ToIdleTransition : Transition
{
    private readonly INotifyVelocityChanged _mover;

    public ToIdleTransition(State nextState, INotifyVelocityChanged mover) : base(nextState) => 
        _mover = mover ?? throw new ArgumentNullException(nameof(mover));

    public override void Enable() =>
        _mover.VelocityChanged += OnVelocityChanged;

    public override void Disable() => 
        _mover.VelocityChanged -= OnVelocityChanged;

    private void OnVelocityChanged(Vector2 velocity)
    {
        if (velocity.magnitude < Constants.Epsilon)
            Transit();
    }
}