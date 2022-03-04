using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public abstract class StateBase : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private TransitionBase _transition;
    [SerializeField] private AnimationClip _clip;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _smoke;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _smoke = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        if (_clip == null)
            return;

        _smoke.Play();
        _animator.Play(_clip.name);
        _spriteRenderer.sprite = _sprite;
    }

    public void EnterState()
    {
        enabled = true;

        if (_transition != null)
            _transition.enabled = true;
    }

    public StateBase GetNextState()
    {
        if (_transition != null)
        {
            if (_transition.NeedTransit)
                return _transition.TargetState;
        }

        return null;
    }

    public void DisableState()
    {
        if (enabled == true)
            _transition.enabled = false;

        enabled = false;
    }

    public void InitBaseHealth(Base targetBase)
    {
        if (_transition != null)
            _transition.InitBaseHealth(targetBase);
    }

}
