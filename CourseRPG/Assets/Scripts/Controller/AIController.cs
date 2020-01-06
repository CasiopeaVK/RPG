using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;
using System;

namespace RPG.Controll
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float wayPointTollerance = 1f;
        [SerializeField] float waypointDwellTime = 2f;
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] PatrolPath patrolPath;

        int currentWayPointIndex = 0;
        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        Vector3 enemyLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();

            enemyLocation = transform.position;
        }

        void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange() && fighter.CanAttack(player))
            {
                AttackBehavior();
                timeSinceLastSawPlayer = 0;
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                print("suspicion");
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = enemyLocation;
            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWayPoint();
                }

                nextPosition = GetCurrentWayPoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                GetComponent<Mover>().StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.transform.GetChild(currentWayPointIndex).position;
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % patrolPath.transform.childCount;
        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWaypoint < wayPointTollerance;
        }

        private void SuspicionBehavior()
        {
            GetComponent<Scheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            fighter.Attack(player);
        }

        private bool InAttackRange()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

        //Called by unity
        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}