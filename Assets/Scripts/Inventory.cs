using System;
using System.Collections.Generic;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.Utils;
using StarterAssets;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text pearls, keys, batteries;
    [SerializeField] private GameObject ui;
    [SerializeField] private HintBoard hintBoard;
    [SerializeField] private GameObject compass, gemRed, gemGreen, gemBlue, gemYellow;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private MenuOption cheatMenu;

    private bool _visible;
    private readonly Stack<Material> _pearlMaterials = new();
    private readonly List<CollectibleType> _collectibles = new();
    
    public int Keys { get; set; }
    public int Pearls { get; private set; }
    public int Batteries { get; set; }
    public int Coins { get; set; }
    public bool IsHacking { get; private set; }

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
        
        if (DevKey.Down(KeyCode.B))
        {
            Batteries++;
            UpdateCounts();
        }

        if (DevKey.Down(KeyCode.J))
        {
            firstPersonController.CanDoubleJump = true;
        }
        
        if (DevKey.Down(KeyCode.H))
        {
            IsHacking = true;
            cheatMenu.Unlock();
        }
        
        if (DevKey.Down(KeyCode.C))
        {
            Add(CollectibleType.Compass);
        }
        
        if (DevKey.Down(KeyCode.G))
        {
            Add(CollectibleType.GemGreen);
            Add(CollectibleType.GemRed);
            Add(CollectibleType.GemBlue);
            Add(CollectibleType.GemYellow);
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
        hintBoard.UpdatePearls();
        
        compass.SetActive(Has(CollectibleType.Compass));
        gemBlue.SetActive(Has(CollectibleType.GemBlue));
        gemGreen.SetActive(Has(CollectibleType.GemGreen));
        gemRed.SetActive(Has(CollectibleType.GemRed));
        gemYellow.SetActive(Has(CollectibleType.GemYellow));
    }

    public void RemovePearl()
    {
        _pearlMaterials.Pop();
        Pearls--;
    }

    public void Add(CollectibleType type)
    {
        if(!_collectibles.Contains(type)) _collectibles.Add(type);
        if (type == CollectibleType.Jetpack) firstPersonController.CanDoubleJump = true;
        if (type == CollectibleType.Router) cheatMenu.Unlock();
    }

    public bool Has(CollectibleType type)
    {
        return _collectibles.Contains(type);
    }

    public void Toggle(CollectibleType type)
    {
        if (_collectibles.Contains(type))
        {
            _collectibles.Remove(type);
            return;
        }
        _collectibles.Add(type);
    }
}