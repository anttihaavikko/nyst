using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class Door : Clickable
{
    [SerializeField] private SoundComposition lockSound;
    
    private Activatable _toggleable;

    private void Start()
    {
        _toggleable = GetComponent<Activatable>();
    }

    public override void Click(Inventory inventory)
    {
        if (locked)
        {
            lockSound?.Play(transform.position);
            return;
        }
        
        _toggleable?.Activate();
    }
    
    public override bool CanInteract(Inventory inventory)
    {
        return true;
    }
}