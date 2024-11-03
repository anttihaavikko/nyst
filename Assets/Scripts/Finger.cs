using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Finger : MonoBehaviour
{
    [SerializeField] private float amount = 1f;
    
    private List<Transform> _bones;
    private Quaternion _origin;
    private float noiseOffset;

    private void Start()
    {
        SetBones();
        _origin = transform.localRotation;
        noiseOffset = Random.value * 1000f;
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
        var diff = Mathf.Clamp01(Mathf.Abs(Mathf.Sin(Time.time) + Mathf.PerlinNoise1D(Time.time * 0.3f + noiseOffset) * 1.3f - 0.7f));
        _bones.ForEach(b => b.localRotation = _origin * Quaternion.Euler(diff * -90f * amount, 0f, 0f));
    }
}