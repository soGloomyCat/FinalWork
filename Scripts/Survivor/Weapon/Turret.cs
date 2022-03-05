using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private Transform _shootPoint;

    private float _timeAfterShoot;
    private float _rotateAngle;
    private float _rotateStep;

    private void Start()
    {
        _rotateAngle = transform.position.x < 0 ? 5f : -5;
        StartCoroutine(RotateTurret());
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator RotateTurret()
    {
        WaitForSeconds timer = new WaitForSeconds(.1f);
        _rotateStep = _rotateAngle;

        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(_shootPoint.position, transform.up);

            transform.rotation = Quaternion.Euler(0, 0, _rotateAngle);
            _rotateAngle += _rotateStep;

            if (_rotateAngle <= -35 || _rotateAngle >= 35)
                _rotateStep = -_rotateStep;

            if (hit.collider.gameObject.TryGetComponent(out Zombie zombie))
            {
                if (_timeAfterShoot <= 0 && hit.distance <= 3.5f)
                {
                    _timeAfterShoot = _cooldown;
                    _bulletsPool.InitializeShoot(transform.rotation, _shootPoint.position);
                }
            }

            _timeAfterShoot -= Time.deltaTime;
            yield return timer;
        }
    }
}
