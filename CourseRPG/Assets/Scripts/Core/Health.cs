using UnityEngine;
using RPG.Saving;
namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        public object CaptureState()
        {
            return healthPoints;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void RestoreState(object state)
        {
            float restoreHealth = (float)state;
            healthPoints = restoreHealth;

            if (healthPoints == 0)
            {
                Die();
            }
            else
            {
                isDead = false;
            }
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            
            GetComponent<Animator>().SetTrigger("death");
            GetComponent<Scheduler>().CancelCurrentAction();
        }
    }
}
