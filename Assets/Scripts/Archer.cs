using UnityEngine;

namespace Assets.Scripts
{
    public class Archer : MonoBehaviour
    {
        public GameObject arrowPrefab;
        public Transform shootPoint;
        private Animator animator;

        public AudioSource audioSource;

        public LayerMask enemyLayers;
        public float detectionRadius = 5.0f;
        public float shootingInterval = 2.0f;
        private float timeSinceLastShot = 0f;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shootingInterval)
            {
                CheckForEnemiesAndShoot();
                timeSinceLastShot = 0f;
            }
        }

        void CheckForEnemiesAndShoot()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayers);

            if(hitEnemies.Length == 0)
            {
                return;
            }

            var enemy = OrderByDistance(hitEnemies);
            Vector3 direction = enemy.transform.position - transform.position;
            ShootArrow(direction.normalized);
        }

        Collider2D OrderByDistance(Collider2D[] hitEnemies)
        {
            Collider2D closestEnemy = null;
            float minDistance = Mathf.Infinity;

            foreach (Collider2D enemy in hitEnemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        void ShootArrow(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Voltear sprite del arquero dependiendo de la dirección
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (angle < -157.5f || angle > 157.5f)
            {
                animator.SetTrigger("ShootFront");
            }
            else if (angle > 112.5f)
            {
                animator.SetTrigger("ShootDiagonalUp");
            }
            else if (angle > 67.5f)
            {
                animator.SetTrigger("ShootUp");
            }
            else if (angle > 22.5f)
            {
                animator.SetTrigger("ShootDiagonalUp");
            }
            else if (angle > -22.5f)
            {
                animator.SetTrigger("ShootFront");
            }
            else if (angle > -67.5f)
            {
                animator.SetTrigger("ShootDiagonalDown");
            }
            else if (angle > -112.5f)
            {
                animator.SetTrigger("ShootDown");
            }
            else if (angle > -157.5f)
            {
                animator.SetTrigger("ShootDiagonalDown");
            }

            audioSource.Play();
            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.Euler(0, 0, angle));
            arrow.GetComponent<Arrow>().SetDirection(direction);
        }

        void OnDrawGizmosSelected()
        {
            if (shootPoint == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(shootPoint.position, detectionRadius);
        }
    }
}