public class Collectable : Clickable
{
    public override void Click(Inventory inventory)
    {
        gameObject.SetActive(false);
    }
}