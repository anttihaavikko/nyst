using System;
using TMPro;
using UnityEngine;

namespace AnttiStarterKit.Utils
{
    [RequireComponent(typeof(TMP_Text))]
    public class FrameRateDisplay : MonoBehaviour
    {
        [SerializeField] private float updateDelay = 0.2f;
        
        private TMP_Text _display;
        private float _sum;
        private int _frames;

        private void Start()
        {
            _display = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _frames++;
            _sum += Time.deltaTime;
            if (_sum < updateDelay) return;
            _display.text = Mathf.RoundToInt(1f / (_sum / _frames)).ToString();
            _sum = 0;
            _frames = 0;
        }
    }
}