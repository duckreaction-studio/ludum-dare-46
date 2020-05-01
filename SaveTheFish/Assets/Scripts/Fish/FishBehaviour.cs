using DUCK.Tween;
using DUCK.Tween.Easings;
using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fish
{

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

        private Dictionary<string, BlendShapeAnimation> tweens = new Dictionary<string, BlendShapeAnimation>();

        private void Start()
        {
            var mouthAnimation = new BlendShapeAnimation(fishSkin, 3, 0, maxBlend, 1.7f, Ease.PingPong.Linear);
            mouthAnimation.Play(-1);
        }

        void ActionSuccess()
        {
            SoundManager.Play("ActionDone");
        }

        void ActionFail()
        {
            SoundManager.Play("Defeat");
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
            BlendShapeAnimation tween = new BlendShapeAnimation(fishSkin, index, fishSkin.GetBlendShapeWeight(index), endValue, blendTransition);
            tweens["fish-" + index] = tween;
            tween.Play();

            index = TargetToBendShapeIndex("press");
            for (var i = 0; i < eyes.Length; i++)
            {
                tween = new BlendShapeAnimation(eyes[i], index, fishSkin.GetBlendShapeWeight(index), endValue, blendTransition);
                tweens["eye-" + i + "-" + index] = tween;
                tween.Play();
            }
        }

        int TargetToBendShapeIndex(string target)
        {
            switch (target)
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

}