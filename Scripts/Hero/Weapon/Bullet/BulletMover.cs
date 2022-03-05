using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Bullet _bullet;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _bullet = GetComponent<Bullet>();
        _coroutine = StartCoroutine(Flight());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletDamageSwitcher bulletDamageSwitcher;

        StopCoroutine(_coroutine);
        bulletDamageSwitcher = _bullet.GetComponent<BulletDamageSwitcher>();

        if (collision.TryGetComponent<Zombie>(out Zombie zombie))
        {
            _bullet.Disable();
            zombie.TakeDamage(bulletDamageSwitcher.Damage);
        }
        else if (collision.TryGetComponent<InvisableWall>(out InvisableWall wall))
        {
            _bullet.Disable();
        }
    }

    private IEnumerator Flight()
    {
        while (true)
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
            yield return null;
        }
    }
}
