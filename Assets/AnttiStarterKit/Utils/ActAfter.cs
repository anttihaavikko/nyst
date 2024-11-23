using System;
using AnttiStarterKit.Extensions;
using UnityEngine;

namespace AnttiStarterKit.Utils
{
    [ExecuteInEditMode]
    public class ActAfter : MonoBehaviour
    {
        public void Init(Action action, float delay)
        {
            this.StartCoroutine(action.Invoke, delay);
        }
    }
}