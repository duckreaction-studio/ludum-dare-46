using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public SkinnedMeshRenderer fishSkin;

    void Update()
    {
        fishSkin.SetBlendShapeWeight(3, Mathf.SmoothStep(70f, 0, Mathf.PingPong(Time.time, 0.85f)));
    }

    void FishStartPress(string target)
    {

    }

    void FishStopPress(string target)
    {

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
