using System;
using AnttiStarterKit.Extensions;
using StarterAssets;
using UnityEngine;

public class StartView : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private GameObject cursor;

    private bool _started;

    private void Start()
    {
        firstPersonController.Locked = true;
        cursor.SetActive(false);
    }

    private void Update()
    {
        if (_started || !Input.anyKeyDown) return;
        cam.SetActive(false);
        _started = true;
        cursor.SetActive(true);
        firstPersonController.Locked = false;
    }
}