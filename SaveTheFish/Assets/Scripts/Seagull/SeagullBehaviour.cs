using ManasparkAssets.LogicStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seagull
{
    [RequireComponent(typeof(EntityAnimatorExtension))]
    public class SeagullBehaviour : MonoBehaviour, IEntityAgent
    {
        private EntityAnimatorExtension _entityAnimatorExtension;

        private EntityAnimatorExtension entityAnimatorExtension
        {
            get
            {
                if(_entityAnimatorExtension == null)
                {
                    _entityAnimatorExtension = GetComponent<EntityAnimatorExtension>();
                }
                return _entityAnimatorExtension;
            }
        }

        [SerializeField]
        private float flyDuration;

        public Spawner startPoint { get; set; }
        public Spawner landPoint { get; set; }
        public Spawner endPoint { get; set; }

        private float currentTime;
        private Bezier currentTrajectory;

        private void Start()
        {
            transform.position = startPoint.origin;
            transform.forward = startPoint.forward;
        }

        public void StartFly()
        {
            entityAnimatorExtension.SetAnimatorTrigger("fly",0);
            currentTime = 0;
            currentTrajectory = Bezier.CubicBezier(
                startPoint.origin, startPoint.forward, 
                landPoint.backward, landPoint.origin);
        }

        public void FlyToLandingPoint()
        {
            currentTime += Time.deltaTime;
            float t = currentTime / flyDuration;
            transform.position = currentTrajectory.Calculate(t);
            transform.forward = currentTrajectory.CalculateForward(t);
        }
        
        public bool IsCloseToLandingPoint()
        {
            return false;
        }

        public void Landing()
        {

        }

        public bool IsOnGround()
        {
            return false;
        }
    }

}