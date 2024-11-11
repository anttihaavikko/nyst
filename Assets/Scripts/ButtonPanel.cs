using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Utils;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Activator secondary;

    private Activator _activator;
    private PushButton[] _buttons;

    private bool _done;

    private void Start()
    {
        _activator = GetComponent<Activator>();
        _buttons = GetComponentsInChildren<PushButton>();
    }

    public void Check()
    {
        if (_done && secondary && _buttons.All(b => b.IsInverted))
        {
            secondary.Activate();
            secondary = null;
        }
        
        if (_done || !_buttons.All(b => b.IsOk)) return;
        const float duration = 0.6f;
        _done = true;
        Tweener.ScaleToBounceOut(transform, Vector3.one * 0.8f, duration);
        Tweener.RotateToBounceOut(transform, Quaternion.Euler(rotation), duration);
        
        _activator?.Activate();
    }
}