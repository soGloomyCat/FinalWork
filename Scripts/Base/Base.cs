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

    private float _shieldPoints;

    public float Health => _health;

    public event UnityAction Destroyed;

    private void OnEnable()
    {
        _healthBar.value = _health;
        _shieldBar.value = 0;
    }

    public void TakeDamge(float damage)
    {
        if (_shieldPoints <= 0)
        {
            _health -= damage;
            _healthBar.value = _health / 100;
        }
        else
        { 
            _shieldPoints -= damage;
            _shieldBar.value = _shieldPoints / 100;
        }

        if (_health < 0)
            Destroyed?.Invoke();
    }

    public void IncreaseArmor()
    {
        _shieldPoints += 25;
        _shieldBar.value = _shieldPoints / 100;
    }
}
