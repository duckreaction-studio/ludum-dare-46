using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishButton : MonoBehaviour
{
    [SerializeField]
    private string target;

    private float startTime;
    private bool press = false;

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
        StopPress();
    }

    private void StartPress()
    {
        press = true;
        startTime = Time.realtimeSinceStartup;
    }

    private void StopPress()
    {
        if (press)
        {
            press = false;
            float time = Time.realtimeSinceStartup - startTime;
            if (time > ActionManager.Instance.holdMinTime)
            {
                ActionManager.Instance.DoAction(Action.Hold(target));
            }
            else
            {
                ActionManager.Instance.DoAction(Action.Click(target));
            }
        }
    }
}
