using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SoundManager : SingletonSaved<SoundManager>
    {
        private Dictionary<string, RandomSoundPlayer> soundPlayers = new Dictionary<string, RandomSoundPlayer>();

        void Start()
        {
            var components = GetComponentsInChildren<RandomSoundPlayer>(true);
            foreach (var component in components)
            {
                soundPlayers.Add(component.gameObject.name, component);
            }
        }

        public void PlaySound(string soundCategory)
        {
            RandomSoundPlayer player;
            if (soundPlayers.TryGetValue(soundCategory, out player))
            {
                player.PlaySound();
            }
        }

        public static void Play(string soundCategory)
        {
            Instance.PlaySound(soundCategory);
        }
    }
}