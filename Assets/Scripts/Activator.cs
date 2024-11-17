using System.Collections.Generic;
using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private List<Activatable> activates;
    [SerializeField] private SoundComposition sound;

    public void Activate()
    {
        activates.ForEach(a => a.Activate());
        sound?.Play(transform.position);
    }
}