using UnityEngine;

public class Lever : Clickable
{
    private Activator _activator;

    private void Start()
    {
        _activator = GetComponent<Activator>();
    }

    public override void Click(Inventory inventory)
    {
        if (locked) return;
        _activator.Activate();
    }
}