using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.Animations;
using AnttiStarterKit.ScriptableObjects;
using AnttiStarterKit.Utils;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Activator secondary;
    [SerializeField] private SoundComposition sound;

    private Activator _activator;
    private PushButton[] _buttons;

    private bool _done;

    private void Start()
    {
        _activator = GetComponent<Activator>();
        _buttons = GetComponentsInChildren<PushButton>();
    }

    public virtual void Check(PushButton button)
    {
        if (_done && secondary && _buttons.All(b => b.IsInverted))
        {
            sound?.Play(transform.position);
            secondary.Activate();
            secondary = null;
        }
        
        if (_done || !_buttons.All(b => b.IsOk)) return;
        const float duration = 0.6f;
        _done = true;
        sound?.Play(transform.position);
        Tweener.ScaleToBounceOut(transform, Vector3.one * 0.8f, duration);
        Tweener.RotateToBounceOut(transform, Quaternion.Euler(rotation), duration);
        
        _activator?.Activate();
    }
}