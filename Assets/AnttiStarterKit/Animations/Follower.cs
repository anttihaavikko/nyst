using System;
using UnityEngine;

namespace AnttiStarterKit.Animations
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            Fix();
        }

        private void FixedUpdate()
        {
            Fix();
        }

        private void LateUpdate()
        {
            Fix();
        }

        private void Fix()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}