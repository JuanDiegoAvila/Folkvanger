using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour, IDamageable
    {


        public AudioSource hurtSound;
        public GameObject endGameCanva;

        public GameObject deathAnimationPrefab;
        public float speed = 3f; // Velocidad del jugador

        private Rigidbody2D rb; // Referencia al componente Rigidbody2D
        public Animator animator;

        public int maxHealth = 100;
        public int currentHealth;

        public Slider slider;

        void Start()
        {
            // Obtener la referencia al componente Rigidbody2D
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            currentHealth = maxHealth;
        }

        void Update()
        {
            // Obtener las entradas de movimiento horizontal y vertical
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Calcular el vector de movimiento basado en las entradas
            Vector2 movement = new(moveHorizontal, moveVertical);

            // Normalizar el vector para que la velocidad diagonal no sea mayor
            movement.Normalize();

            // Mover el jugador multiplicando por la velocidad y el tiempo
            rb.velocity = movement * speed;
            if (movement.magnitude > 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            if (moveHorizontal > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            if (moveHorizontal < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        public void AddHealth(int health)
        {
            currentHealth += health;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            // Update health bar
            slider.value = currentHealth / (float)maxHealth;
        }

        public void TakeDamage(int damage, Vector2 direction)
        {
            hurtSound.Play();

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
            // Ocultar el jugador
            gameObject.SetActive(false);

            // Instanciar el objeto de animación de muerte en la posición del jugador
            Instantiate(deathAnimationPrefab, (transform.position + new Vector3(0, 0.5f, 0)), Quaternion.identity);

            // Activar el canvas de fin de juego
            _ = new WaitForSeconds(1.5f);

            endGameCanva.SetActive(true);
            
        }
    }
}