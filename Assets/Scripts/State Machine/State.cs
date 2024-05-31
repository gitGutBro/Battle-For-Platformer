using System;
using System.Collections.Generic;

public abstract class State
{
    private List<Transition> _transitions;

    public State() =>
        _transitions = new List<Transition>();

    public event Action<State> NextStateChanged;

    private void OnTransitionCompleted(State state) =>
        NextStateChanged?.Invoke(state);

    public void Enter()
    {
        foreach (Transition transition in _transitions)
            transition.Completed += OnTransitionCompleted;
        
        foreach (Transition transition in _transitions)
            transition.Enable();

        OnEnter();
    }

    public void Exit()
    {
        OnExit();

        foreach (Transition transition in _transitions)
            transition.Disable();

        foreach (Transition transition in _transitions)
            transition.Completed -= OnTransitionCompleted;
    }

    public void Add(Transition transition) =>
        _transitions.Add(transition);

    public void Remove(Transition transition) =>
        _transitions.Remove(transition);

    protected abstract void OnEnter();

    protected abstract void OnExit();
}