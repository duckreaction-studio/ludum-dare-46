using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FocusDof : MonoBehaviour
{
    public GameObject focusTarget;

    DepthOfField dofLayer = null;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out dofLayer);
    }

    // Update is called once per frame
    void Update()
    {
        dofLayer.focusDistance.value = Vector3.Distance(focusTarget.transform.position, Camera.main.transform.position);
    }
}
