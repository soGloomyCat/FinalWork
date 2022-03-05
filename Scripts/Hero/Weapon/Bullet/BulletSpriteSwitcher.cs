using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BulletSpriteSwitcher : MonoBehaviour
{
    [SerializeField] private List<Sprite> _bulletSprites;

    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        _spriteRenderer.sprite = _bulletSprites[0];
        _bulletSprites.RemoveAt(0);
    }
}
