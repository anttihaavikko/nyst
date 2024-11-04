using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnttiStarterKit.Utils
{
    public class DevReload : MonoBehaviour
    {
        [SerializeField] private bool needsShift;
        
        private void Update()
        {
            if (!Application.isEditor) return;

            if (Input.GetKeyDown(KeyCode.R) && (!needsShift || Input.GetKey(KeyCode.LeftShift)))
            {
                SceneChanger.Instance.ChangeScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}