using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurvivorsShopOpportunities : MonoBehaviour
{
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private Turret _leftTurret;
    [SerializeField] private Turret _rightTurret;

    private int _killedZombies;
    private SurvivorsInformationChanger _informationChanger;

    public int WalletBalance { get; private set; }

    public event UnityAction ChangedBalance;
    public event UnityAction<float> ChangedBulletsType;

    private void OnEnable()
    {
        _informationChanger = GetComponent<SurvivorsInformationChanger>();
    }

    public void BuyShieldPoints(int price)
    {
        WalletBalance -= price;
        _informationChanger.ChangeResultsInfo(WalletBalance, _killedZombies);
    }

    public void BuyItem(ShopItem item)
    {
        WalletBalance -= item.Price;
        ChangedBalance?.Invoke();
        _informationChanger.ChangeResultsInfo(WalletBalance, _killedZombies);

        switch (item.Label)
        {
            case "Double bullets":
                _bulletsPool.IncreaseShootCount();
                break;
            case "Triple bullet":
                _bulletsPool.IncreaseShootCount();
                break;
            case "Double damage":
                ChangedBulletsType?.Invoke(2f);
                break;
            case "Triple damage":
                ChangedBulletsType?.Invoke(1.5f);
                break;
            case "Left turret":
                _leftTurret.Activate();
                break;
            case "Right turret":
                _rightTurret.Activate();
                break;
            default:
                break;
        }
    }

    public void AddMoney(int reward)
    {
        WalletBalance += reward;
        _killedZombies++;
        _informationChanger.ChangeResultsInfo(WalletBalance, _killedZombies);
        ChangedBalance?.Invoke();
    }
}
