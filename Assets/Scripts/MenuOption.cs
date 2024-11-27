using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private bool locked;

    public bool IsLocked => locked;

    public void Unlock()
    {
        locked = false;
    }

    public void Act()
    {
        button.onClick?.Invoke();
    }

    public void Quit()
    {
        Debug.Log("Quit...");
        Application.Quit();
    }
}