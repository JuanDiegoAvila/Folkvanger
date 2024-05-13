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
            }
            if (moveVertical > 0 && moveHorizontal == 0)
            {
                animator.SetTrigger("upAttack");
            }
            if (moveVertical < 0 && moveHorizontal == 0)
            {
                animator.SetTrigger("downAttack");
            }
            if (moveVertical != 0 && moveHorizontal != 0)
            {
                animator.SetTrigger("rightAttack");
            }
            if (moveVertical == 0 && moveHorizontal == 0)
            {
                animator.SetTrigger("rightAttack");
            }

            audioSource.Play();

            // Detect enemies in range of attack - OBJECTS
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Deal damage to enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                IDamageable damageable = enemy.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(20);
                }

                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 forceDirection = enemy.transform.position - transform.position;
                    forceDirection.Normalize(); // Asegura que la dirección tenga longitud 1
                    float forceMagnitude = 500f; // Ajusta la magnitud de la fuerza según necesites
                    rb.AddForce(forceDirection * forceMagnitude);
                }
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