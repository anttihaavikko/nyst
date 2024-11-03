using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Finger : MonoBehaviour
{
    [SerializeField] private float amount = 1f;
    
    private List<Transform> _bones;
    private Quaternion _origin;

    private void Start()
    {
        SetBones();
        _origin = transform.localRotation;
    }

    private void SetBones()
    {
        _bones = new List<Transform> { transform };
        while (true)
        {
            if (_bones.Last().childCount <= 0) continue;
            _bones = _bones.Append(_bones.Last().GetChild(0)).ToList();
            break;
        }
    }

    private void Update()
    {
        _bones.ForEach(b => b.localRotation = _origin * Quaternion.Euler(Mathf.Abs(Mathf.Sin(Time.time)) * -90f * amount, 0f, 0f));
    }
}