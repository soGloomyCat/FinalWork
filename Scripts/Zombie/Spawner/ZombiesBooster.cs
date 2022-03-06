using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesBooster : MonoBehaviour
{
    private ZombiesSpawner _spawner;
    private float _stepMultiplierIncrease;

    public float MultiplierHealth { get; private set; }

    private void OnEnable()
    {
        _spawner = GetComponent<ZombiesSpawner>();
        _spawner.BoostZombies += IncreaseMultiplier;
        MultiplierHealth = 1;
        _stepMultiplierIncrease = 0.2f;
    }

    private void OnDisable()
    {
        _spawner.BoostZombies -= IncreaseMultiplier;
    }

    public void IncreaseMultiplier()
    {
        MultiplierHealth += _stepMultiplierIncrease;
    }
}
