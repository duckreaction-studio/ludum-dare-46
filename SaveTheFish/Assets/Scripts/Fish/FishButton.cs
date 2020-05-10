using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fish
{
    public class FishButton : MonoBehaviour
    {
        [SerializeField]
        private string target;
        [SerializeField]
        private Transform followTransform;

        private float startTime;
        private bool press = false;

        [ContextMenu("Update position and rotation")]
        public void Update()
        {
            if (followTransform != null)
            {
                transform.position = followTransform.position;
                transform.rotation = followTransform.rotation;
            }
        }

        private void OnMouseDown()
        {
            StartPress();
        }

        private void OnMouseUp()
        {
            StopPress();
        }

        private void OnMouseExit()
        {
            //StopPress();
        }

        private void StartPress()
        {
            press = true;
            startTime = Time.realtimeSinceStartup;
            ActionManager.Instance.StartPauseTimer();
            SendMessageUpwards("FishStartPress", target);
            SoundManager.Play("Grab");
        }

        private void StopPress()
        {
            if (press)
            {
                press = false;
                ActionManager.Instance.StopPauseTimer();
                SendMessageUpwards("FishStopPress", target);
                float time = Time.realtimeSinceStartup - startTime;
                if (time > ActionManager.Instance.holdMinTime)
                {
                    ActionManager.Instance.DoAction(UserAction.Hold(target));
                }
                else
                {
                    ActionManager.Instance.DoAction(UserAction.Click(target));
                }
            }
        }
    }
}