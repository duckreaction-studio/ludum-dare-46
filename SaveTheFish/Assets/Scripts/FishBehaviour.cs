using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer fishSkin;
    [SerializeField]
    private float maxBlend = 70f;
    [SerializeField]
    private float blendTransition = 0.15f;

    private int currentBlendIndex;
    private float startValue;
    private float endValue;
    private bool play;
    private float delta;

    void Update()
    {
        fishSkin.SetBlendShapeWeight(3, Mathf.SmoothStep(maxBlend, 0, Mathf.PingPong(Time.time, 0.85f)));
        if (play)
        {
            delta += Time.deltaTime;
            fishSkin.SetBlendShapeWeight(currentBlendIndex, Mathf.SmoothStep(startValue, endValue,delta / blendTransition));
            if (delta > blendTransition)
                play = false;
        }
    }

    void FishStartPress(string target)
    {
        StartBlend(target);
    }

    void FishStopPress(string target)
    {
        StartBlend(target, true);
    }

    private void StartBlend(string target, bool revert = false)
    {
        currentBlendIndex = TargetToBendShapeIndex(target);
        if (currentBlendIndex >= 0)
        {
            if (revert)
            {
                startValue = maxBlend;
                endValue = 0f;
            } else { 
                startValue = 0f;
                endValue = maxBlend;
            }
            play = true;
            delta = 0;
        }
    }

    int TargetToBendShapeIndex(string target)
    {
        switch(target)
        {
            case "tail":
                return 0;
            case "body":
                return 1;
            case "head":
                return 2;
        }
        return -1;
    }
}
