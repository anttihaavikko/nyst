using UnityEngine;

public class ComponentToggler : Activatable
{
    [SerializeField] private Behaviour component;
    
    public override void Activate()
    {
        component.enabled = !component.enabled;
    }
}