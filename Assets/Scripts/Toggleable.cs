using System;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class Toggleable : Activatable
{
    [SerializeField] private Transform normal, toggled;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private bool bounces = true;
    [SerializeField] private SoundComposition sound;
    [SerializeField] private float nudgeAmount = 0.1f;

    private bool _state;
    private Vector3 _originalPosition, _originalScale;
    private Quaternion _originalRotation;
    
    private void Start()
    {
        if (!normal) return;
        _originalPosition = normal.position;
        _originalScale = normal.localScale;
        _originalRotation = normal.rotation;
    }

    private void Toggle()
    {
        ToggleTo(!_state);
    }

    private void ToggleTo(bool state, float durationMod = 1f)
    {
        sound?.Play(transform.position);
        _state = state;
        var easeFunc = GetEase();
        Tweener.MoveTo(normal, _state ? toggled.position : _originalPosition, duration * durationMod, easeFunc);
        Tweener.ScaleTo(normal, _state ? toggled.localScale : _originalScale, duration * durationMod, easeFunc);
        Tweener.Instance.RotateTo(normal, _state ? toggled.rotation : _originalRotation, duration * durationMod, 0, easeFunc);
    }

    public void Nudge()
    {
        Tweener.MoveTo(normal, Vector3.Lerp(_originalPosition, toggled.position, nudgeAmount), 0.15f, TweenEasings.BounceEaseOut);
        Tweener.ScaleTo(normal, Vector3.Lerp(_originalScale, toggled.localScale, nudgeAmount), 0.15f, TweenEasings.BounceEaseOut);
        Tweener.Instance.RotateTo(normal, Quaternion.Lerp(_originalRotation, toggled.rotation, nudgeAmount), 0.15f, 0, TweenEasings.BounceEaseOut);
        this.StartCoroutine(() => ToggleTo(false, 0.5f), 0.15f);
    }
    

    private Func<float, float> GetEase()
    {
        return bounces ? TweenEasings.BounceEaseOut : TweenEasings.QuadraticEaseInOut;
    }

    public override void Activate()
    {
        Toggle();
    }
}