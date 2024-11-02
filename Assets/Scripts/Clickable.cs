using UnityEngine;
using UnityEngine.Serialization;

public abstract class Clickable : MonoBehaviour
{
    public Color buttonOffColor = new(0.5f, 0.5f, 0.5f);
    
    public abstract void Click();
}