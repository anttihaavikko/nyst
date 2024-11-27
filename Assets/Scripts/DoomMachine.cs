using System;
using System.Collections.Generic;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Managers;
using AnttiStarterKit.Utils;
using Cinemachine;
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
    [SerializeField] private MusicChanger musicChanger;
    [SerializeField] private Material waterMaterial, lavaMaterial;
    [SerializeField] private MeshRenderer water;
    [SerializeField] private CinemachineImpulseSource impulseSource;

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
        // AudioManager.Instance.ChangeMusic(_state ? 1 : 0);
        musicChanger.Toggle();
        water.material = _state ? lavaMaterial : waterMaterial;
        impulseSource.GenerateImpulse(0.2f);
    }
}