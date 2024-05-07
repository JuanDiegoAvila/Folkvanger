using UnityEngine;

namespace Assets.Scripts
{
    public class Archer : MonoBehaviour
    {
        public GameObject arrowPrefab;
        public Transform shootPoint;
        private Animator animator;

        private Vector3 lastEnemyPosition;

        private bool hasTarget = false;

        private float timeSinceLastShot = 0f;
        public float shootingInterval = 2.0f; 

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (hasTarget)
            {
                timeSinceLastShot += Time.deltaTime;
                if (timeSinceLastShot >= shootingInterval)
                {
                    Vector3 direction = lastEnemyPosition - transform.position;
                    ShootArrow(direction.normalized);
                    timeSinceLastShot = 0f;
                }
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Enemy")) 
            {
                lastEnemyPosition = other.transform.position;
                hasTarget = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                hasTarget = false;
            }
        }

        void ShootArrow(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Disparo hacia arriba
            if (angle > 67.5f && angle <= 112.5f)
            {
                animator.SetTrigger("ShootUp");
            }
            // Disparo hacia abajo
            else if (angle > -112.5f && angle <= -67.5f)
            {
                animator.SetTrigger("ShootDown");
            }
            // Disparo frontal
            else if (angle > -67.5f && angle <= 0 || angle > 0 && angle <= 67.5f)
            {
                animator.SetTrigger("ShootFront");
            }
            // Disparo diagonal hacia abajo
            else if (angle > -167.5f && angle <= -112.5f)
            {
                animator.SetTrigger("ShootDiagonalDown");
            }
            // Disparo diagonal hacia arriba
            else if (angle > 112.5f && angle <= 167.5f)
            {
                animator.SetTrigger("ShootDiagonalUp");
            }

            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.Euler(0, 0, angle));
            arrow.GetComponent<Arrow>().SetDirection(direction);
        }
    }
}