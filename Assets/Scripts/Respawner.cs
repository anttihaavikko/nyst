using System;
using StarterAssets;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private CharacterController controller;
    
    private Vector3 _respawn;

    private void Start()
    {
        SaveSpot();
        firstPersonController.Jumped += SaveSpot;
    }

    private void SaveSpot()
    {
        _respawn = controller.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kill"))
        {
            controller.enabled = false;
            controller.transform.position = _respawn;
            controller.enabled = true;
        }
    }
}