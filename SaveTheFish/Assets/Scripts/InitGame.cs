using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    [SerializeField]
    private GameObject[] disableOnStartup;
    [SerializeField]
    private GameObject[] enableOnStartup;
    [SerializeField]
    private GameObject startupScreen;
    [SerializeField]
    private float waitBeforeShowStartupScreen = 3f;


    void Start()
    {
        startupScreen.SetActive(false);
        foreach(var go in disableOnStartup)
        {
            go.SetActive(false);
        }
        foreach(var go in enableOnStartup)
        {
            go.SetActive(true);
        }
        StartCoroutine(IntroCoroutine());
        SoundManager.Play("Splash");
    }

    private IEnumerator IntroCoroutine()
    {
        yield return new WaitForSecondsRealtime(waitBeforeShowStartupScreen);
        startupScreen.SetActive(true);
        SoundManager.Play("StartClick");
    }

}
