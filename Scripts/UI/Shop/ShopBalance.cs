using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopBalance : MonoBehaviour
{
    [SerializeField] private Hero _buyer;
    [SerializeField] private TMP_Text _balance;

    private void OnEnable()
    {
        ChangeBalanceInfo();
        _buyer.ChangedBalance += ChangeBalanceInfo;
    }

    private void OnDisable()
    {
        _buyer.ChangedBalance -= ChangeBalanceInfo;
    }

    private void ChangeBalanceInfo()
    {
        _balance.text = $"Balance - {_buyer.WalletBalance}";
    }
}
