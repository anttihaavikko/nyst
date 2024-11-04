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
    private float _noiseOffset;
    private float _curl;

    private void Start()
    {
        SetBones();
        _origin = transform.localRotation;
        _noiseOffset = Random.value * 1000f;
    }

    public void Curl(float curl)
    {
        _curl = curl;
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
        var noise = Mathf.PerlinNoise1D(Time.time * 0.3f + _noiseOffset) * 1.3f - 0.7f;
        var diff = Mathf.Clamp01(Mathf.Abs(Mathf.Sin(Time.time) + noise));
        var angle = Vector3.Lerp(new Vector3(diff * -90f * amount, 0f, 0f), new Vector3(5f - noise * 10f, 0f, 0f), _curl);
        _bones.ForEach(b => b.localRotation = _origin * Quaternion.Euler(angle));
    }
}