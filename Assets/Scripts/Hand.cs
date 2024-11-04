using System;
using AnttiStarterKit.Animations;
using StarterAssets;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hand : MonoBehaviour
{
    [SerializeField] private float direction;
    [SerializeField] private Transform target;
    [SerializeField] private Finger thumb, index, middle, ring, little;
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Transform wrist;

    private Vector3 _originalPosition;
    private float _delta;
    private float _time;
    private float _lift;
    private float _wristOffset;
    private bool _pointing;
    private float _pointPhase;
    
    private const float Speed = 3f;

    private void Start () {
        _originalPosition = target.localPosition;
        _wristOffset = Random.value * 1000f;
    }

    public void Point(bool state)
    {
        _pointing = state;
    }

    public void Push()
    {
        _pointPhase = 0.25f;
    }

    private void LateUpdate()
    {
        wrist.Rotate(GetWristAngle());
    }

    private Vector3 GetWristAngle()
    {
        var isPointing = _pointing || direction > 0 && Input.GetMouseButton(1);
        _pointPhase = Mathf.MoveTowards(_pointPhase, isPointing ? 1f : 0f, Time.deltaTime * (isPointing ? 5f : 2f));
        thumb.Curl(_pointPhase);
        index.Curl(_pointPhase);
        var idle = new Vector3((Mathf.PerlinNoise1D(Time.time * 0.5f + _wristOffset) - 0.5f) * 40f, 0, 0);
        return Vector3.Lerp(idle, new Vector3(45f, 10f, 0), TweenEasings.SineEaseInOut(_pointPhase));
    }

    private void Update ()
    {
        var fall = firstPersonController.Grounded ? 0 : -firstPersonController.VerticalVelocity;
        _lift = Mathf.MoveTowards(_lift, fall * 0.5f, Time.deltaTime * (firstPersonController.Grounded ? 20f : 10f));
        var moving = input.move.magnitude > 0;
        var running = moving && input.sprint;
        var speed = running ? 1.5f : 1f;
        _delta = Mathf.MoveTowards(_delta, moving ? speed : 0f, Time.deltaTime);
        var phaseOffset = moving && direction < 0 ? Mathf.PI * 0.5f : 0;
        _time += Time.deltaTime * speed;
        var phase = _time * 0.75f * Speed + Mathf.PI + phaseOffset;
        var diff = Mathf.Lerp(0, Mathf.Abs(Mathf.Sin(phase)) - 0.5f, _delta);
        var dir = Mathf.PerlinNoise1D(_time * 0.3f + direction * 100f) * 1.5f - 0.75f + diff + _lift;
        target.localPosition = _originalPosition + Vector3.up * dir * 0.1f;
    }
}