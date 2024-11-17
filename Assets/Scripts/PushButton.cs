using AnttiStarterKit.Animations;
using AnttiStarterKit.Extensions;
using UnityEngine;

public class PushButton : Clickable
{
    [SerializeField] private string letter;
    [SerializeField] private bool correct;
    [SerializeField] private bool blinks;
    [SerializeField] private float resetAfter = -1;
    
    private Activator _activator;
    
    private bool _state;
    private MeshRenderer _meshRenderer;
    private ButtonPanel _panel;
    private float _start;
    
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    public bool IsOk => _state == correct;
    public bool IsInverted => _state != correct;
    public bool GetState => _state;

    public string Letter => letter;

    private void Start()
    {
        _activator = GetComponent<Activator>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _panel = GetComponentInParent<ButtonPanel>();
        _start = transform.localPosition.x;
    }

    public override void Click(Inventory inventory)
    {
        CancelInvoke(nameof(Reset));
        CancelInvoke(nameof(Toggle));
        
        Toggle();

        if (blinks)
        {
            Invoke(nameof(Reset), 0.2f);
        }

        if (_state && resetAfter > 0)
        {
            Invoke(nameof(Toggle), resetAfter);
        }
    }

    private void Toggle()
    {
        _state = !_state;
        Tweener.MoveLocalTo(transform, transform.localPosition.WhereX(_state ? _start * 0.5f : _start), 0.2f);
        _meshRenderer.material.SetColor(BaseColor, GetColor());
        // _meshRenderer.material.EnableKeyword("_EMISSION");
        _panel?.Check(this);
        
        _activator?.Activate();
    }

    public void Reset()
    {
        _state = false;
        _meshRenderer.material.SetColor(BaseColor, buttonOffColor);
        Tweener.MoveLocalTo(transform, transform.localPosition.WhereX(_start), 0.2f);
    }

    private Color GetColor()
    {
        if (!_state) return buttonOffColor;
        return correct ? Color.white : Color.black;
    }
}