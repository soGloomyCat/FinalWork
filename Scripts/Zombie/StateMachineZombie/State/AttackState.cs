using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateZombie
{
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldown;

    private float _lastAttackTime;
    private const string Attack = "Attack";

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            AttackTarget();
            _lastAttackTime = _cooldown;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void AttackTarget()
    {
        CurrentAnimator.SetTrigger(Attack);
        Target.TakeDamge(_damage);
    }
}
