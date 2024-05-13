using System.Resources;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tree : MonoBehaviour, IDamageable
    {
        public Animator animator;
        public AudioSource audioSource;
        public ResourceManager resourceManager;

        public int maxHits = 3;
        public int currentHits;

        public float coolingTime = 50f;
        public float currentCoolingTime = 0f;

        void Start()
        {
            currentHits = maxHits;
        }

        void Update()
        {
            if (currentHits <= 0)
            {
                currentCoolingTime += Time.deltaTime;
                if (currentCoolingTime >= coolingTime)
                {
                    animator.SetBool("isChopped", false);
                    gameObject.GetComponent<Collider2D>().enabled = true;
                    currentHits = maxHits;
                    currentCoolingTime = 0f;
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (!gameObject.GetComponent<Collider2D>().enabled)
            {
                return;
            }

            audioSource.Play();
            resourceManager.AddWood(5);
            // Trees don't take damage, they just lose hits
            currentHits -= 1;
            animator.SetTrigger("isHit");

            if (currentHits <= 0)
            {
                animator.SetBool("isChopped", true);
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}