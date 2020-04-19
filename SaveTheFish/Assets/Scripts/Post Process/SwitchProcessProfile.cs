using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SwitchProcessProfile : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume postProcessVolume;

    [SerializeField]
    private PostProcessProfile[] profiles;

    public void SetProfile(int profileIndex)
    {
        if (profileIndex >= 0 && profileIndex < profiles.Length)
        {
            postProcessVolume.profile = profiles[profileIndex];
        }
    }
}
