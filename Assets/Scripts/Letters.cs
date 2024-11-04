using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LetterSet", menuName = "LetterSet", order = 0)]
public class Letters : ScriptableObject
{
    [SerializeField] private List<LetterDefinition> sprites;

    private Dictionary<string, Sprite> _dictionary;

    public LetterDefinition Get(string letter)
    {
        if (letter == " ") return Get("space");
        var match = sprites.FirstOrDefault(s => s.key.ToLower() == letter.ToLower());
        return match ?? sprites.First();
    }
}

[Serializable]
public class LetterDefinition
{
    public string key;
    public Sprite sprite;
    public int offset = 0;
    public int width = 100;
}