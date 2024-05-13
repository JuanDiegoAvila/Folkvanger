using UnityEngine;

namespace Assets.Scripts
{
    public class Arrow : MonoBehaviour
    {
        public float speed = 20f; // Ajusta según necesites
        private float lifeTime = 0.5f; // Tiempo de vida de la flecha en segundos

        public void SetDirection(Vector3 dir)
        {
            var direction = dir.normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }

        void Update()
        {
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                lifeTime -= Time.deltaTime;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<IDamageable>().TakeDamage(10);
                Destroy(gameObject);
            }
        }
    }
}