using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public int maxHealth = 100;
        public int currentHealth;

        public Slider slider;

        private RoundManager roundManager;

        void Start()
        {
            currentHealth = maxHealth;
            roundManager = FindObjectOfType<RoundManager>();
        }


        public void TakeDamage(int damage)
        {

            currentHealth -= damage;

            // Play hurt animation

            if (currentHealth <= 0)
            {
                Die();
            }

            // Update health bar
            slider.value = currentHealth / (float)maxHealth;
        }

        void Die()
        {
            roundManager.OnEnemyKilled();
            Destroy(gameObject);
        }
    }
}