using System;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Managers;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] private AudioSource normal, doom;

    private bool _state;
    private float _phase;

    private void Update()
    {
        var vol = AudioManager.Instance.MusicVolume;
        _phase = Mathf.MoveTowards(_phase, _state ? 1 : 0, Time.deltaTime * 0.5f);
        var val = TweenEasings.QuadraticEaseInOut(_phase);
        normal.volume = (1 - val) * vol;
        doom.volume = val * vol;
        normal.pitch = Mathf.Lerp(1f, 1.4f, val);
        doom.pitch = Mathf.Lerp(1f / 1.4f, 1f, val);
    }

    public void Toggle()
    {
        _state = !_state;
    }
}