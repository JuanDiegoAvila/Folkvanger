using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerAttack : MonoBehaviour
    {

        public Animator animator;
        public AudioSource audioSource;
        public Transform attackPoint;

        public LayerMask enemyLayers;

        public float attackRange = 0.5f;

        public float attackRate = 2f;
        float nextAttackTime = 0f;

        private void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Space))
            {
                Attack((int)moveHorizontal, (int)moveVertical);
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }

        void Attack(int moveHorizontal, int moveVertical)
        {
            if (moveHorizontal != 0 && moveVertical == 0)
            {
                animator.SetTrigger("rightAttack");
                audioSource.Play();
            }
            if (moveVertical > 0 && moveHorizontal == 0)
            {
                animator.SetTrigger("upAttack");
                audioSource.Play();
            }
            if (moveVertical < 0 && moveHorizontal == 0)
            {
                animator.SetTrigger("downAttack");
                audioSource.Play();
            }
            if (moveVertical != 0 && moveHorizontal != 0)
            {
                animator.SetTrigger("rightAttack");
                audioSource.Play();
            }
            if (moveVertical == 0 && moveHorizontal == 0)
            {
                animator.SetTrigger("rightAttack");
                audioSource.Play();
            }

            // Detect enemies in range of attack - OBJECTS
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Deal damage to enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<IDamageable>().TakeDamage(20);
            }
        }

        void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}