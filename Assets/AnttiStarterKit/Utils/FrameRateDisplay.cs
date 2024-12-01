using System;
using AnttiStarterKit.Animations;
using TMPro;
using UnityEngine;

namespace AnttiStarterKit.Utils
{
    [RequireComponent(typeof(TMP_Text))]
    public class FrameRateDisplay : MonoBehaviour
    {
        [SerializeField] private float updateDelay = 0.2f;
        [SerializeField] private Appearer lowFrameRateWarning;
        
        private TMP_Text _display;
        private float _sum;
        private int _frames;
        private bool _warned;
        private int _lowCount;
        
        public bool CanWarn { get; set; }

        private void Start()
        {
            _display = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _frames++;
            _sum += Time.deltaTime;
            if (_sum < updateDelay) return;
            var fps = 1f / (_sum / _frames);
            if (fps < 60f) _lowCount++;
            _display.text = Mathf.RoundToInt(fps).ToString();
            _sum = 0;
            _frames = 0;

            if (CanWarn && !_warned && lowFrameRateWarning && _lowCount > 10)
            {
                ShowWarning();
            }
        }

        private void ShowWarning()
        {
            lowFrameRateWarning.Show();
            _warned = true;
            Invoke(nameof(HideWarning), 10f);
        }

        public void HideWarning()
        {
            if(lowFrameRateWarning.IsShown) lowFrameRateWarning.Hide();
        }

        public void DisableWarning()
        {
            _warned = true;
        }
    }
}