using UnityEngine;

public class Combiner : Activatable
{
    [SerializeField] private int targetCount;
    
    private Activator _activator;
    private int _count;

    private void Start()
    {
        _activator = GetComponent<Activator>();
    }

    public override void Activate()
    {
        _count++;
        if(_count >= targetCount) _activator.Activate();
    }
}