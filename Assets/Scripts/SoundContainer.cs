using AnttiStarterKit.ScriptableObjects;
using UnityEngine;

public class SoundContainer : MonoBehaviour
{
    [SerializeField] private SoundComposition sound;

    public void Play(Vector3 pos)
    {
        sound?.Play(pos);
    }
}