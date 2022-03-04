using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : TransitionZombie
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Base>(out Base currentBase))
            NeedTransit = true;
    }
}
