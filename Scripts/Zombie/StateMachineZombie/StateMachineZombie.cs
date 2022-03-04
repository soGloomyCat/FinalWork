using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Zombie))]
public class StateMachineZombie : MonoBehaviour
{
    [SerializeField] private StateZombie _startState;

    private StateZombie _currentState;
    private StateZombie _tempState;
    private Zombie _currentZombie;

    private void Start()
    {
        ResetMachine(_startState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        _tempState = _currentState.GetNextState();

        if (_tempState != null)
            TransitState(_tempState);
    }

    private void ResetMachine(StateZombie state)
    {
        _currentState = state;

        if (_currentState != null)
        { 
            _currentState.EnterState();
        }
    }

    private void TransitState(StateZombie nextState)
    {
        if (_currentState != null)
            _currentState.DisableState();

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.EnterState();
        }
    }
}
