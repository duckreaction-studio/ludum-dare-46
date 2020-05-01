using UnityEngine;
using DUCK.Tween;
using DUCK.Tween.Easings;

namespace Fish
{
    public class FishEyeMouseTracker : MonoBehaviour
    {
        public const int BLEND_DILATE_IDX = 0;

        [SerializeField]
        private float maxDistance = 60f;
        [SerializeField]
        private float dilateDuration = 0.2f;
        [SerializeField]
        private float maxDilate = 100f;

        private bool dilate = false;
        private BlendShapeAnimation currentAnimation;

        public float sqrMaxDistance
        {
            get { return maxDistance * maxDistance; }
        }

        private SkinnedMeshRenderer _skin;
        public SkinnedMeshRenderer skin
        {
            get
            {
                if(_skin == null)
                {
                    _skin = GetComponent<SkinnedMeshRenderer>();
                }
                return _skin;
            }
        }


        void Update()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(skin.bounds.center);
            float sqrDistance = (screenPos - Input.mousePosition).sqrMagnitude;
            if(sqrDistance < sqrMaxDistance)
            {
                if(dilate == false)
                {
                    dilate = true;
                    CreateAnimation(maxDilate);
                }
            }
            else
            {
                if(dilate == true)
                {
                    dilate = false;
                    CreateAnimation(0);
                }
            }
        }

        private void CreateAnimation(float toValue)
        {
            float fromValue = skin.GetBlendShapeWeight(BLEND_DILATE_IDX);
            if(currentAnimation != null)
            {
                currentAnimation.Abort();
            }
            currentAnimation = new BlendShapeAnimation(skin, BLEND_DILATE_IDX, fromValue, toValue, dilateDuration, Ease.Cubic.Out );
            currentAnimation.Play();
        }
    }
}