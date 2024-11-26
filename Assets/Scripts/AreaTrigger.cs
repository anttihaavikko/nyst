using UnityEngine;

[RequireComponent(typeof(Activator))]
public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private bool singleUse;
    
    private Activator _activator;
    private bool _used;

    private void Start()
    {
        _activator = GetComponent<Activator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (singleUse && _used) return;
        _activator.Activate();
        _used = true;
    }
}