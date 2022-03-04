using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTransit : TransitionBase
{
    private void Update()
    {
        if (TargetBase.Health <= TargetHealthValue)
            NeedTransit = true;
    }
}
