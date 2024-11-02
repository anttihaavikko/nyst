using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Utils;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] private Toggleable toggleable;
    [SerializeField] private Vector3 rotation;
    
    private PanelButton[] _buttons;

    private bool _done;

    private void Start()
    {
        _buttons = GetComponentsInChildren<PanelButton>();
    }

    public void Check()
    {
        if (_done || !_buttons.All(b => b.IsOk)) return;
        const float duration = 0.6f;
        _done = true;
        Tweener.ScaleToBounceOut(transform, Vector3.one * 0.8f, duration);
        Tweener.RotateToBounceOut(transform, Quaternion.Euler(rotation), duration);
        
        toggleable?.Toggle();
    }
}