using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class Collectable : Clickable
{
    [SerializeField] private CollectibleType type;
    [SerializeField] private SoundComposition lockSound;
    
    public override void Click(Inventory inventory)
    {
        if (locked)
        {
            lockSound?.Play(transform.position);
            return;
        }
        
        gameObject.SetActive(false);
        if (type == CollectibleType.Battery) inventory.Batteries++;
        if (type == CollectibleType.Pearl) inventory.AddPearl(gameObject.GetComponent<Pearl>());
        if (type == CollectibleType.Key) inventory.Keys++;
        if (type == CollectibleType.Compass) inventory.Compass = true;
        inventory.UpdateCounts();
    }
    
    public override bool CanInteract(Inventory inventory)
    {
        return true;
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