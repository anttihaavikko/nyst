using System;
using System.Collections.Generic;
using AnttiStarterKit.Managers;
using AnttiStarterKit.ScriptableObjects;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private TMP_Text qualityDisplay;
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private List<BoolToggle> toggles;
    [SerializeField] private List<Slider> sliders;
    [SerializeField] private SoundComposition sample;

    private float _sampleCooldown;
    
    private void Awake()
    {
        toggles[0].OnToggle += state => input.InvertY = state;
        toggles[1].OnToggle += state => input.InvertX = state;
    }

    private void Start()
    {
        var cam = Camera.main!.transform;
        UpdateQualityName();

        sliders[0].value = AudioManager.Instance.MusicVolume;
        sliders[1].value = AudioManager.Instance.SoundVolume;
        
        sliders[0].onValueChanged.AddListener(vol => AudioManager.Instance.ChangeMusicVolume(vol));
        sliders[1].onValueChanged.AddListener(vol =>
        {
            AudioManager.Instance.ChangeSoundVolume(vol);
            if (!(_sampleCooldown <= 0)) return;
            sample.Play(cam.position);
            _sampleCooldown = 0.3f;
        });
    }

    private void Update()
    {
        _sampleCooldown -= Time.deltaTime;
    }

    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
        UpdateQualityName();
    }
    
    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
        UpdateQualityName();
    }

    private void UpdateQualityName()
    {
        qualityDisplay.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }
}