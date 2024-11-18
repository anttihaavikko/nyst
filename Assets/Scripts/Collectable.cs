using UnityEngine;

public class Collectable : Clickable
{
    [SerializeField] private CollectibleType type;
    
    public override void Click(Inventory inventory)
    {
        gameObject.SetActive(false);
        if (type == CollectibleType.Battery) inventory.Batteries++;
        if (type == CollectibleType.Pearl) inventory.AddPearl(gameObject.GetComponent<Pearl>());
        if (type == CollectibleType.Key) inventory.Keys++;
        if (type == CollectibleType.Compass) inventory.Compass = true;
        inventory.UpdateCounts();
    }
}

public enum CollectibleType
{
    Pearl,
    Battery,
    Key,
    Compass
}