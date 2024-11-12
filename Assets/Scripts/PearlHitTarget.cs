using UnityEngine;

public class PearlHitTarget : PearlTarget
{
    private Activator _activator;

    private void Start()
    {
        _activator = GetComponent<Activator>();
    }

    public override void Hit(Vector3 dir, Vector3 point)
    {
        _activator.Activate();
    }
}