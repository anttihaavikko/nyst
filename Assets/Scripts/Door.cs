using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class Door : Clickable
{
    [SerializeField] private bool nudges;
    
    private Activatable _toggleable;

    private void Start()
    {
        _toggleable = GetComponent<Activatable>();
    }

    public override void Click(Inventory inventory)
    {
        if (locked)
        {
            Nudge();
            return;
        }
        _toggleable?.Activate();
    }

    public override void Nudge()
    {
        if (nudges) (_toggleable as Toggleable)?.Nudge();
    }
}