using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DisplaySequence : MonoBehaviour
    {
        [SerializeField]
        private GameObject container;

        [SerializeField]
        private TMP_Text uiMainAction;

        [SerializeField]
        private TMP_Text uiSequence;

        [SerializeField]
        private float showDuration = 3f;


        public void Show(Action currentAction, List<Action> lastActions)
        {
            UpdateContent(currentAction, lastActions);
            StartCoroutine(Appear());
        }

        private void UpdateContent(Action currentAction, List<Action> lastActions)
        {
            uiMainAction.text = currentAction.ToString();
            string msg = "";
            foreach (var action in lastActions)
            {
                msg += action.ToString() + ", ";
            }
            uiSequence.text = msg;
        }

        private IEnumerator Appear()
        {
            container.SetActive(true);
            yield return new WaitForSecondsRealtime(showDuration);
            container.SetActive(false);
            ActionManager.Instance.OnInstructionsHidden();
        }
    }
}