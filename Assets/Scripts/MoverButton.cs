using AnttiStarterKit.Animations;
using AnttiStarterKit.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

public class MoverButton : Clickable
{
    [SerializeField] private Toggleable toggleable;

    private Vector3 _originalPosition, _originalScale;
    private Quaternion _originalRotation;
    private bool _state;
    private MeshRenderer _meshRenderer;
    private float _start;
    
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _start = transform.localPosition.x;
    }

    public override void Click()
    {
        _state = !_state;
        Tweener.MoveLocalTo(transform, transform.localPosition.WhereX(_state ? _start * 0.5f : _start), 0.2f);
        _meshRenderer.material.SetColor(BaseColor, GetColor());
        toggleable.ToggleTo(_state);
    }

    private Color GetColor()
    {
        return _state ? Color.white : buttonOffColor;
    }      
}