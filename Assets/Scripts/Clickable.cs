using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Clickable : MonoBehaviour
{
    [SerializeField] private string screenName;
    [SerializeField] private Outline outline;
    [SerializeField] private List<Outline> extraOutlines;
    [SerializeField] private float pointDelay;
    [SerializeField] protected bool locked;
    [SerializeField] private List<BatteryBox> poweredBy;
    [SerializeField] private int clickEffect;
    [SerializeField] protected SoundComposition sound;
    [SerializeField] protected SoundComposition lockSound;
    
    public Color buttonOffColor = new(0.5f, 0.5f, 0.5f);

    public float PointDelay => pointDelay;
    public int ClickEffect => clickEffect;

    public string ScreenName => screenName;

    public bool IsPowered => poweredBy.Count == 0 || poweredBy.All(b => b.IsPowered);

    public void ToggleOutline(bool state)
    {
        // Debug.Log($"Toggle {gameObject.name} to {state}");
        if (outline)
        {
            outline.enabled = state;   
        }
        
        extraOutlines.ForEach(o => o.enabled = state);
    }

    public void PlaySound(Inventory inventory)
    {
        if (IsLocked(inventory))
        {
            lockSound?.Play(transform.position);
            return;
        }
        sound?.Play(transform.position);
    }

    public abstract void Click(Inventory inventory);

    public virtual bool CanInteract(Inventory inventory)
    {
        return true;
    }
    
    public virtual bool IsLocked(Inventory inventory)
    {
        return locked;
    }

    public void OpenLock()
    {
        locked = false;
    }

    public virtual void Nudge()
    {
    }
}