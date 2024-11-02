using UnityEngine;
using UnityEngine.Serialization;

public abstract class Clickable : MonoBehaviour
{
    [SerializeField] private Outline outline;
    public Color buttonOffColor = new(0.5f, 0.5f, 0.5f);

    public void ToggleOutline(bool state)
    {
        Debug.Log($"Toggle {gameObject.name} to {state}");
        if (outline)
        {
            outline.enabled = state;   
        }
    }

    public abstract void Click();
}