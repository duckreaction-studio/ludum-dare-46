using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fish
{
    public class FishSparkle : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particleSystem;
        public void ActionSuccess()
        {
            var mod = particleSystem.main;
            mod.loop = false;
            particleSystem.Play();
        }
    }
}