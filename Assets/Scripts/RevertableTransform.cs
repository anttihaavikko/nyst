using UnityEngine;

public class RevertableTransform : MonoBehaviour
{
    private Vector3 _position, _scale;
    private Quaternion _rotation;

    public void Snap()
    {
        _position = transform.position;
        _scale = transform.localScale;
        _rotation = transform.rotation;
    }

    public void Revert()
    {
        transform.position = _position;
        transform.localScale = _scale;
        transform.rotation = _rotation;
    }
}