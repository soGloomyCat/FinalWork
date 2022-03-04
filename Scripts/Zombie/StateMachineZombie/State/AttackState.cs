using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateZombie
{
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldown;

    private float _lastAttackTime;

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack();
            _lastAttackTime = _cooldown;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack()
    {
        CurrentAnimator.SetTrigger("Attack");
        Target.TakeDamge(_damage);
    }
}
