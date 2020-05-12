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
        private float flyDuration = 2f;
        [SerializeField]
        private float landingDuration = 0.3f;
        [SerializeField]
        private float landingPointDistance = 0.5f;

        public Spawner startPoint { get; set; }
        public Spawner landPoint { get; set; }
        public Spawner endPoint { get; set; }

        private float currentTime;
        private Bezier currentTrajectory;
        private float landingStartOffset;

        private void Start()
        {
            transform.position = startPoint.origin;
            transform.forward = startPoint.forwardDirection;
        }

        public void StartFly()
        {
            Debug.Log("Start fly");
            transform.position = startPoint.origin;
            transform.forward = startPoint.forwardDirection;
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
            if (t <= 1f)
            {
                transform.position = currentTrajectory.Calculate(t);
                transform.forward = currentTrajectory.CalculateForward(t);
            }
        }
        
        public bool IsCloseToLandingPoint()
        {
            return (landPoint.origin - transform.position).sqrMagnitude < landingPointDistance * landingPointDistance;
        }

        public void StartLanding()
        {
            Debug.Log("Start landing");
            landingStartOffset = currentTime / flyDuration;
            currentTime = 0;
        }

        public void Landing()
        {
            currentTime += Time.deltaTime;
            float t = (1 - landingStartOffset) * (currentTime / landingDuration) + landingStartOffset;
            if (t <= 1f)
            {
                transform.position = currentTrajectory.Calculate(t);
                transform.forward = currentTrajectory.CalculateForward(t);
            }
        }

        public bool IsOnGround()
        {
            return false;
        }
    }

}