﻿using PostProcess;
using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailSequence : MonoBehaviour
{
    [SerializeField]
    private TMP_Text failText;
    [SerializeField]
    private float shakeDuration = 1.2f;
    [SerializeField]
    private int repeatBlink = 10;

    private SwitchProcessProfile _switchProcess;
    public SwitchProcessProfile switchProcess
    {
        get
        {
            if(_switchProcess == null)
            {
                _switchProcess = Camera.main.GetComponent<SwitchProcessProfile>();
            }
            return _switchProcess;
        }
    }

    private CameraShake _cameraShake;
    public CameraShake cameraShake
    {
        get
        {
            if (_cameraShake == null)
            {
                _cameraShake = GetComponentInChildren<CameraShake>(true);
            }
            return _cameraShake;
        }
    }

    private BlinkColorGradient _blinkColor;
    public BlinkColorGradient blinkColor
    {
        get
        {
            if (_blinkColor == null)
            {
                _blinkColor = Camera.main.GetComponent<BlinkColorGradient>();
            }
            return _blinkColor;
        }
    }

    public void Show(UserAction userAction, UserAction wantedAction)
    {
        gameObject.SetActive(true);
        switchProcess.SetProfile(2);
        failText.text = CreateFailText(userAction, wantedAction);
        SoundManager.Play("DefeatHowl");
        if (cameraShake != null)
            cameraShake.StartShake(shakeDuration);
        if (blinkColor != null)
            blinkColor.StartBlink(shakeDuration, repeatBlink);
    }

    private string CreateFailText(UserAction userAction, UserAction wantedAction)
    {
        string userActionMsg = userAction == null ? 
            "You have done nothing" : "You have done '" + userAction.ShortLabel() +"'";
        string fishMsg = wantedAction.doIt ? 
            "But Fish says '" + wantedAction.ShortLabel() + "'" : "But Fish says nothing";
        return ""+userActionMsg+ "\n" +
             fishMsg + "\n" +
            "Fish died and it's all your fault... Hope you sleep well...";
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
        switchProcess.SetProfile(0);
        ActionManager.Instance.Restart();
    }


#if UNITY_EDITOR
    [ContextMenu("Test blink")]
    public void TestBlink()
    {
        switchProcess.SetProfile(2);
        blinkColor.StartBlink(shakeDuration, repeatBlink);
    }
#endif
}
