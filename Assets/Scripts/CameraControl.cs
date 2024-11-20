using UnityEngine;

public class CameraControl : Activatable
{
    [SerializeField] private CameraMover cam;
    [SerializeField] private int zoom;
    [SerializeField] private int angle;
    
    public override void Activate()
    {
        cam.Move(angle, zoom);
    }
}