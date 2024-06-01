using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class PlayerAttack : MonoBehaviour
    {

        public Animator animator;
        public AudioSource audioSource;
        public Transform attackPoint;

        private int currentDirection;

        public LayerMask enemyLayers;

        public float attackRange = 0.5f;

        public float attackRate = 2f;
        float nextAttackTime = 0f;

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        }

        private Direction lastDirection = Direction.None;

        private void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Actualizar la última dirección basada en las entradas del usuario
            if (moveHorizontal > 0)
            {
                lastDirection = Direction.Right;
            }
            else if (moveHorizontal < 0)
            {
                lastDirection = Direction.Left;
            }

            if (moveVertical > 0)
            {
                lastDirection = Direction.Up;
            }
            else if (moveVertical < 0)
            {
                lastDirection = Direction.Down;
            }

            if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Space))
            {
                Attack((int)moveHorizontal, (int)moveVertical);
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }

        void Attack(int moveHorizontal, int moveVertical)
        {

            switch (lastDirection)
            {
                case Direction.Up:
                    animator.SetTrigger("upAttack");
                    break;
                case Direction.Down:
                    animator.SetTrigger("downAttack");
                    break;
                case Direction.Left:
                    animator.SetTrigger("rightAttack");
                    break;
                case Direction.Right:
                    animator.SetTrigger("rightAttack");
                    break;
                default:
                    animator.SetTrigger("rightAttack"); // Puedes decidir un ataque por defecto
                    break;
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
                    Vector2 forceDirection = enemy.transform.position - transform.position;
                    forceDirection.Normalize();
                    damageable.TakeDamage(20, forceDirection);
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