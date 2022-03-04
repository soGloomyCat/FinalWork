using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTransit : TransitionBase
{
    private void Update()
    {
        if (TargetBase.Health <= TargetHealthValue)
            NeedTransit = true;
    }
}
