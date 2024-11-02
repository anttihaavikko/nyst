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

    [SerializeField] private Transform cursorWrap;
    [SerializeField] private List<Transform> cursorLines;

    private Clickable _prev;

    private void Start()
    {
        UpdateCursor(false);
    }

    private void Update()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray.origin, ray.direction, distance);
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
        var clickable = target.collider.GetComponent<Clickable>();

        if (clickable != _prev)
        {
            UpdateCursor(clickable);   
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
        const float duration = 0.2f;
        var size = Vector3.one * (state ? 1f : 0.6f);
        cursorLines.ForEach(l => Tweener.ScaleToBounceOut(l, size, duration));
        Tweener.ScaleToBounceOut(cursorWrap, Vector3.one * (state ? 2f : 1.5f), duration);
        Tweener.RotateToBounceOut(cursorWrap, Quaternion.Euler(0, 0, state ? 45f * Misc.PlusMinusOne() : 0f), duration);
    }
}