using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BoolToggle : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private bool state;
    [SerializeField] private TMP_Text field;
    [SerializeField] private GameObject toggles;
    [SerializeField] private TMP_Text togglesText;

    private string _label;
    
    public Action<bool> OnToggle { get; set; }

    private void Start()
    {
        _label = field.text.Split(":").FirstOrDefault();
        state = PlayerPrefs.GetInt(key, state ? 1 : 0) == 1;
        UpdateState();
    }

    public void Toggle()
    {
        state = !state;
        PlayerPrefs.SetInt(key, state ? 1 : 0);
        UpdateState();
    }

    private void UpdateState()
    {
        if(toggles) toggles.SetActive(state);
        if(togglesText) togglesText.enabled = state;
        OnToggle?.Invoke(state);
        field.text = $"{_label}: {(state ? "ON" : "OFF")}";
    }
}