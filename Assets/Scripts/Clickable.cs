using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Clickable : MonoBehaviour
{
    [SerializeField] private string screenName;
    [SerializeField] private Outline outline;
    [SerializeField] private float pointDelay;
    [SerializeField] protected bool locked;
    [SerializeField] private List<BatteryBox> poweredBy;
    
    public Color buttonOffColor = new(0.5f, 0.5f, 0.5f);

    public float PointDelay => pointDelay;

    public string ScreenName => screenName;

    public bool IsPowered => poweredBy.Count == 0 || poweredBy.All(b => b.IsPowered);

    public void ToggleOutline(bool state)
    {
        // Debug.Log($"Toggle {gameObject.name} to {state}");
        if (outline)
        {
            outline.enabled = state;   
        }
    }

    public abstract void Click(Inventory inventory);

    public virtual bool CanInteract(Inventory inventory)
    {
        return !locked;
    }

    public void OpenLock()
    {
        locked = false;
    }
}