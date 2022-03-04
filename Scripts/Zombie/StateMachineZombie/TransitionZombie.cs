using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitionZombie : MonoBehaviour
{
    [SerializeField] private StateZombie _targetState;

    public StateZombie TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
