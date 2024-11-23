using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private TMP_Text qualityDisplay;

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
    
    private void Start()
    {
        UpdateQualityName();
    }
}