using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private Hero _survivor;
    [SerializeField] private int _poolCapacity;

    private int _iterationCount;
    private List<Bullet> _bulletsList;
    private Bullet _tempBullet;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _survivor.ChangedBulletsType += ChangeBulletsParameters;
    }

    private void OnDisable()
    {
        _survivor.ChangedBulletsType -= ChangeBulletsParameters;
    }

    private void Start()
    {
        _iterationCount = 1;
        _tempBullet = GetComponent<Bullet>();
        _bulletsList = new List<Bullet>();
        InitializePool(_survivor.Bullet);
    }

    public void InitializeShoot(Quaternion rotation, Vector3 spawnPosition)
    {
        _coroutine = StartCoroutine(ShootBullet(rotation, spawnPosition));
    }

    public void IncreaseIterationsCount()
    {
        _iterationCount++;
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
        foreach (var bullet in _bulletsList)
        {
            bullet.IncreaseDamage(multiplier);
            bullet.ChangeSprite();
        }
    }

    private void StopRoutine()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator ShootBullet(Quaternion rotation, Vector3 spawnPosition)
    {
        WaitForSeconds timer = new WaitForSeconds(0.1f);

        for (int i = 0; i < _iterationCount; i++)
        {
            Bullet bullet = _bulletsList.First(p => p.gameObject.activeSelf == false);
            bullet.ActivatedBullet(rotation, spawnPosition);
            yield return timer;
        }

        StopRoutine();
    }
}
