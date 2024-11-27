using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HackMenu : MonoBehaviour
{
    [SerializeField] private List<Image> lights;
    [SerializeField] private List<Safe> safes;
    [SerializeField] private TMP_Text frequency;

    private int _light;
    private Safe _current;
    private Color _offColor;
    private List<int> _numbers;
    private int _prev;

    private void Start()
    {
        _offColor = lights[0].color;
        Invoke(nameof(Cycle), 1f);
    }

    public void Find()
    {
        lights.ForEach(l => l.color = _offColor);
        _current = safes.Where(s => Vector3.Distance(transform.position, s.transform.position) < 2f)
            .OrderBy(s => Vector3.Distance(transform.position, s.transform.position))
            .FirstOrDefault();
        _light = 0;
        if(_current) _numbers = _current.GetNumbers();
        frequency.text = _current ? _current.Frequency : "NOT FOUND!";
        Blink();
    }

    private void Blink()
    {
        lights[_prev].color = _offColor;
        
        if (_current)
        {
            _light = (_light + 1).LoopAround(0, _numbers.Count);
            _prev = _numbers[_light] - 1;
            this.StartCoroutine(() => lights[_prev].color = Color.white, 0.2f);
            return;
        }
        
        _light = 0;
    }

    private void Cycle()
    {
        Blink();
        Invoke(nameof(Cycle), 0.5f);
    }
}