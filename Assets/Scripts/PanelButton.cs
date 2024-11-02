using AnttiStarterKit.Animations;
using AnttiStarterKit.Extensions;
using UnityEngine;

public class PanelButton : Clickable
{
    [SerializeField] private bool correct;
    
    private bool _state;
    private MeshRenderer _meshRenderer;
    private ButtonPanel _panel;
    private float _start;
    
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    public bool IsOk => _state == correct;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _panel = GetComponentInParent<ButtonPanel>();
        _start = transform.localPosition.x;
    }

    public override void Click()
    {
        _state = !_state;
        Tweener.MoveLocalTo(transform, transform.localPosition.WhereX(_state ? _start * 0.5f : _start), 0.2f);
        _meshRenderer.material.SetColor(BaseColor, GetColor());
        _panel.Check();
    }

    private Color GetColor()
    {
        if (!_state) return new Color(0.7f, 0.7f, 0.7f);
        return correct ? Color.white : Color.black;
    }
}