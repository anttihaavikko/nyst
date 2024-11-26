using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gem : Clickable
{
    [SerializeField] private CollectibleType type;
    [SerializeField] private Material invisible;
    [SerializeField] private Combiner combiner;
    [SerializeField] private Light light;

    private MeshRenderer _mesh;
    private Material _normal;
    private bool _visible;

    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _normal = _mesh.material;
        _mesh.material = invisible;
    }

    public override void Click(Inventory inventory)
    {
        _visible = !_visible;
        inventory.Toggle(type);
        _mesh.material = _visible ? _normal : invisible;
        if(_visible) combiner.Activate();
        else combiner.Deactivate();
        light.enabled = _visible;
    }
    
    public override bool CanInteract(Inventory inventory)
    {
        return _visible || inventory.Has(type);
    }
}