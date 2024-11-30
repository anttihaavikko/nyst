using System;
using System.Collections.Generic;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Slots : Clickable
{
    [SerializeField] private List<GameObject> rewards;
    [SerializeField] private Toggleable lever;
    [SerializeField] private List<Wheel> wheels;
    [SerializeField] private Transform spawnSpot;
    [SerializeField] private Inventory inventory;
    [SerializeField] private List<Material> pearlMaterials;
    [SerializeField] private List<Light> lights;

    private bool _busy;
    private float _lightMulti = 1f;

    private void Update()
    {
        var intensity = (0.15f + Mathf.Abs(Mathf.Sin(Time.time * 2.5f)) * 0.05f) * _lightMulti;
        lights.ForEach(l => l.intensity = intensity);
    }

    public override void Click(Inventory inventory)
    {
        if (_busy) return;
        if (inventory.Coins == 0 && !inventory.IsHacking)
        {
            lever.Nudge();
            return;
        }

        _busy = true;
        inventory.Coins--;
        lever.Activate();
        var delay = 1f + Random.value * 2;
        wheels[0].Spin(delay);
        delay += Random.value * 2f;
        wheels[1].Spin(delay);
        delay += Random.value * 2f;
        wheels[2].Spin(delay);
        Invoke(nameof(Reset), 1f);
        Invoke(nameof(Check), delay);
    }

    private void Check()
    {
        // Debug.Log($"{wheels[0].Value} - {wheels[1].Value} - {wheels[2].Value}");
        
        if (rewards[wheels[0].Value] == rewards[wheels[1].Value] && rewards[wheels[1].Value] == rewards[wheels[2].Value] || inventory.IsHacking)
        {
            _lightMulti = 1.5f;

            this.StartCoroutine(() => _lightMulti = 1f, 0.6f);
            this.StartCoroutine(() => _lightMulti = 1.25f, 1.25f);
            this.StartCoroutine(() => _lightMulti = 1f, 1.75f);
            
            this.StartCoroutine(() =>
            {
                AudioManager.Instance.PlayEffectAt(0, spawnSpot.position, 1f, false);
            }, 0.5f);
            
            // Debug.Log(rewards[wheels[0].Value].name);
            for (var i = 0; i < 3; i++)
            {
                this.StartCoroutine(() =>
                {
                    var obj = Instantiate(rewards[wheels[0].Value], spawnSpot.position + spawnSpot.right * Random.value * 0.3f, Random.rotation);
                    var pearl = obj.GetComponent<Pearl>();
                    if (pearl) pearl.Mesh.material = pearlMaterials.Random();
                }, 0.75f + Random.value * 0.5f);
            }
        }
        
        _busy = false;
    }

    private void Reset()
    {
        lever.Activate();
    }
}