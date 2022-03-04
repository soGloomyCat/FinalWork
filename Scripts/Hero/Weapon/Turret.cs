using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private Transform _shootPoint;

    private float _timeAfterShoot;
    private float _rotateAngle;
    private float _rotateStep;

    private void Start()
    {
        StartCoroutine(RotateTurret());
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    private void IdentifyPosition()
    {
        if (transform.position.x < 0)
            _rotateAngle = 5f;
        else
            _rotateAngle = -5f;
    }

    private IEnumerator RotateTurret()
    {
        WaitForSeconds timer = new WaitForSeconds(.1f);
        IdentifyPosition();
        _rotateStep = _rotateAngle;

        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(_shootPoint.position, transform.up);
            Debug.DrawRay(_shootPoint.position, transform.up * 3f, Color.red);

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
