using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BatteryBox : MonoBehaviour
{
    [SerializeField] private List<Battery> batteries;

    public bool IsPowered => batteries.All(b => b.IsPlaced);
}