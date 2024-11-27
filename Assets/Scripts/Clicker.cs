using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Managers;
using AnttiStarterKit.ScriptableObjects;
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

    [SerializeField] private Hand right;
    [SerializeField] private HandScreen handScreen;

    [SerializeField] private Inventory inventory;
    [SerializeField] private SoundComposition noPowerSound;

    [SerializeField] private Hands hands;

    private Clickable _prev;

    private void Start()
    {
        UpdateCursor(false);
    }

    private void Update()
    {
        if (hands.IsMenuing) return;
        
        var ray = new Ray(cam.transform.position - cam.transform.forward * 0.5f, cam.transform.forward);
        var hits = Physics.RaycastAll(ray.origin, ray.direction, distance, mask);
        if (hits.Length <= 0)
        {
            if (_prev)
            {
                UpdateCursor(false);
            }
            right.Point(false);
            _prev = null;
            return;
        }
        
        var target = hits.OrderBy(o => Vector3.Distance(cam.transform.position, o.point)).First();

        // Debug.Log($"Pointing at {target.collider.name}");
        
        var clickable = target.collider.GetComponent<Clickable>();
        if (clickable && !clickable.enabled) clickable = null;

        if (clickable && !clickable.CanInteract(inventory))
        {
            clickable = null;
        }

        if (clickable != _prev)
        {
            UpdateCursor(clickable);
            clickable?.ToggleOutline(true);
        }

        _prev = clickable;
        
        right.Point(clickable);
        if (clickable)
        {
            handScreen.Show(clickable.ScreenName);
        }

        if (!Input.GetMouseButtonDown(0)) return;
        
        if (clickable)
        {
            EffectManager.AddEffect(clickable.ClickEffect, hits[0].point - ray.direction * 0.25f);
            right.Push(clickable.PointDelay);
            if (clickable.IsPowered || inventory.IsHacking)
            {
                clickable.PlaySound(inventory);
                clickable.Click(inventory);
                return;
            }

            noPowerSound?.Play();
            clickable.Nudge();
        }
    }

    private void UpdateCursor(bool state)
    {
        _prev?.ToggleOutline(false);
        const float duration = 0.2f;
        var size = Vector3.one * (state ? 1f : 0.6f);
        // cursorLines.ForEach(l => Tweener.ScaleToBounceOut(l, size, duration));
        Tweener.ScaleToBounceOut(cursorWrap, Vector3.one * (state ? 2.5f : 2f), duration);
        Tweener.RotateToBounceOut(cursorWrap, Quaternion.Euler(0, 0, state ? 45f * Misc.PlusMinusOne() : 0f), duration);
    }
}