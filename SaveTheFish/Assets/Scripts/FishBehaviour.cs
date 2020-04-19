using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeTween
{
    private SkinnedMeshRenderer skin;
    private int blendIndex;
    private float startValue;
    private float endValue;
    private float duration;
    private float delta;
    public bool play { get; private set; }

    public BlendShapeTween(SkinnedMeshRenderer skin, int blendIndex, float startValue, float endValue, float duration)
    {
        this.skin = skin;
        this.blendIndex = blendIndex;
        this.startValue = startValue;
        this.endValue = endValue;
        this.duration = duration;
    }

    public void Update(float _delta)
    {
        if (play)
        {
            delta += _delta;
            if (delta > duration)
            {
                play = false;
                skin.SetBlendShapeWeight(blendIndex, endValue);
            }
            else
            {
                skin.SetBlendShapeWeight(blendIndex, Mathf.SmoothStep(startValue, endValue, delta / duration));
            }
        }
    }

    public void Play()
    {
        this.play = true;
        this.delta = 0;
    }
}

public class FishBehaviour : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer fishSkin;
    [SerializeField]
    private SkinnedMeshRenderer[] eyes; 

    [SerializeField]
    private float maxBlend = 70f;
    [SerializeField]
    private float blendTransition = 0.15f;

    private Dictionary<string, BlendShapeTween> tweens = new Dictionary<string, BlendShapeTween>();

    void Update()
    {
        fishSkin.SetBlendShapeWeight(3, Mathf.SmoothStep(maxBlend, 0, Mathf.PingPong(Time.time, 0.85f)));
        foreach(var item in tweens)
        {
            item.Value.Update(Time.deltaTime);
        }
    }

    void FishStartPress(string target)
    {
        StartBlend(target);
        BroadcastMessage("FishImpact", target, SendMessageOptions.DontRequireReceiver);
    }

    void FishStopPress(string target)
    {
        StartBlend(target, true);
    }

    private void StartBlend(string target, bool revert = false)
    {
        int index = TargetToBendShapeIndex(target);
        float endValue = revert ? 0f : maxBlend;
        BlendShapeTween tween = new BlendShapeTween(fishSkin, index, fishSkin.GetBlendShapeWeight(index), endValue, blendTransition);
        tweens["fish-"+index] = tween;
        tween.Play();

        index = TargetToBendShapeIndex("press");
        for (var i = 0; i < eyes.Length; i++)
        {
            tween = new BlendShapeTween(eyes[i], index, fishSkin.GetBlendShapeWeight(index), endValue, blendTransition);
            tweens["eye-" + i + "-" + index] = tween;
            tween.Play();
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
            case "press":
                return 1;
        }
        return -1;
    }
}
