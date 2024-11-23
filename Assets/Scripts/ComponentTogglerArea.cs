using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class ComponentTogglerArea : MonoBehaviour
{
    [SerializeField] private List<Behaviour> components;
    [SerializeField] private bool defaultState;
    [SerializeField] private SoundComposition sound;

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"Entered: {other.gameObject.name}");
        if (other.CompareTag("Player") && components.Any())
        {
            sound?.Play(components[0].transform.position);
            components.ForEach(c => c.enabled = !defaultState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && components.Any())
        {
            sound?.Play(components[0].transform.position);
            components.ForEach(c => c.enabled = defaultState);
        }
    }
}