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
        private Animator actorAnimator;
        [SerializeField]
        private float flySpeed = 1.5f;
        [SerializeField]
        private float landingSpeed = 1f;
        [SerializeField]
        private float landingPointDistance = 0.5f;

        public Spawner startPoint { get; set; }
        public Spawner landPoint { get; set; }
        public Spawner endPoint { get; set; }

        private float progress;
        private Bezier currentTrajectory;
        private float speed;

        private void Start()
        {
            transform.position = startPoint.origin;
            transform.forward = startPoint.forwardDirection;

            actorAnimator.SetTrigger("fly");
        }

        public void StartFly()
        {
            Debug.Log("Start fly");
            transform.position = startPoint.origin;
            transform.forward = startPoint.forwardDirection;
            entityAnimatorExtension.SetAnimatorTrigger("fly",0);
            actorAnimator.ResetTrigger("prepareLanding");
            entityAnimatorExtension.SetAnimatorBool("isOnGround", false, 0);
            progress = 0;
            speed = flySpeed;
            currentTrajectory = Bezier.CubicBezier(
                startPoint.origin, startPoint.forward, 
                landPoint.backward, landPoint.origin);

            actorAnimator.SetTrigger("fly");
        }

        public void FlyToLandingPoint()
        {
            progress = currentTrajectory.CalculateTByLength(progress,flySpeed * Time.deltaTime);
            if (progress <= 1f)
            {
                transform.position = currentTrajectory.Calculate(progress);
                transform.forward = currentTrajectory.CalculateForward(progress);
            }
        }
        
        public bool IsCloseToLandingPoint()
        {
            return (landPoint.origin - transform.position).sqrMagnitude < landingPointDistance * landingPointDistance;
        }

        public void StartLanding()
        {
            Debug.Log("Start landing");
            speed = landingSpeed;
            actorAnimator.SetTrigger("prepareLanding");
        }

        public void Landing()
        {
            FlyToLandingPoint();
            entityAnimatorExtension.SetAnimatorBool("isOnGround", progress >= 1f, 0);
        }

        public void OnLand()
        {
            actorAnimator.SetTrigger("land");
            actorAnimator.ResetTrigger("fly");
        }
    }

}