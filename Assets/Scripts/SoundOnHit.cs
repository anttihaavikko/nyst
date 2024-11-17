using System;
using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class SoundOnHit : MonoBehaviour
{
    [SerializeField] private float threshold = 1f;
    [SerializeField] private SoundComposition sound;
    [SerializeField] private float volume = 1f;
    [SerializeField] private float delay = 0.2f;
    
    private float _cooldown;

    private void OnCollisionEnter(Collision other)
    {
        if (!(other.relativeVelocity.magnitude > threshold) || !(_cooldown <= 0)) return;
        
        sound?.Play(transform.position, volume);
        _cooldown = delay;
    }

    private void Update()
    {
        _cooldown -= Time.deltaTime;
    }
}