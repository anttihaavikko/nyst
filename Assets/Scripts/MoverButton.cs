using AnttiStarterKit.Animations;
using AnttiStarterKit.Extensions;
using UnityEngine;

public class MoverButton : Clickable
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform end;

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
        
        _originalPosition = target.position;
        _originalScale = target.localScale;
        _originalRotation = target.rotation;
    }

    public override void Click()
    {
        _state = !_state;
        Tweener.MoveLocalTo(transform, transform.localPosition.WhereX(_state ? _start * 0.5f : _start), 0.2f);
        _meshRenderer.material.SetColor(BaseColor, GetColor());
        Tweener.MoveToBounceOut(target, _state ? end.position : _originalPosition, 0.5f);
        Tweener.ScaleToBounceOut(target, _state ? end.localScale : _originalScale, 0.5f);
        Tweener.RotateToBounceOut(target, _state ? end.rotation : _originalRotation, 0.5f);
    }

    private Color GetColor()
    {
        return _state ? Color.white : buttonOffColor;
    }      
}