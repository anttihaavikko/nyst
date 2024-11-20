using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private float _zoom;
    private float _angle;

    private Quaternion _rot;

    private void Start()
    {
        _rot = transform.rotation;
        _zoom = cam.fieldOfView;
    }

    public void Move(int angle, int zoom)
    {
        _angle = Mathf.Clamp(_angle + angle * 5f, -90f, 90f);
        _zoom = Mathf.Clamp(_zoom - zoom * 10f, 10f, 110f);
    }

    private void Update()
    {
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, _zoom, Time.deltaTime * 20f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot * Quaternion.Euler(Vector3.up * _angle), Time.deltaTime * 20f);
    }
}