using System;
using System.Collections.Generic;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hands : MonoBehaviour
{
    [SerializeField] private Animator left, right;
    [SerializeField] private List<RevertableTransform> revertables;
    [SerializeField] private TMP_Text screen;
    [SerializeField] private List<string> options;
    [SerializeField] private List<GameObject> arrows;

    private bool _state;
    private int _screenOption;
    private static readonly int ShowAnim = Animator.StringToHash("show");
    private static readonly int GrabAnim = Animator.StringToHash("grab");

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Toggle();

        if (_state)
        {
            if (Input.GetKeyDown(KeyCode.W)) ChangeOption(1);
            if (Input.GetKeyDown(KeyCode.S)) ChangeOption(-1);
        }
    }

    private void ChangeOption(int dir)
    {
        _screenOption = (_screenOption + dir).LoopAround(0, options.Count);
        screen.text = options[_screenOption];
    }

    private void Toggle()
    {
        _state = !_state;
        
        if(_state) revertables.ForEach(r => r.Snap());
        else revertables.ForEach(r => r.Revert());
        
        left.SetBool(ShowAnim, _state);
        right.SetBool(GrabAnim, _state);

        screen.text = _state ? options[_screenOption] : "";
        arrows.ForEach(a => a.SetActive(_state));
    }
}