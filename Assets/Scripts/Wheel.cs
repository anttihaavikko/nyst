using System;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wheel : MonoBehaviour
{
    [SerializeField] private AudioSource spinSound;
    [SerializeField] private SoundComposition stopSound;
    
    public int Value { get; private set; }

    private float _phase;
    private float _duration;
    private float _start;
    private float _target;
    private float _offset;

    private void Start()
    {
        _offset = transform.localRotation.eulerAngles.z;
    }

    public void Spin(float duration)
    {
        _phase = 1f;
        _duration = duration;
        Value = Random.Range(0, 6);
        _start = transform.localRotation.eulerAngles.z;
        _target = _offset - 360 * Mathf.CeilToInt(_duration) - Value * 60;
        spinSound.Play();
    }

    private void Update()
    {
        if (_phase <= 0) return;
        _phase -= Time.deltaTime / _duration;

        if (_phase <= 0)
        {
            spinSound.Stop();
            stopSound.Play(transform.position);
        }
        
        var angle = Mathf.Lerp(_target, _start, _phase);
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.WhereZ(angle));
    }
}