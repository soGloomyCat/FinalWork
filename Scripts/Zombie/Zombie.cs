using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class Zombie : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private int _reward;

    private Animator _animator;
    private ParticleSystem _deadFog;
    private const string Damage = "Damage";
    private const string Die = "Die";

    public int Reward => _reward;
    public Base Target { get; private set; }

    public event UnityAction<Zombie> Dying;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _deadFog = GetComponent<ParticleSystem>();
    }

    private void OnDisable()
    {
        Target.Destroyed -= Deactivated;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _animator.SetTrigger(Damage);

        if (_health <= 0)
        {
            _animator.SetTrigger(Die);
            _deadFog.Play();
            Invoke(nameof(Hide), 0.4f);
            Dying?.Invoke(this);
        }
    }

    public void InizializeParameters(Base target, float multiplier)
    {
        InitTarget(target);
        IncreaseHealth(multiplier);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void InitTarget(Base target)
    {
        Target = target;
        Target.Destroyed += Deactivated;
    }

    private void IncreaseHealth(float multiplier)
    {
        _health *= multiplier;
    }

    private void Deactivated()
    {
        gameObject.SetActive(false);
    }
}
