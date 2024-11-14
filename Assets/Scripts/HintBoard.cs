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

    private List<int> _counts;
    private int _prevCount;
    private List<int> _indices;
    private List<bool> _inverted;

    private const string Password = "jormungandr!";

    private void Start()
    {
        _inverted = images.Select(_ => Random.value < 0.5f).ToList();
        _indices = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 }.RandomOrder().ToList();
        _counts = images.Select(_ => 0).ToList();
    }

    public void UpdatePearls(Inventory inventory)
    {
        if (inventory.Pearls <= _prevCount) return;

        var index = inventory.Pearls - 1;
        if (index >= _indices.Count) return;
        _counts[_indices[index]]++;
        _prevCount = inventory.Pearls;
        
        for(var i = 0; i < images.Count; i++)
        {
            images[i].color = _counts[i] > 0 ? Color.black : Color.white;
            images[i].sprite = _counts[i] > 1 ? fullImage : halfImage;
            if (_counts[i] <= 0) continue;
            characters[i].gameObject.SetActive(true);
            characters[i].Setup(letters.GetList(GetPasswordPart(i)), Color.black);
        }
    }

    private string GetPasswordPart(int index)
    {
        if (_counts[index] > 1) return Password.Substring(index * 2, 2);
        return _inverted[index] ?
            $" {Password.Substring(index * 2 + 1, 1)}" :
            $"{Password.Substring(index * 2, 1)} ";
    }
}