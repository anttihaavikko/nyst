using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private int rowWidth;
    [TextArea][SerializeField] private string contents;
    [SerializeField] private bool centered;
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Letters letters;
    [SerializeField] private Character characterPrefab;
    [SerializeField] private RectTransform rowPrefab;

    private RectTransform _row;
    private int _rowWidth;
    
    private void Start()
    {
        AddRow();
        
        for (var i = 0; i < contents.Length; i += 2)
        {
            var first = contents.Substring(i, 1);
            var second = contents.Length > i + 1 ? contents.Substring(i + 1, 1) : "space";
            var firstChar = letters.Get(first);
            var secondChar = letters.Get(second);
            var width = Mathf.Max(firstChar.offset + firstChar.width, secondChar.offset + secondChar.width);
            var special = GetSpecial(first, second);
            
            if (_rowWidth + width > rowWidth) AddRow();
            _rowWidth += width;
            
            var character = Instantiate(characterPrefab, _row);
            character.Setup(firstChar, secondChar, letters.Get(special), color);
        }
    }

    private void AddRow()
    {
        _rowWidth = 0;
        _row = Instantiate(rowPrefab, transform);
        _row.GetComponent<HorizontalLayoutGroup>().childAlignment = centered ? 
            TextAnchor.MiddleCenter : 
            TextAnchor.MiddleLeft;
    }

    private static string GetSpecial(string first, string second)
    {
        return string.CompareOrdinal(first, second) switch
        {
            < 0 => "first",
            > 0 => "second",
            _ => "same"
        };
    }
}