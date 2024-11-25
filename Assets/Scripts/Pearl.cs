using System;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pearl : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Clickable clickable;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private SoundComposition splash;

    private Vector3 _respawn;

    public MeshRenderer Mesh => mesh;

    public void Throw(Vector3 dir, Vector3 respawn)
    {
        _respawn = respawn;
        rigidBody.isKinematic = false;
        rigidBody.AddForce(dir, ForceMode.Impulse);
        this.StartCoroutine(() => clickable.enabled = true, 1f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            other.gameObject.GetComponent<PearlTarget>()?.Hit(rigidBody.linearVelocity, other.contacts[0].point);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Kill")) return;
        splash.Play(transform.position);
        transform.position = _respawn;
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.AddForce(new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) * 10f, ForceMode.Impulse);
    }
}