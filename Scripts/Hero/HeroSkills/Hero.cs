using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HeroMover))]
public class Hero : MonoBehaviour
{
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Ray _ray;
    [SerializeField] private float _cooldown;
    [SerializeField] private AudioSource _shootSound;
    
    private float _rotateAngle;
    private float _afterShootTime;

    public BulletsPool BulletsPool => _bulletsPool;
    public Bullet Bullet => _bullet;

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (_afterShootTime <= 0)
                {
                    _ray.Show();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _ray.Hide();
                Shoot();
            }

            if (_afterShootTime > 0)
                _afterShootTime -= Time.deltaTime;
        }
    }
    
    private void Shoot()
    {
        _rotateAngle = GetComponent<HeroMover>().RotateAngle;

        if (_afterShootTime <= 0)
        {
            _shootSound.Play();
            _bulletsPool.InitializeShoot(Quaternion.Euler(0, 0, -Mathf.Clamp(_rotateAngle, -60, 60)), _shootPoint.position);
            _afterShootTime = _cooldown;
        }
    }
}
