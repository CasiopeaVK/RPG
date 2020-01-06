using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Saving;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
        NavMeshAgent navMeshAgent;
        Animator playerAnimator;
        Health health;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerAnimator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }
        void Update()
        {   
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 point, float speedFraction)
        {   
            GetComponent<Scheduler>().StartAction(this);
            MoveTo(point, speedFraction);
        }

        public void MoveTo(Vector3 point, float speedFraction)
        {
            navMeshAgent.destination = point;
            navMeshAgent.speed = maxSpeed * speedFraction;
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

        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
