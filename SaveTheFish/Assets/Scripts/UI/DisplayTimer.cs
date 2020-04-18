using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DisplayTimer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text textUi;

        void Start()
        {
            textUi.text = "";
        }

        void Update()
        {
            textUi.text = FormatTime(ActionManager.Instance.remaingTime);
        }

        private string FormatTime(float remaingTime)
        {
            int seconds = ((int)remaingTime) / 1;
            int milli = (int)((remaingTime - ((float)seconds)) * 1000);
            return String.Format("{0:0}:{1:000}", seconds, milli);
        }
    }
}