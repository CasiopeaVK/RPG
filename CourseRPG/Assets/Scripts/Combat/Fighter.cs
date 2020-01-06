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
        float timeSinceLastAttack = Mathf.Infinity;
        [SerializeField] float timeBetweenAttacks = 0.25f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        Weapon currentWeapon = null;

        public void Start()
        {
            playerMover = GetComponent<Mover>();
            EquipWeapon(defaultWeapon);
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);

        }

        public void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;

            float distanceToTarget = Vector3.Distance(target.transform.position, gameObject.transform.position);
            if (distanceToTarget >= currentWeapon.GetWeaponRange())
            {
                playerMover.MoveTo(target.transform.position, 1f);
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

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<Scheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        // Animation Event
        public void Hit()
        {
            if (target == null) return;
            if (currentWeapon.hasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetWeaponDamage());
            }
        }

        public void Shoot()
        {
            Hit();
        }
    }
}
