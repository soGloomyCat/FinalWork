using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private List<Sprite> _bulletSprites;

    private SpriteRenderer _spriteRenderer;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActivatedBullet(Quaternion rotation, Vector3 spawnPosition)
    {
        gameObject.SetActive(true);
        transform.rotation = rotation;
        transform.position = spawnPosition;
        _coroutine = StartCoroutine(BulletFlight());
    }

    public void IncreaseDamage(float multiplier)
    {
        _damage *= multiplier;
    }

    public void ChangeSprite()
    {
        _spriteRenderer.sprite = _bulletSprites[0];
        _bulletSprites.RemoveAt(0);
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
        StopCoroutine(_coroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Zombie>(out Zombie zombie))
        {
            DisableBullet();
            zombie.TakeDamage(_damage);
        }

        if (collision.TryGetComponent<InvisableWall>(out InvisableWall wall))
        {
            DisableBullet();
        }
    }

    private IEnumerator BulletFlight()
    {
        while (true)
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
            yield return null;
        }
    }
}
