using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class Collectable : Clickable
{
    [SerializeField] private CollectibleType type;
    
    public override void Click(Inventory inventory)
    {
        if (locked) return;
        
        gameObject.SetActive(false);
        if (type == CollectibleType.Battery) inventory.Batteries++;
        if (type == CollectibleType.Pearl) inventory.AddPearl(gameObject.GetComponent<Pearl>());
        if (type == CollectibleType.Key) inventory.Keys++;
        inventory.Add(type);
        inventory.UpdateCounts();
    }

    public void ToggleLock()
    {
        locked = !locked;
    }
}

public enum CollectibleType
{
    Pearl,
    Battery,
    Key,
    Compass,
    GemGreen,
    GemRed,
    GemBlue,
    GemYellow
}