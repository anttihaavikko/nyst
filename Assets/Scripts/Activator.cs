using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private List<Activatable> activates;

    public void Activate()
    {
        activates.ForEach(a => a.Activate());
    }
}