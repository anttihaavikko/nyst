using System;
using AnttiStarterKit.Animations;
using AnttiStarterKit.Managers;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class EndSwitch : Activatable
{
    [SerializeField] private MusicChanger musicChanger;
    [SerializeField] private Image fader;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private float _phase;
    private bool _ending;

    public override void Activate()
    {
        musicChanger.PitchOut();
        Invoke(nameof(Quit), 5f);
        fader.gameObject.SetActive(true);
        _ending = true;
        impulseSource.GenerateImpulse(0.5f);
    }

    private void Update()
    {
        if (!_ending) return;
        _phase += Time.deltaTime * 1f;
        fader.color = Color.Lerp(Color.clear, Color.black, _phase);
    }

    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}