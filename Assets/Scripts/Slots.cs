using System.Collections.Generic;
using AnttiStarterKit.Animations;
using UnityEngine;

public class Slots : Clickable
{
    [SerializeField] private Toggleable lever;
    [SerializeField] private List<Transform> wheels;

    private bool _busy;
    
    public override void Click(Inventory inventory)
    {
        if (_busy) return;
        if (inventory.Coins == 0)
        {
            lever.Nudge();
            return;
        }

        _busy = true;
        inventory.Coins--;
        lever.Activate();
        Invoke(nameof(Reset), 1f);
    }

    private void Reset()
    {
        lever.Activate();
        _busy = false;
    }
}