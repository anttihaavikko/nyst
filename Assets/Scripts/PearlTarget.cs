using AnttiStarterKit.Managers;
using UnityEngine;

public class PearlTarget : MonoBehaviour
{
    private Rigidbody _body;
    
    public virtual void Hit(Vector3 dir, Vector3 point)
    {
        if (!_body) _body = gameObject.AddComponent<Rigidbody>();
        EffectManager.AddEffect(1, point);
        _body?.AddForceAtPosition(dir * 0.75f, point, ForceMode.Impulse);
    }
}