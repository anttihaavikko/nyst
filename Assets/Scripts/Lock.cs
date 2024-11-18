using System.Collections.Generic;
using UnityEngine;

public class Lock : Clickable
{
    [SerializeField] private List<Activatable> toggleable;
    [SerializeField] private Clickable opens;

    public override void Click(Inventory inventory)
    {
        if (IsLocked(inventory)) return;
        toggleable.ForEach(t => t.Activate());
        opens.OpenLock();
        enabled = false;
        if (locked)
        {
            inventory.Keys--;
            inventory.UpdateCounts();
        }
    }
    
    public override bool IsLocked(Inventory inventory)
    {
        return locked && inventory.Keys <= 0;
    }
}