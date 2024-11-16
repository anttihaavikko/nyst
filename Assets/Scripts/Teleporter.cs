using AnttiStarterKit.Extensions;
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
        this.StartCoroutine(() => controller.enabled = true, 0.1f);
    }
}