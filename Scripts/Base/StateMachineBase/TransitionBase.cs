using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitionBase : MonoBehaviour
{
    [SerializeField] private StateBase _targetState;
    [SerializeField] private int _targetHealthValue;

    protected Base TargetBase;

    protected int TargetHealthValue => _targetHealthValue;
    public StateBase TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void InitBaseHealth(Base targetBase)
    {
        TargetBase = targetBase;
    }
}
