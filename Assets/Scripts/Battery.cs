using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Battery : Clickable
{
    [SerializeField] private bool visible;
    [SerializeField] private Material invisible;

    private MeshRenderer _mesh;
    private Material _normal;

    public bool IsPlaced => visible;
    
    public Action Toggled { get; set; }

    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _normal = _mesh.material;
        _mesh.material = visible ? _normal : invisible;
    }

    public override void Click(Inventory inventory)
    {
        inventory.Batteries += visible ? 1 : -1;
        inventory.UpdateCounts();
        visible = !visible;
        _mesh.material = visible ? _normal : invisible;
        Toggled?.Invoke();
        transform.rotation *= Quaternion.Euler(0, 0, Random.value * 360f);
    }
    
    public override bool CanInteract(Inventory inventory)
    {
        return visible || inventory.Batteries > 0;
    }
}