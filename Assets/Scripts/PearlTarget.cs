using UnityEngine;

public class PearlTarget : MonoBehaviour
{
    private Rigidbody _body;
    
    public void Hit(Vector3 dir, Vector3 point)
    {
        if (!_body) _body = gameObject.AddComponent<Rigidbody>();
        _body?.AddForceAtPosition(dir * 0.75f, point, ForceMode.Impulse);
    }
}