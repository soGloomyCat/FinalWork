using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesBooster : MonoBehaviour
{
    private float _stepMultiplierIncrease;

    public float MultiplierHealth { get; private set; }

    private void Start()
    {
        MultiplierHealth = 1;
        _stepMultiplierIncrease = 0.2f;
    }

    public void IncreaseMultiplier()
    {
        MultiplierHealth += _stepMultiplierIncrease;
    }
}
