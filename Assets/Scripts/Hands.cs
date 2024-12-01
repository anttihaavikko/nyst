using System;
using System.Collections.Generic;
using AnttiStarterKit.Extensions;
using AnttiStarterKit.Managers;
using AnttiStarterKit.ScriptableObjects;
using AnttiStarterKit.Utils;
using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hands : MonoBehaviour
{
    [SerializeField] private Animator left, right;
    // [SerializeField] private List<RevertableTransform> revertables;
    [SerializeField] private List<GameObject> arrows;
    [SerializeField] private StarterAssetsInputs inputs;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private GameObject cursor;
    [SerializeField] private Transform launchSpot;
    [SerializeField] private Pearl pearlPrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform launchBar;
    [SerializeField] private Transform pearlRespawn;
    [SerializeField] private Inventory inventory;
    [SerializeField] private SoundComposition jumpSound, landSound, throwSound, spawnSound, jetpackSound;
    [SerializeField] private Transform legPosition;
    [SerializeField] private Compass compass;
    [SerializeField] private Menu menu;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private FrameRateDisplay fpsDisplay;
    
    private float _launchSpeed;
    private Pearl _spawned;
    private bool _state;
    private bool _canChange = true;
    private float _jumpSoundDelay, _landSoundDelay;
    private bool _wasGrounded;
    
    private static readonly int ShowAnim = Animator.StringToHash("show");
    private static readonly int GrabAnim = Animator.StringToHash("grab");

    public bool IsMenuing => _state;

    private void Start()
    {
        firstPersonController.Jumped += JumpSound;
    }

    private void JumpSound()
    {
        if (_jumpSoundDelay > 0) return;
        _jumpSoundDelay = 0.2f;

        (firstPersonController.Grounded ? jumpSound : jetpackSound).Play(legPosition.position);
    }

    private void Update()
    {
        _jumpSoundDelay -= Time.deltaTime;
        _landSoundDelay -= Time.deltaTime;

        if (!_wasGrounded && firstPersonController.Grounded && _landSoundDelay <= 0)
        {
            var volume = Mathf.Clamp(Mathf.Abs(firstPersonController.VerticalVelocity) * 0.1f, 0, 1f);
            _landSoundDelay = 0.2f;
            landSound.Play(legPosition.position, volume);
            impulseSource.GenerateImpulse(volume * 0.3f);
        }

        _wasGrounded = firstPersonController.Grounded;
        
        if (Input.GetKeyDown(KeyCode.Escape)) Toggle();

        if (_state)
        {
            if (inputs.move.y > 0.2f || Input.mouseScrollDelta.y > 0f) ChangeOption(1);
            if (inputs.move.y < -0.2f || Input.mouseScrollDelta.y < 0f) ChangeOption(-1);
            if (Mathf.Abs(inputs.move.y) < 0.2f) _canChange = true;
            if(Input.GetKeyDown(KeyCode.Return)) menu.Act();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(inventory.Has(CollectibleType.Compass)) compass.Show(true);

            if (inventory.Pearls > 0)
            {
                if (!_spawned)
                {
                    _spawned = Instantiate(pearlPrefab, launchSpot.position, Quaternion.identity);
                    _spawned.GetComponent<Clickable>().enabled = false;
                    _spawned.Mesh.material = inventory.PearlMaterial;
                }

                _spawned.gameObject.SetActive(true);
                EffectManager.AddEffect(2, _spawned.transform.position);
                spawnSound?.Play(_spawned.transform.position);   
            }
        }
        
        if(Input.GetMouseButtonUp(1))
        {
            compass.Show(false);

            if (_spawned)
            {
                 _spawned.gameObject.SetActive(false);
                EffectManager.AddEffect(2, _spawned.transform.position);
                throwSound?.Play(_spawned.transform.position);   
            }
        }

        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1) && _spawned)
        {
            throwSound?.Play(_spawned.transform.position);
            _spawned.Throw(cam.transform.forward * (150f * _launchSpeed), pearlRespawn.position);
            compass.AddTarget(_spawned.transform);
            inventory.RemovePearl();
            inventory.UpdateCounts();
            _launchSpeed = 0;
            _spawned = null;
        }

        _launchSpeed = Mathf.MoveTowards(_launchSpeed, Input.GetMouseButton(1) && _spawned ? 1f : 0f, Time.deltaTime);
        launchBar.transform.localScale = new Vector3(_launchSpeed, 1, 1);
    }

    private void LateUpdate()
    {
        if (_spawned)
        {
            var speed = Vector3.Distance(_spawned.transform.position, launchSpot.position) * Time.deltaTime * 5f;
            _spawned.transform.position = Vector3.MoveTowards(_spawned.transform.position, launchSpot.position, speed);
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
        menu.ChangeOption(dir);
        this.StartCoroutine(() => _canChange = true, 0.3f);
        if(menu.IsQuality) fpsDisplay.DisableWarning();
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
            fpsDisplay.HideWarning();
        }
        
        // if(_state) revertables.ForEach(r => r.Snap());
        // else revertables.ForEach(r => r.Revert());
        
        left.SetBool(ShowAnim, _state);
        right.SetBool(GrabAnim, _state);

        menu.Toggle(_state);
        arrows.ForEach(a => a.SetActive(_state));
    }
}