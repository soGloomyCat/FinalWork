using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _shieldBar;

    public float ShieldPoints { get; private set; }
    public float Health => _health;

    public event UnityAction Destroyed;

    private void OnEnable()
    {
        _healthBar.value = _health;
        _shieldBar.value = 0;
    }

    public void TakeDamge(float damage)
    {
        if (ShieldPoints <= 0)
        {
            _health -= damage;
            _healthBar.value = _health / 100;
        }
        else
        { 
            ShieldPoints -= damage;
            _shieldBar.value = ShieldPoints / 100;
        }

        if (_health < 0)
            Destroyed?.Invoke();
    }

    public void IncreaseArmor()
    {
        if (ShieldPoints > 75)
            ShieldPoints += 100 - ShieldPoints;

        ShieldPoints += 25;
        _shieldBar.value = ShieldPoints / 100;
    }
}
