using StarterAssets;
using UnityEngine;

public class Teleporter : Activatable
{
    [SerializeField] private Transform target;
    [SerializeField] private FirstPersonController controller;
    
    public override void Activate()
    {
        controller.enabled = false;
        controller.transform.position = target.position;
        controller.enabled = true;
    }
}