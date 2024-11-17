using System;
using AnttiStarterKit.Animations;
using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class Toggleable : Activatable
{
    [SerializeField] private Transform normal, toggled;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private bool bounces = true;
    [SerializeField] private SoundComposition sound;

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

    private void ToggleTo(bool state)
    {
        sound?.Play(transform.position);
        _state = state;
        var easeFunc = GetEase();
        Tweener.MoveTo(normal, _state ? toggled.position : _originalPosition, duration, easeFunc);
        Tweener.ScaleTo(normal, _state ? toggled.localScale : _originalScale, duration, easeFunc);
        Tweener.Instance.RotateTo(normal, _state ? toggled.rotation : _originalRotation, duration, 0, easeFunc);
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