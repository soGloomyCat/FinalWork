using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Hero : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Ray _ray;
    [SerializeField] private Turret _leftTurret;
    [SerializeField] private Turret _rightTurret;
    [SerializeField] private float _cooldown;
    [SerializeField] private TMP_Text _killedZombiesText;
    [SerializeField] private TMP_Text _currentBalanceText;
    [SerializeField] private TMP_Text _killedZombiesResultText;
    [SerializeField] private AudioSource _shootSound;

    private Vector2 _mouseCoordinates;
    private float _rotateAngle;
    private float _afterShootTime;
    private int _killedZombies;
    private int _wallet;

    public int WalletBalance => _wallet;
    public Bullet Bullet => _bullet;

    public event UnityAction ChangedBalance;
    public event UnityAction<float> ChangedBulletsType;

    private void OnEnable()
    {
        ChangeResultsInfo();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            RotateHero();

            if (Input.GetMouseButton(0))
            {
                if (_afterShootTime <= 0)
                {
                    _ray.ShowRay();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _ray.HideRay();
                Shoot();
            }

            if (_afterShootTime > 0)
                _afterShootTime -= Time.deltaTime;
        }
    }

    public void BuyShieldPoints(int price)
    {
        _wallet -= price;
        ChangeResultsInfo();
    }

    public void BuyItem(ShopItem item)
    {
        _wallet -= item.Price;
        ChangedBalance?.Invoke();
        ChangeResultsInfo();

        switch (item.Label)
        {
            case "Double bullets":
                _bulletsPool.IncreaseIterationsCount();
                break;
            case "Triple bullet":
                _bulletsPool.IncreaseIterationsCount();
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
        ChangeResultsInfo();
        ChangedBalance?.Invoke();
    }

    private void RotateHero()
    {
        _mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _rotateAngle = Mathf.Atan2(_mouseCoordinates.x, _mouseCoordinates.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Clamp(_rotateAngle, -60, 60));
    }

    private void Shoot()
    {
        if (_afterShootTime <= 0)
        {
            _shootSound.Play();
            _bulletsPool.InitializeShoot(Quaternion.Euler(0, 0, -Mathf.Clamp(_rotateAngle, -60, 60)), _shootPoint.position);
            _afterShootTime = _cooldown;
        }
    }

    private void ChangeResultsInfo()
    {
        _currentBalanceText.text = $"Balance\n{_wallet}";
        _killedZombiesText.text = $"Killed Zombies\n{_killedZombies}";
        _killedZombiesResultText.text = $"Killed Zombies - {_killedZombies}";
    }
}
