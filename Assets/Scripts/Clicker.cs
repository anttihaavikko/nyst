using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Clicker : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask mask;

    [SerializeField] private Transform cursorWrap;
    [SerializeField] private List<Transform> cursorLines;

    private Clickable _prev;

    private void Start()
    {
        UpdateCursor(false);
    }

    private void Update()
    {
        var ray = new Ray(cam.transform.position - cam.transform.forward * 0.5f, cam.transform.forward);
        var hits = Physics.RaycastAll(ray.origin, ray.direction, distance, mask);
        if (hits.Length <= 0)
        {
            if (_prev)
            {
                UpdateCursor(false);
            }
            _prev = null;
            return;
        }
        
        var target = hits.OrderBy(o => Vector3.Distance(cam.transform.position, o.point)).First();

        // Debug.Log($"Pointing at {target.collider.name}");
        
        var clickable = target.collider.GetComponent<Clickable>();

        if (clickable != _prev)
        {
            UpdateCursor(clickable);   
            clickable?.ToggleOutline(true);
        }

        _prev = clickable;
        
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (clickable)
        {
            clickable.Click();
        }
    }

    private void UpdateCursor(bool state)
    {
        _prev?.ToggleOutline(false);
        const float duration = 0.2f;
        var size = Vector3.one * (state ? 1f : 0.6f);
        cursorLines.ForEach(l => Tweener.ScaleToBounceOut(l, size, duration));
        Tweener.ScaleToBounceOut(cursorWrap, Vector3.one * (state ? 2.5f : 2f), duration);
        Tweener.RotateToBounceOut(cursorWrap, Quaternion.Euler(0, 0, state ? 45f * Misc.PlusMinusOne() : 0f), duration);
    }
}