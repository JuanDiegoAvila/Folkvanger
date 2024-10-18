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
        public int woodPerHit = 20;

        public float coolingTime = 50f;
        public float currentCoolingTime = 0f;

        public bool isChoped;

        void Start()
        {
            currentHits = maxHits;
            isChoped = false;
        }

        void Update()
        {
            if (currentHits <= 0)
            {
                currentCoolingTime += Time.deltaTime;
                if (currentCoolingTime >= coolingTime)
                {
                    animator.SetBool("isChopped", false);
                    isChoped = false;
                    gameObject.GetComponent<Collider2D>().enabled = true;
                    currentHits = maxHits;
                    currentCoolingTime = 0f;
                }
            }
        }

        public void TakeDamageWithoutResources()
        {
            if (!gameObject.GetComponent<Collider2D>().enabled)
            {
                return;
            }

            audioSource.Play();
            currentHits -= 1;
            animator.SetTrigger("isHit");

            if (currentHits <= 0)
            {
                isChoped = true;
                animator.SetBool("isChopped", true);
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }

        public void TakeDamage(int damage, Vector2 direction)
        {
            if (!gameObject.GetComponent<Collider2D>().enabled)
            {
                return;
            }

            audioSource.Play();
            resourceManager.AddWood(woodPerHit);

            currentHits -= 1;
            animator.SetTrigger("isHit");

            if (currentHits <= 0)
            {
                isChoped = true;
                animator.SetBool("isChopped", true);
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}