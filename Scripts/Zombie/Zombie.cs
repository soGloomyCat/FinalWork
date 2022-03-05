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

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private ParticleSystem _deadFog;
    private const string Damage = "Damage";
    private const string Die = "Die";

    public int Reward => _reward;
    public Base Target { get; private set; }

    public event UnityAction<Zombie> Dying;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _deadFog = GetComponent<ParticleSystem>();
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

    public void InitTarget(Base target)
    {
        Target = target;
    }

    public void SetColor()
    {
        Color color;

        color = new Color(Random.Range(0.65f, 1.0f), Random.Range(0.65f, 1.0f), Random.Range(0.65f, 1.0f));
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = color;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void IncreaseHealth(float multiplier)
    {
        _health *= multiplier;
    }
}
