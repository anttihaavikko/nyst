using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.Utils;
using StarterAssets;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform safeSpot;
    [SerializeField] private Transform spot;
    
    private readonly List<Vector3> _respawns = new();
    private bool _waiting;

    private void Start()
    {
        SaveSpot();
        firstPersonController.Jumped += SaveSpot;
    }

    private void Update()
    {
        if (DevKey.Down(KeyCode.C)) Teleport(spot.position);
    }

    private void SaveSpot()
    {
        if (_waiting || (_respawns.Count > 0 && !firstPersonController.Grounded)) return;
        _respawns.Add(controller.transform.position);
        if (_respawns.Count > 20) _respawns.RemoveAt(0);
        _waiting = true;
        this.StartCoroutine(() => _waiting = false, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Kill")) return;
        other.GetComponent<SoundContainer>()?.Play(transform.position);
        Teleport(_respawns.Any() ? _respawns.Last() : safeSpot.position);
    }

    private void Teleport(Vector3 position)
    {
        var wasLocked = firstPersonController.Locked;
        SceneChanger.Instance.blinders.Close();
        firstPersonController.Locked = true;
        
        this.StartCoroutine(() =>
        {
            controller.enabled = false;
            controller.transform.position = position;
            if(_respawns.Any()) _respawns.RemoveAt(_respawns.Count - 1);
            controller.enabled = true;
            SceneChanger.Instance.blinders.Open();
            firstPersonController.Locked = wasLocked;
        }, 0.6f);
    }
}