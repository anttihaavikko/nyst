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

    public List<LetterDefinition> GetList(string text)
    {
        var first = text.Substring(0, 1);
        var second = text.Length > 1 ? text.Substring(1, 1) : "space";
        var firstChar = Get(first);
        var secondChar = Get(second);
        var width = Mathf.Max(firstChar.offset + firstChar.width, secondChar.offset + secondChar.width);
        var special = Board.GetSpecial(first, second);
        return new List<LetterDefinition> { firstChar, secondChar, Get(special) };
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