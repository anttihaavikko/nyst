using System;
using System.Collections.Generic;
using AnttiStarterKit.Extensions;
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
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private GameObject uiCanvas;

    private float _sampleCooldown;
    
    private void Awake()
    {
        toggles[0].OnToggle += state => input.InvertY = state;
        toggles[1].OnToggle += state => input.InvertX = state;
    }

    private void Start()
    {
        var cam = Camera.main!.transform;
        if (PlayerPrefs.HasKey("NystQuality"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("NystQuality"));
        }

        UpdateQualityName();

        sliders[0].value = AudioManager.Instance.MusicVolume;
        sliders[1].value = AudioManager.Instance.SoundVolume;
        sliders[2].value = firstPersonController.RotationSpeed / 10f;
        
        sliders[0].onValueChanged.AddListener(vol => AudioManager.Instance.ChangeMusicVolume(vol));
        sliders[1].onValueChanged.AddListener(vol =>
        {
            AudioManager.Instance.ChangeSoundVolume(vol);
            if (!(_sampleCooldown <= 0)) return;
            sample.Play(cam.position);
            _sampleCooldown = 0.3f;
        });
        sliders[2].onValueChanged.AddListener(val => firstPersonController.RotationSpeed = val * 10f);
    }

    private void Update()
    {
        _sampleCooldown -= Time.deltaTime;
    }

    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel(true);
        UpdateQualityName();
    }
    
    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel(true);
        UpdateQualityName();
    }

    private void UpdateQualityName()
    {
        PlayerPrefs.SetInt("NystQuality", QualitySettings.GetQualityLevel());
        qualityDisplay.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }
}