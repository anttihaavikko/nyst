using System;
using StarterAssets;
using UnityEngine;

public class JumpDisabler : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;

    private void Toggle(Collider other, bool state)
    {
        if (!other.CompareTag("Player")) return;
        firstPersonController.CanJump = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        Toggle(other, false);
    }

    private void OnTriggerExit(Collider other)
    {
        Toggle(other, true);
    }
}