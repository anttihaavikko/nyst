using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HintBoard : MonoBehaviour
{
    [SerializeField] private List<Character> characters;
    [SerializeField] private Letters letters;
    [SerializeField] private List<Image> images;
    [SerializeField] private Sprite halfImage, fullImage;
    [SerializeField] private List<BatteryBox> batteryBoxes;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Computer computer;
    // [SerializeField] private List<GameObject> checks;
    [SerializeField] private Color green;

    private List<int> _counts;
    private int _prevCount;
    private List<int> _indices;
    private List<bool> _inverted;
    private List<bool> _corrects;

    private void Start()
    {
        _inverted = images.Select(_ => Random.value < 0.5f).ToList();
        _corrects = images.Select(_ => false).ToList();
        _indices = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 }.RandomOrder().ToList();
        _counts = images.Select(_ => 0).ToList();
        batteryBoxes.ForEach(b => b.Toggled += UpdateScreen);
        computer.PasswordInput += CheckPassword;
    }

    private void CheckPassword()
    {
        for (var i = 0; i < 6; i++)
        {
            _corrects[i] = computer.HasCorrect(i * 2);
        }
        UpdateScreen();
    }

    public void UpdatePearls()
    {
        if (inventory.Pearls <= _prevCount) return;

        var index = inventory.Pearls - 1;
        if (index >= _indices.Count) return;
        _counts[_indices[index]]++;
        _prevCount = inventory.Pearls;

        UpdateScreen();
    }

    private void UpdateScreen()
    {
        for(var i = 0; i < images.Count; i++)
        {
            var powered = batteryBoxes[Mathf.FloorToInt(i * 0.5f)].IsPowered;
            var ringColor = _corrects[i] ? green : Color.white;
            images[i].color = powered && _counts[i] > 0 ? ringColor : Color.clear;
            images[i].sprite = powered && _counts[i] > 1 ? fullImage : halfImage;
            characters[i].gameObject.SetActive(_counts[i] > 0 && powered);
            if (_counts[i] <= 0 || !powered) continue;
            characters[i].Setup(letters.GetList(GetPasswordPart(i)), Color.white);
        }
    }

    private string GetPasswordPart(int index)
    {
        var pass = computer.Password;
        if (_counts[index] > 1) return pass.Substring(index * 2, 2);
        return _inverted[index] ?
            $" {pass.Substring(index * 2 + 1, 1)}" :
            $"{pass.Substring(index * 2, 1)} ";
    }
}