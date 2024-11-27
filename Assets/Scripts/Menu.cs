using System;
using System.Collections.Generic;
using AnttiStarterKit.Extensions;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private List<MenuOption> options;
    [SerializeField] private HackMenu hackMenu;
    
    private int _current;

    private void Start()
    {
        options.ForEach(o => o.gameObject.SetActive(false));
    }

    public void ChangeOption(int dir)
    {
        options[_current].gameObject.SetActive(false);
        _current = (_current + dir).LoopAround(0, options.Count);
        if (options[_current].IsLocked)
        {
            ChangeOption(dir);
            return;
        }
        Toggle(true);
    }

    public void Act()
    {
        options[_current]?.Act();
    }

    public void Toggle(bool state)
    {
        if(_current == 1) hackMenu.Find();
        options[_current].gameObject.SetActive(state);
    }
}