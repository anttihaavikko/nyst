using System.Collections.Generic;
using UnityEngine;

public class Lock : Clickable
{
    [SerializeField] private List<Activatable> toggleable;
    [SerializeField] private Clickable opens;

    public override void Click(Inventory inventory)
    {
        toggleable.ForEach(t => t.Activate());
        opens.OpenLock();
        enabled = false;
    }      
}