using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class MirrorPuzzle : Activatable
{
    [SerializeField] private List<PushButton> buttons;
    [SerializeField] private Activatable activatable;
    [SerializeField] private SoundComposition failSound;

    private bool _done;
    
    public override void Activate()
    {
        var fails = 0;
        if (buttons[0].GetState != buttons[3].GetState) fails++;
        if (buttons[1].GetState != buttons[4].GetState) fails++;
        if (buttons[2].GetState != buttons[5].GetState) fails++;
        if(fails > 1)
        {
            failSound.Play(transform.position, 2f);
            buttons.ForEach(b => b.Reset());
        }

        if (buttons.All(b => b.IsOk) && !_done)
        {
            _done = true;
            activatable.Activate();
        }
    }
}