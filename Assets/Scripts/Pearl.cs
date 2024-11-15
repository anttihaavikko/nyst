using System;
using AnttiStarterKit.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pearl : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Clickable clickable;

    private Vector3 _respawn;

    public void Throw(Vector3 dir, Vector3 respawn)
    {
        _respawn = respawn;
        rigidBody.isKinematic = false;
        rigidBody.AddForce(dir, ForceMode.Impulse);
        this.StartCoroutine(() => clickable.enabled = true, 1f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Target")
        {
            other.gameObject.GetComponent<PearlTarget>()?.Hit(rigidBody.linearVelocity, other.contacts[0].point);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kill"))
        {
            transform.position = _respawn;
            rigidBody.linearVelocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            rigidBody.AddForce(new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) * 10f, ForceMode.Impulse);
        }
    }
}