using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
namespace RPG.Combat
{
    
    public class Fighter : MonoBehaviour
    {
        Transform target;
        Mover playerMover;
        [SerializeField] float rangeDistance = 2f;

        public void Start()
        {
            playerMover = GetComponent<Mover>();
        }
        public void Update()
        {
            if(target != null)
            {
                float distanceToTarget = Vector3.Distance(target.position, gameObject.transform.position);
                if(distanceToTarget >= rangeDistance)
                {
                    playerMover.MoveTo(target.position);
                }
                else
                {
                    playerMover.Stop();
                }
                
            }
        }
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<Scheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void CancelAttack()
        {
            target = null;
        }
    }
}
