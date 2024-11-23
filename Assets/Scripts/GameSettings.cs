using System;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private TMP_Text qualityDisplay;
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private List<BoolToggle> toggles;

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

    private void Awake()
    {
        toggles[0].OnToggle += state => input.InvertY = state;
        toggles[1].OnToggle += state => input.InvertX = state;
    }

    private void Start()
    {
        UpdateQualityName();
    }
}