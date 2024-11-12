using UnityEngine;

public class BallOpener : Clickable
{
    [SerializeField] private GameObject contents;
    [SerializeField] private GameObject ball;
    
    public override void Click(Inventory inventory)
    {
        contents.transform.parent = null;
        contents.SetActive(true);
        ball.SetActive(false);
    }
}