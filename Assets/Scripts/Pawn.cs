using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Pawn : MonoBehaviour, IDamageable
    {
        public int maxHealth = 100;
        private int currentHealth;
        public Slider slider;

        private Resource resource;

        [SerializeField]
        private ResourceManager manager;

        public float speed;
        private Transform target;
        private Animator animator;
        private bool isHoldingObject = false;

        private bool collectingWood = false;
        private Tree currentTree;

        private float restingTime = 10f;
        private float currentRestingTime = 0f;

        public GameObject origin;

        private PawnState state;

        public BoxCollider2D areaLimit;

        [SerializeField]
        private float chopCooldown = 2f; // Tiempo entre golpes (2 segundos por ejemplo)
        private float chopCooldownTimer = 0f;

        void Start()
        {
            state = PawnState.Idle;
            currentHealth = maxHealth;
            animator = GetComponentInChildren<Animator>();
        }

        public void InstanciarPeon(GameObject origen, ResourceManager resourceManager, BoxCollider2D limit)
        {
            origin = origen;
            manager = resourceManager;
            areaLimit = limit;
            state = PawnState.Idle;
            chopCooldownTimer = 0f;
        }

        private void Update()
        {
            if (chopCooldownTimer > 0)
            {
                chopCooldownTimer -= Time.deltaTime;
            }

            if (isHoldingObject && state != PawnState.Resting)
            {
                target = origin.transform;
                state = PawnState.Walking;
                animator.SetBool("isRunning", true);
            }

            if (collectingWood && currentTree != null)
            {
                state = PawnState.Chopping;

                // Solo permite golpear si el cooldown ha terminado
                if (chopCooldownTimer <= 0)
                {
                    // Realiza el golpe
                    currentTree.TakeDamageWithoutResources();
                    animator.SetTrigger("chop"); // Ejecuta la animación de chop

                    // Reiniciar el timer del cooldown
                    chopCooldownTimer = chopCooldown;

                    // Verificar si el árbol ha sido talado completamente
                    if (currentTree.currentHits <= 0)
                    {
                        collectingWood = false;
                        resource = new ()
                        {
                            resourceType = ResourceType.Wood,
                            amount = currentTree.woodPerHit * currentTree.maxHits
                        };
                        isHoldingObject = true;
                        currentTree = null;
                        state = PawnState.Walking;
                        animator.SetBool("isRunning", true);
                    }
                }
            }

            if (state == PawnState.Walking)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                transform.position += (Vector3)direction * speed * Time.deltaTime;

                if (Vector2.Distance(transform.position, target.position) < 0.4f && target != origin.transform)
                {
                    state = PawnState.Idle;
                    animator.SetBool("isRunning", false);

                    if (target.gameObject.CompareTag("Resource"))
                    {
                        if (target.gameObject.TryGetComponent<MinaDeOro>(out var mina))
                        {
                            var oro = mina.PawnCollect();
                            resource = new()
                            {
                                resourceType = ResourceType.Gold,
                                amount = oro
                            };
                            isHoldingObject = true;
                        }

                        if (target.gameObject.TryGetComponent<Tree>(out var tree))
                        {
                            currentTree = tree;
                            collectingWood = true;
                            animator.SetTrigger("chop");
                        }
                    }
                }
            }

            if (state == PawnState.Resting)
            {
                currentRestingTime += Time.deltaTime;

                if (currentRestingTime >= restingTime)
                {
                    state = PawnState.Idle;
                    target = null;
                    currentRestingTime = 0f;
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            if(state != PawnState.Resting && target == null)
            {
                // find nearest resource
                GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");

                var noActivos = resources.Where(resource =>
                {

                    if (!areaLimit.bounds.Contains(resource.transform.position))
                    {
                        return true;
                    }

                    MinaDeOro mina = resource.GetComponentInChildren<MinaDeOro>();
                    if (mina != null)
                    {
                        return mina.estado != MinaDeOro.Estado.completo;
                    }

                    Tree tree = resource.GetComponentInChildren<Tree>();
                    if (tree != null)
                    {
                        return tree.isChoped;
                    }

                    return false;
                });
                
                resources = resources.Except(noActivos).ToArray();

                float minDistance = Mathf.Infinity;
                GameObject nearestResource = null;

                foreach (GameObject r in resources)
                {
                    float distance = Vector2.Distance(transform.position, r.transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestResource = r;
                    }
                }

                if (nearestResource != null)
                {
                    target = nearestResource.transform;
                    state = PawnState.Walking;
                    animator.SetBool("isRunning", true);
                }
                else
                {
                    state = PawnState.Idle;
                    animator.SetBool("isRunning", false);

                }
            }
        }

        public void TakeDamage(int damage, Vector2 direction)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }

            slider.value = currentHealth / (float)maxHealth;
        }

        public void PawnArrivedHome()
        {
            state = PawnState.Resting;
            target = null;

            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (isHoldingObject)
            {
                switch (resource.resourceType)
                {
                    case ResourceType.Wood:
                        manager.AddWood(resource.amount);
                        break;
                    case ResourceType.Gold:
                        manager.AddGold(resource.amount);
                        break;
                    default:
                        break;
                }

                isHoldingObject = false;
            }
        }

        public void DisableSpriteRender()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        public void EnableSpriteRender()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        private void OnDrawGizmos()
        {
            // Dibujar línea si hay un target definido
            if (target != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, target.position);

                // También puedes dibujar una esfera para visualizar el target
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(target.position, 0.5f);
            }
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}