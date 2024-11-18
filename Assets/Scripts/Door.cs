using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class Door : Clickable
{
    private Activatable _toggleable;

    private void Start()
    {
        _toggleable = GetComponent<Activatable>();
    }

    public override void Click(Inventory inventory)
    {
        if (locked) return;
        _toggleable?.Activate();
    }
}