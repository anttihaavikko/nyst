using AnttiStarterKit.Extensions;
using UnityEngine;

public class Lever : Clickable
{
    [SerializeField] private float resetAfter = -1;
    
    private Activator _activator;
    private bool _state;

    private void Start()
    {
        _activator = GetComponent<Activator>();
    }

    public override void Click(Inventory inventory)
    {
        if (locked) return;
        _state = !_state;
        _activator.Activate();

        if (_state && resetAfter > 0)
        {
            this.StartCoroutine(() =>
            {
                _state = !_state;
                _activator.Activate();
            }, resetAfter);
        }
    }

    public override void Nudge()
    {
        _activator.Nudge();
    }
}