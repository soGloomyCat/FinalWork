using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurvivorsShopOpportunities : MonoBehaviour
{
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private Turret _leftTurret;
    [SerializeField] private Turret _rightTurret;
    [SerializeField] private ZombiesSpawner _spawner;

    private int _killedZombies;
    private bool _isAvailableBullets;
    private bool _isAvailableDamage;

    public int WalletBalance { get; private set; }

    public event UnityAction ChangedBalance;
    public event UnityAction<float> ChangedBulletsType;
    public event UnityAction<int, int> ChangedResultsInfo;

    private void OnEnable()
    {
        _isAvailableBullets = false;
        _isAvailableDamage = false;
        _spawner.ZombieKilled += AddMoney;
    }

    private void OnDisable()
    {
        _spawner.ZombieKilled -= AddMoney;
    }

    public void BuyShieldPoints(int price)
    {
        WalletBalance -= price;
        ChangedResultsInfo?.Invoke(WalletBalance, _killedZombies);
    }

    public void BuyItem(ShopItem item)
    {
        WalletBalance -= item.Price;
        ChangedBalance?.Invoke();
        ChangedResultsInfo?.Invoke(WalletBalance, _killedZombies);

        switch (item.Label)
        {
            case "Double bullets":
                _bulletsPool.IncreaseShootCount();
                _isAvailableBullets = true;
                break;
            case "Triple bullet":
                _bulletsPool.IncreaseShootCount();
                break;
            case "Double damage":
                ChangedBulletsType?.Invoke(2f);
                _isAvailableDamage = true;
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

    public bool CheckAvailabelItem(string name)
    {
        switch (name)
        {
            case "Double bullets":
                return true;
            case "Triple bullet":
                if (_isAvailableBullets)
                    return true;

                return false;
            case "Double damage":
                return true;
            case "Triple damage":
                if(_isAvailableDamage)
                    return true;

                return false;
            case "Left turret":
                return true;
            case "Right turret":
                return true;
            default:
                break;
        }

        return false;
    }

    public void AddMoney(int reward)
    {
        WalletBalance += reward;
        _killedZombies++;
        ChangedResultsInfo?.Invoke(WalletBalance, _killedZombies);
        ChangedBalance?.Invoke();
    }
}
