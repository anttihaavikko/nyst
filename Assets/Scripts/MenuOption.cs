using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    [SerializeField] private Button button;

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