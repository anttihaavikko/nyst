using UnityEngine;

public class ComputerClearer : Toggleable
{
    [SerializeField] private Computer computer;

    public override void ToggleTo(bool state)
    {
        computer.Clear();
    }
}