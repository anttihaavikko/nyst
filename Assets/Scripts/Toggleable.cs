using AnttiStarterKit.Animations;
using UnityEngine;

public class Toggleable : Activatable
{
    [SerializeField] private Transform normal, toggled;

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

    public void Toggle()
    {
        ToggleTo(!_state);
    }

    public virtual void ToggleTo(bool state)
    {
        _state = state;
        Tweener.MoveToBounceOut(normal, _state ? toggled.position : _originalPosition, 0.5f);
        Tweener.ScaleToBounceOut(normal, _state ? toggled.localScale : _originalScale, 0.5f);
        Tweener.RotateToBounceOut(normal, _state ? toggled.rotation : _originalRotation, 0.5f);
    }

    public override void Activate()
    {
        Toggle();
    }
}