public class StateMachine
{
    private readonly State _firstState;

    private State _currentState;

    public StateMachine(State currentState) =>
        _firstState = currentState;

    public void Run() =>
        Change(_firstState);

    public void Change(State state)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
            _currentState.NextStateChanged -= Change;
        }

        _currentState = state;

        if (_currentState != null)
        {
            _currentState.NextStateChanged += Change;
            _currentState.Enter();
        }
    }
}