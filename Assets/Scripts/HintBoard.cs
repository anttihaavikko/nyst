using System;
using System.Collections.Generic;
using UnityEngine;

public class HintBoard : MonoBehaviour
{
    [SerializeField] private List<Character> characters;
    [SerializeField] private Letters letters;

    private const string Password = "jormungandr!";

    private void Start()
    {
        for(var i = 0; i < characters.Count; i++)
        {
            characters[i].Setup(letters.GetList(Password.Substring(i * 2, 2)), Color.black); 
        }
    }
}