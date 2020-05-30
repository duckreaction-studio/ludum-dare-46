using ManasparkAssets.LogicStateMachine;
using System;
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
        [SerializeField]
        private float walkSpeed = 0.4f;

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
            entityAnimatorExtension.SetAnimatorBool("isOnFish", false, 0);
            actorAnimator.ResetTrigger("prepareLanding");
            entityAnimatorExtension.SetAnimatorTrigger("fly", 0);
            entityAnimatorExtension.SetAnimatorBool("isOnGround", false, 0);

            CreateTrajectory(flySpeed,startPoint,landPoint);

            actorAnimator.SetTrigger("fly");
        }

        public void FlyToLandingPoint()
        {
            MoveAlongCurrentTrajectory();
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
            actorAnimator.ResetTrigger("walk");
            actorAnimator.ResetTrigger("eat");

            CreateTrajectory(walkSpeed, landPoint, endPoint);

            WaitAndStarkWalk();
        }

        public void WalkToEndPoint()
        {
            MoveAlongCurrentTrajectory();
            entityAnimatorExtension.SetAnimatorBool("isOnFish", progress >= 1f, 0);
        }

        public void StartEating()
        {
            actorAnimator.SetTrigger("eat");
        }

        private void CreateTrajectory(float newSpeed, Spawner p1, Spawner p2)
        {
            progress = 0;
            speed = newSpeed;
            currentTrajectory = Bezier.CubicBezier(
                p1.origin, p1.forward,
                p2.backward, p2.origin);
        }

        private void MoveAlongCurrentTrajectory()
        {
            progress = currentTrajectory.CalculateTByLength(progress, speed * Time.deltaTime);
            if (progress <= 1f)
            {
                transform.position = currentTrajectory.Calculate(progress);
                transform.forward = currentTrajectory.CalculateForward(progress);
            }
        }

        public void StartWalking()
        {
            actorAnimator.SetTrigger("walk");
        }

        private void WaitAndStarkWalk()
        {
            float time = UnityEngine.Random.Range(0.4f, 1.5f);
            entityAnimatorExtension.SetAnimatorTrigger("walk", time);
        }
    }

}