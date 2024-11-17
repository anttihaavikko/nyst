using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class SafeHandle : Clickable
{
    [SerializeField] private Safe safe;
    [SerializeField] private Activator activator;
    [SerializeField] private SoundComposition failSound;
    
    public override void Click(Inventory inventory)
    {
        if (safe.IsCorrect)
        {
            activator.Activate();
            return;
        }
        
        failSound.Play(transform.position);
        safe.Reset();
    }
}