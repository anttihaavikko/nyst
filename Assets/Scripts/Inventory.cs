using System;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.Utils;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text pearls, keys, batteries;
    [SerializeField] private GameObject ui;

    private bool _visible;
    
    public int Keys { get; set; }
    public int Pearls { get; set; }
    public int Batteries { get; set; }

    private void ShowCounts()
    {
        _visible = true;
        this.StartCoroutine(() => _visible = false, 1f);
    }

    private void Update()
    {
        ui.SetActive(_visible || Input.GetMouseButton(1));

        if (DevKey.Down(KeyCode.Q))
        {
            Pearls++;
            UpdateCounts();
        }
    }

    public void UpdateCounts()
    {
        keys.text = Keys.ToString();
        pearls.text = Pearls.ToString();
        batteries.text = Batteries.ToString();
        ShowCounts();
    }
}