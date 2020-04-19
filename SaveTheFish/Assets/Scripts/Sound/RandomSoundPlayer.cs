using GD.MinMaxSlider;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    [Serializable]
    public class SoundList
    {
        [SerializeField]
        private bool _enabled = true;
        public bool enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        [Range(0f, 3f)]
        private float volumeFactor = 1f;

        [SerializeField]
        private AudioClip[] clips;

        public void PlaySound(float volume, float pitch)
        {
            if (enabled)
            {
                var randomSound = clips[Random.Range(0, clips.Length)];
                audioSource.volume = volume * volumeFactor;
                audioSource.pitch = pitch;
                audioSource.Stop();
                audioSource.PlayOneShot(randomSound);
            }
        }
    }

    public class RandomSoundPlayer : MonoBehaviour
    {
        [SerializeField]
        [MinMaxSlider(0f, 1f)]
        public Vector2 volume = new Vector2(1f, 1f);

        [SerializeField]
        [MinMaxSlider(-3f, 3f)]
        public Vector2 pitch = new Vector2(1f, 1f);

        [SerializeField]
        private SoundList[] lists;

        [ContextMenu("PlaySound")]
        public void PlaySound()
        {
            float randomVolume = Random.Range(volume.x, volume.y);
            float randomPitch = Random.Range(pitch.x, pitch.y);

            foreach (var list in lists)
            {
                list.PlaySound(randomVolume, randomPitch);
            }
        }
    }
}