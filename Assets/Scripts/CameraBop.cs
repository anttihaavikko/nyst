using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class CameraBop : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private StarterAssetsInputs input;
    
    private int _mode;
    private CinemachineBasicMultiChannelPerlin _noise;

    private void Start()
    {
        _noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        var next = GetMode();

        if (next == _mode) return;

        var amplitudes = new[] { 0.5f, 0.75f, 1f };
        var frequencies = new[] { 0.3f, 1f, 3f };
        
        _mode = next;
        _noise.m_AmplitudeGain = amplitudes[_mode];
        _noise.m_FrequencyGain = frequencies[_mode];
    }

    private int GetMode()
    {
        if (input.sprint) return 2;
        if (input.move.magnitude > 0.1f) return 1;
        return 0;
    }
}