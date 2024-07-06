using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState _currentState;
    private Dictionary<Type, IState> _states;

    public StateMachine(EnemyMover mover, Damager damager, AttackArea attackArea, Animator animator)
    {
        _states = new()
        {
            { typeof(PatrolState), new PatrolState(mover)},
            { typeof(MoveToTargetState), new MoveToTargetState(mover)},
            { typeof(AttackState), new AttackState(damager, attackArea, animator)},
            { typeof(DieState), new DieState(animator)}
        };

        _currentState = _states[typeof(PatrolState)];

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