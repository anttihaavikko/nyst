using UnityEngine;

public class Collectable : Clickable
{
    [SerializeField] private CollectibleType type;
    
    public override void Click(Inventory inventory)
    {
        gameObject.SetActive(false);
        if (type == CollectibleType.Battery) inventory.Batteries++;
        if (type == CollectibleType.Pearl) inventory.Pearls++;
        if (type == CollectibleType.Key) inventory.Keys++;
        inventory.UpdateCounts();
    }
}

public enum CollectibleType
{
    Pearl,
    Battery,
    Key
}