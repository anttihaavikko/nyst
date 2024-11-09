using System;
using AnttiStarterKit.Extensions;
using UnityEngine;

public class Pearl : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Clickable clickable;

    private Vector3 _respawn;

    public void Throw(Vector3 dir, Vector3 respawn)
    {
        _respawn = respawn;
        rigidBody.AddForce(dir, ForceMode.Impulse);
        rigidBody.useGravity = true;
        this.StartCoroutine(() => clickable.enabled = true, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kill"))
        {
            transform.position = _respawn.RandomOffset(0.2f);
            rigidBody.linearVelocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }
}