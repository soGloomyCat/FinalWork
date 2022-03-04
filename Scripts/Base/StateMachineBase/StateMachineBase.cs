using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Base))]
public class StateMachineBase : MonoBehaviour
{
    [SerializeField] private StateBase _startState;

    private Base _base;
    private StateBase _currentState;
    private StateBase _tempState;

    private void Start()
    {
        _base = GetComponent<Base>();
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

    private void ResetMachine(StateBase state)
    {
        _currentState = state;
        _currentState.InitBaseHealth(_base);

        if (_currentState != null)
            _currentState.EnterState();
    }

    private void TransitState(StateBase nextState)
    {
        if (_currentState != null)
            _currentState.DisableState();

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.EnterState();
            _currentState.InitBaseHealth(_base);
        }
    }
}
