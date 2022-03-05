using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shoper : MonoBehaviour
{
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private Turret _leftTurret;
    [SerializeField] private Turret _rightTurret;

    private int _wallet;
    private int _killedZombies;
    private InformationChanger _informationChanger;

    public int WalletBalance => _wallet;

    public event UnityAction ChangedBalance;
    public event UnityAction<float> ChangedBulletsType;

    private void OnEnable()
    {
        _informationChanger = GetComponent<InformationChanger>();
    }

    public void BuyShieldPoints(int price)
    {
        _wallet -= price;
        _informationChanger.ChangeResultsInfo(_wallet, _killedZombies);
    }

    public void BuyItem(ShopItem item)
    {
        _wallet -= item.Price;
        ChangedBalance?.Invoke();
        _informationChanger.ChangeResultsInfo(_wallet, _killedZombies);

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
        _wallet += reward;
        _killedZombies++;
        _informationChanger.ChangeResultsInfo(_wallet, _killedZombies);
        ChangedBalance?.Invoke();
    }
}
