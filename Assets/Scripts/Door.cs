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
        _toggleable?.Activate();
    }
}