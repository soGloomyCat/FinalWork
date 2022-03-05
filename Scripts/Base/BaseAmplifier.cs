using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Base))]
public class BaseAmplifier : MonoBehaviour
{
    [SerializeField] private SurvivorsShopOpportunities _survivor;
    [SerializeField] private Button _shieldButton;

    private Base _targetBase;
    private int _armorPrice;

    private void OnEnable()
    {
        _targetBase = GetComponent<Base>();
        _armorPrice = 100;
        _shieldButton.interactable = false;
        _shieldButton.onClick.AddListener(BuyShieldPoints);
    }

    private void OnDisable()
    {
        _shieldButton.onClick.RemoveListener(BuyShieldPoints);
    }

    private void Update()
    {
        _shieldButton.interactable = _survivor.WalletBalance >= _armorPrice;
    }

    private void BuyShieldPoints()
    {
        _targetBase.IncreaseArmor();
        _survivor.BuyShieldPoints(_armorPrice);
    }
}
