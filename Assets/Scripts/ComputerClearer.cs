using UnityEngine;

public class ComputerClearer : Activatable
{
    [SerializeField] private Computer computer;

    public override void Activate()
    {
        computer.Clear();
    }
}