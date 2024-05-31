using System;

public abstract class Transition
{
    private readonly State _nextState;

    public Transition(State nextState) => 
        _nextState = nextState;

    public event Action<State> Completed; 

    public abstract void Enable();

    public abstract void Disable();

    protected void Transit() => 
        Completed?.Invoke(_nextState);
}