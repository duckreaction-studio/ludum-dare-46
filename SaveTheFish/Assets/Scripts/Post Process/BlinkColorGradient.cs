using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcess
{
    public class BlinkColorGradient : MonoBehaviour
    {
        [SerializeField]
        private float startValue = -200f;
        [SerializeField]
        private float endValue = 200f;

        private PostProcessVolume _volume;
        public PostProcessVolume volume
        {
            get
            {
                if (_volume == null)
                {
                    _volume = GetComponent<PostProcessVolume>();
                }
                return _volume;
            }
        }

        private ColorGrading _colorGrading;
        public ColorGrading colorGrading
        {
            get
            {
                if (_colorGrading == null && volume != null)
                {
                    volume.profile.TryGetSettings<ColorGrading>(out _colorGrading);
                }
                return _colorGrading;
            }
        }

        private float originalValue;
        private float duration = 0;
        private float currentDuration = 0;
        private int repeat;
        private bool play = false;

        public void StartBlink(float duration, int repeat)
        {
            if (colorGrading != null)
            {
                this.duration = duration;
                this.repeat = repeat;
                originalValue = colorGrading.mixerRedOutGreenIn.value;
                currentDuration = 0;
                play = true;
            }
        }

        void Update()
        {
            if (play)
            {
                currentDuration += Time.deltaTime;
                colorGrading.mixerRedOutGreenIn.value = Mathf.Lerp(
                    startValue,
                    endValue,
                    Mathf.PingPong((currentDuration / duration) * repeat, 1f)
                );
                if (currentDuration > duration)
                {
                    play = false;
                    colorGrading.mixerRedOutGreenIn.value = originalValue;
                }
            }
        }
    }
}