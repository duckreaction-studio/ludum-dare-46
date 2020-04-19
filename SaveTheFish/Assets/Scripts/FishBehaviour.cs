using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public SkinnedMeshRenderer fishSkin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fishSkin.SetBlendShapeWeight(3, Mathf.SmoothStep(70f, 0, Mathf.PingPong(Time.time, 0.85f)));
    }
}
