using System;
using TMPro;
using UnityEngine;

namespace AnttiStarterKit.Utils
{
    [RequireComponent(typeof(TMP_Text))]
    public class TimeDisplay : MonoBehaviour
    {
        private TMP_Text _display;
        private TimeSpan _time;

        private void Start()
        {
            _display = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _time += TimeSpan.FromSeconds(Time.deltaTime);
            _display.text = _time.ToString();
        }
    }
}