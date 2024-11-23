using System;
using System.Collections.Generic;
using AnttiStarterKit.Extensions;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private List<MenuOption> options;
    
    private int _current;

    private void Start()
    {
        options.ForEach(o => o.gameObject.SetActive(false));
    }

    public void ChangeOption(int dir)
    {
        options[_current].gameObject.SetActive(false);
        _current = (_current + dir).LoopAround(0, options.Count);
        options[_current].gameObject.SetActive(true);
    }

    public void Act()
    {
        options[_current]?.Act();
    }

    public void Toggle(bool state)
    {
        options[_current].gameObject.SetActive(state);
    }
}