using UnityEngine;

public class ComponentToggler : Activatable
{
    [SerializeField] private Behaviour component;
    [SerializeField] private GameObject target;
    
    public override void Activate()
    {
        if(component) component.enabled = !component.enabled;
        if(target) target.SetActive(!target.activeInHierarchy);
    }
}