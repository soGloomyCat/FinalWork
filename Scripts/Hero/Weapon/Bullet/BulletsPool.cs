using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private Hero _survivor;
    [SerializeField] private int _poolCapacity;

    private int _shootCount;
    private List<Bullet> _bulletsList;
    private Bullet _tempBullet;
    private Coroutine _coroutine;
    private Shoper _shoper;

    private void OnEnable()
    {
        _shoper = _survivor.GetComponent<Shoper>();
        _shoper.ChangedBulletsType += ChangeBulletsParameters;
    }

    private void OnDisable()
    {
        _shoper.ChangedBulletsType -= ChangeBulletsParameters;
    }

    private void Start()
    {
        _shootCount = 1;
        _tempBullet = GetComponent<Bullet>();
        _bulletsList = new List<Bullet>();
        InitializePool(_survivor.Bullet);
    }

    public void InitializeShoot(Quaternion rotation, Vector3 spawnPosition)
    {
        _coroutine = StartCoroutine(ShootBullet(rotation, spawnPosition));
    }

    public void IncreaseShootCount()
    {
        _shootCount++;
    }

    private void InitializePool(Bullet bullet)
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            _tempBullet = Instantiate(bullet, transform);
            _bulletsList.Add(_tempBullet);
            _tempBullet.gameObject.SetActive(false);
        }
    }

    private void ChangeBulletsParameters(float multiplier)
    {
        BulletSpriteSwitcher bulletSpriteSwitcher;
        BulletDamageSwitcher bulletDamageSwitcher;

        foreach (var bullet in _bulletsList)
        {
            bulletSpriteSwitcher = bullet.GetComponent<BulletSpriteSwitcher>();
            bulletDamageSwitcher = bullet.GetComponent<BulletDamageSwitcher>();
            bulletDamageSwitcher.IncreaseDamage(multiplier);
            bulletSpriteSwitcher.ChangeSprite();
        }
    }

    private void StopRoutine()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator ShootBullet(Quaternion rotation, Vector3 spawnPosition)
    {
        WaitForSeconds timer = new WaitForSeconds(0.1f);

        for (int i = 0; i < _shootCount; i++)
        {
            Bullet bullet = _bulletsList.First(p => p.gameObject.activeSelf == false);
            bullet.Activate(rotation, spawnPosition);
            yield return timer;
        }

        StopRoutine();
    }
}
