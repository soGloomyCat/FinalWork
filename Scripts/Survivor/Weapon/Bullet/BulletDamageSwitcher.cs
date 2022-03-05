using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageSwitcher : MonoBehaviour
{
    [SerializeField] private float _damage;

    public float Damage => _damage;

    public void IncreaseDamage(float multiplier)
    {
        _damage *= multiplier;
    }
}
