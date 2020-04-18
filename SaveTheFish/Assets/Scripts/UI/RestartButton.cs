using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject button;

        public void Start()
        {
            UpdateVisibility();
        }

        public void Update()
        {
            UpdateVisibility();
        }

        public void OnClick()
        {
            ActionManager.Instance.Restart();
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            button.SetActive(ActionManager.Instance.IsGameOver());
        }
    }
}