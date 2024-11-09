using System;
using System.Collections.Generic;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.Utils;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hands : MonoBehaviour
{
    [SerializeField] private Animator left, right;
    // [SerializeField] private List<RevertableTransform> revertables;
    [SerializeField] private List<MenuOption> options;
    [SerializeField] private List<GameObject> arrows;
    [SerializeField] private StarterAssetsInputs inputs;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private GameObject cursor;

    private bool _state;
    private int _screenOption;
    private bool _canChange = true;
    private static readonly int ShowAnim = Animator.StringToHash("show");
    private static readonly int GrabAnim = Animator.StringToHash("grab");

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Toggle();

        if (_state)
        {
            if (inputs.move.y > 0.2f || Input.mouseScrollDelta.y > 0f) ChangeOption(1);
            if (inputs.move.y < -0.2f || Input.mouseScrollDelta.y < 0f) ChangeOption(-1);
            if (Mathf.Abs(inputs.move.y) < 0.2f) _canChange = true;
            if(Input.GetKeyDown(KeyCode.Return)) options[_screenOption].Act();
        }
    }
    
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void ChangeOption(int dir)
    {
        if (!_canChange) return;
        _canChange = false;
        options[_screenOption].gameObject.SetActive(false);
        _screenOption = (_screenOption + dir).LoopAround(0, options.Count);
        options[_screenOption].gameObject.SetActive(true);
        this.StartCoroutine(() => _canChange = true, 0.3f);
    }

    public void Toggle()
    {
        _state = !_state;
        SetCursorState(!_state);
        inputs.Locked = _state;
        firstPersonController.Locked = _state;
        cursor.SetActive(!_state);

        if (_state)
        {
            inputs.move = inputs.look = Vector2.zero;
        }
        
        // if(_state) revertables.ForEach(r => r.Snap());
        // else revertables.ForEach(r => r.Revert());
        
        left.SetBool(ShowAnim, _state);
        right.SetBool(GrabAnim, _state);

        options[_screenOption].gameObject.SetActive(_state);
        arrows.ForEach(a => a.SetActive(_state));
    }
}