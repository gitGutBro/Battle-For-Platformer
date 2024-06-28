using System;
using System.Collections.Generic;

public class EnemyStateMachine
{
    private IEnemyState _currentState;

    private Dictionary<Type, IEnemyState> _states;

    public EnemyStateMachine(EnemyMover mover)
    {
        _states = new()
        {
            { typeof(PatrolEnemyState), new PatrolEnemyState(mover)},
            { typeof(MoveToTargetState), new MoveToTargetState(mover)},
            { typeof(EmptyState), new EmptyState()}
            
        };

        _currentState = _states[typeof(PatrolEnemyState)];

        _currentState.Enter();
    }

    public void ChangeState(Type type)
    {
        if (_states[type] == _currentState)
            return;

        _currentState.Exit();
        _currentState = _states[type];
        _currentState.Enter();
    }
}