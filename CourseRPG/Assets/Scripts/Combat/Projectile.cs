using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Health target = null;
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = true;
        float damage = 0f;

        void Start()
        {
            transform.LookAt(GetAimPosition());
        }

        void Update()
        {
            if (target == null) return;
            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimPosition());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimPosition()
        {
            CapsuleCollider capsule = target.GetComponent<CapsuleCollider>();
            if (capsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * capsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (other.GetComponent<Health>().IsDead()) return;
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
