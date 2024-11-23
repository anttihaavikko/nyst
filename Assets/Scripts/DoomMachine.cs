using System;
using System.Collections.Generic;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoomMachine : Activatable
{
    [SerializeField] private GameObject lava;
    [SerializeField] private List<Toggleable> toggleables;
    [SerializeField] private List<Collectable> gems;
    [SerializeField] private Skybox skybox;
    [SerializeField] private Material normalSky, doomSky;
    [SerializeField] private Light sun;
    [SerializeField] private Color doomColor;
    [SerializeField] private Transform sunWrap;
    [SerializeField] private GameObject doomVolume;

    private bool _state;
    private float _transition;

    private void Update()
    {
        _transition = Mathf.MoveTowards(_transition, _state ? 1f : 0f, Time.deltaTime * 2f);
        sun.color = Color.Lerp(Color.white, doomColor, TweenEasings.QuadraticEaseInOut(_transition));

        if (DevKey.Down(KeyCode.T))
        {
            Activate();
        }
    }

    public override void Activate()
    {
        _state = !_state;
        lava.SetActive(_state);
        doomVolume.SetActive(_state);
        toggleables.ForEach(t => t.Activate());
        gems.ForEach(g => g.ToggleLock());
        skybox.material = _state ? doomSky : normalSky;
        Tweener.RotateToQuad(sunWrap, Quaternion.Euler(0, Random.value * 360, 0), 0.5f);
    }
}