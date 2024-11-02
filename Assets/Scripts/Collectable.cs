public class Collectable : Clickable
{
    public override void Click()
    {
        gameObject.SetActive(false);
    }
}