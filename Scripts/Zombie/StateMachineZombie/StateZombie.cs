using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class StateZombie : MonoBehaviour
{
    [SerializeField] private TransitionZombie _transition;

    protected Animator CurrentAnimator;
    protected Base Target;

    private void OnEnable()
    {
        Target = GetComponent<Zombie>().Target;
        CurrentAnimator = GetComponent<Animator>();
    }

    public void EnterState()
    {
        enabled = true;

        if (_transition != null)
            _transition.enabled = true;
    }

    public StateZombie GetNextState()
    {
        if (_transition != null)
        {
            if (_transition.NeedTransit)
                return _transition.TargetState;
        }

        return null;
    }

    public void DisableState()
    {
        if (enabled == true)
            _transition.enabled = false;

        enabled = false;
    }
}
