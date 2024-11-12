using System;
using UnityEngine;

public class Ball : Clickable
{
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody body;

    private void Start()
    {
        body.sleepThreshold = 0;
    }

    public override void Click(Inventory inventory)
    {
        body.AddForce(cam.transform.forward * 20f, ForceMode.Impulse);
    }
}