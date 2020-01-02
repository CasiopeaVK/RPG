using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;
        Animator playerAnimator;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerAnimator = GetComponent<Animator>();
        }
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 point)
        {   
            GetComponent<Scheduler>().StartAction(this);
            MoveTo(point);
        }

        public void MoveTo(Vector3 point)
        {
            navMeshAgent.destination = point;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            playerAnimator.SetFloat("forwardSpeed", speed);
        }
    }
}
