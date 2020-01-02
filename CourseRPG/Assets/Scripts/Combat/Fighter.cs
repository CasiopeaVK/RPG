using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        Health target;
        Mover playerMover;
        float timeSinceLastAttack = 0;
        [SerializeField] float rangeDistance = 2f;
        [SerializeField] float timeBetweenAttacks = 0.25f;
        [SerializeField] float weaponDamage = 30f;
        public void Start()
        {
            playerMover = GetComponent<Mover>();
        }
        public void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;

            float distanceToTarget = Vector3.Distance(target.transform.position, gameObject.transform.position);
            if (distanceToTarget >= rangeDistance)
            {
                playerMover.MoveTo(target.transform.position);
            }
            else
            {
                playerMover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<Scheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        // Animation Event
        void Hit()
        {
            if(target == null)
            {
                print("Misle");
                return;
            }
            target.TakeDamage(weaponDamage);
        }
    }
}
