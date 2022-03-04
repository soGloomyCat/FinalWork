using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Hero _survivor;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _shieldBar;
    [SerializeField] private Button _shieldButton;

    private float _shieldPoints;
    private int _price;

    public float Health => _health;

    public event UnityAction BaseDestroyed;

    private void OnEnable()
    {
        _price = 100;
        _shieldButton.interactable = false;
        _healthBar.value = _health;
        _shieldBar.value = 0;
        _shieldButton.onClick.AddListener(BuyShieldPoints);
    }

    private void OnDisable()
    {
        _shieldButton.onClick.RemoveListener(BuyShieldPoints);
    }

    private void Update()
    {
        if (_survivor.WalletBalance >= _price)
            _shieldButton.interactable = true;
        else
            _shieldButton.interactable = false;
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
            BaseDestroyed?.Invoke();
    }

    private void BuyShieldPoints()
    {
        _shieldPoints += 25;
        _shieldBar.value = _shieldPoints / 100;
        _survivor.BuyShieldPoints(_price);
    }
}
