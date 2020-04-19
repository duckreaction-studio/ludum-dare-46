using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DisplayTimer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text textUi;
        [SerializeField]
        private Image image;

        void Start()
        {
            textUi.text = "";
        }

        void Update()
        {
            textUi.text = FormatTime(ActionManager.Instance.remainingTime);
            image.fillAmount = ActionManager.Instance.remainingRatio;
        }

        private string FormatTime(float remaingTime)
        {
            int seconds = Mathf.CeilToInt(remaingTime);
            //int milli = (int)((remaingTime - ((float)seconds)) * 1000);
            return String.Format("{0:0}", seconds);
        }
    }
}