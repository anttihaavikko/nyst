using System;
using System.Collections.Generic;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.Utils;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text pearls, keys, batteries;
    [SerializeField] private GameObject ui;
    [SerializeField] private HintBoard hintBoard;

    private bool _visible;
    private readonly Stack<Material> _pearlMaterials = new(); 
    
    public int Keys { get; set; }
    public int Pearls { get; private set; }
    public int Batteries { get; set; }
    public bool Compass { get; set; }

    public Material PearlMaterial => _pearlMaterials.Peek();

    private void ShowCounts()
    {
        _visible = true;
        this.StartCoroutine(() => _visible = false, 1f);
    }

    private void Update()
    {
        ui.SetActive(_visible || Input.GetMouseButton(1));
        
        if (DevKey.Down(KeyCode.K))
        {
            Keys++;
            UpdateCounts();
        }

        if (DevKey.Down(KeyCode.Q))
        {
            Pearls++;
            UpdateCounts();
        }
    }

    public void AddPearl(Pearl pearl)
    {
        Pearls++;
        _pearlMaterials.Push(pearl.Mesh.material);
        UpdateCounts();
    }

    public void UpdateCounts()
    {
        keys.text = Keys.ToString();
        pearls.text = Pearls.ToString();
        batteries.text = Batteries.ToString();
        ShowCounts();
        hintBoard.UpdatePearls(this);
    }

    public void RemovePearl()
    {
        _pearlMaterials.Pop();
        Pearls--;
    }
}