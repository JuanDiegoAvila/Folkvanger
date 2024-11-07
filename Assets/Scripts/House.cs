using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class House : MonoBehaviour
    {
        [SerializeField]
        protected List<GameObject> images;
        [SerializeField]
        protected Sprite defaultImage;
        [SerializeField]
        protected Transform startPosition;

        public Estado estado = new Estado();

        private ResourceManager manager;
        public GameObject comprarPeon;
        public GameObject objetoComprar;

        public Sprite[] iconosCompra;
        public SpriteRenderer iconoCompra;

        public GameObject TextoPrecio;
        public GameObject peonPrefab;
        public BoxCollider2D areaLimit;

        private int limitePeones = 3;
        private int peonesActuales = 0;

        public AudioSource audioSource;
        public AudioSource collectAudioSource;

        private int Precio;
        private Animator animator;
        private bool estaCompleto;
        private bool adentroDeCollider;

        public enum Estado
        {
            idle = 1,
            mid = 2,
            completo = 3,
            peonesLlenos = 4
        }

        private void Start()
        {
            estado = Estado.idle;
            estaCompleto = false;
            peonesActuales = 0;

            iconoCompra.sprite = iconosCompra[0];

            var textoPrecio = TextoPrecio.GetComponent<TextMeshPro>();
            manager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
            animator = GetComponentInParent<Animator>();
            Precio = Convert.ToInt32(textoPrecio.text);
        }

        private void Update()
        {
            if (adentroDeCollider && Input.GetKeyDown(KeyCode.C))
            {
                if(!estaCompleto)
                    ComprarMejora();
                else
                    ComprarPeon();
            }

            switch (estado)
            {
                case Estado.idle:
                    animator.SetBool("isMid", false);
                    animator.SetBool("isComplete", false);
                    break;
                case Estado.mid:
                    animator.SetBool("isMid", true);
                    animator.SetBool("isComplete", false);
                    break;
                case Estado.completo:
                    animator.SetBool("isMid", false);
                    animator.SetBool("isComplete", true);
                    break;
            }
        }

        void ComprarMejora()
        {
            if (manager.ComprarMejora(Precio))
            {
                audioSource.Play();
                switch (estado)
                {
                    case Estado.idle:
                        estado = Estado.mid;
                        Precio = 200;
                        TextoPrecio.GetComponent<TextMeshPro>().text = "200";
                        break;
                    case Estado.mid:
                        estado = Estado.completo;
                        //objetoComprar.SetActive(false);
                        iconoCompra.sprite = iconosCompra[1];
                        TextoPrecio.GetComponent<TextMeshPro>().text = "50";
                        estaCompleto = true;
                        break;
                }
            }
        }

        void ComprarPeon()
        {
            if(peonesActuales >= limitePeones)
            {
                estado = Estado.peonesLlenos;
                return;
            }

            if(manager.ComprarMejoraOro(5))
            {
                audioSource.Play();
                var peon = Instantiate(peonPrefab, startPosition.position, Quaternion.identity);
                peon.GetComponent<Pawn>().InstanciarPeon(gameObject, manager, areaLimit);

                images[peonesActuales].GetComponent<SpriteRenderer>().sprite = defaultImage;
                peonesActuales++;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Pawn"))
            {
                if (collision.gameObject.TryGetComponent<Pawn>(out var pawn))
                {
                    pawn.PawnArrivedHome();
                    pawn.transform.position = startPosition.position;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                objetoComprar.SetActive(true);
                if (estaCompleto)
                    comprarPeon.SetActive(true);

                adentroDeCollider = true;
            }

            if (collision.gameObject.CompareTag("Pawn"))
            {
                if (collision.gameObject.TryGetComponent<Pawn>(out var pawn))
                {
                    pawn.DisableSpriteRender();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                objetoComprar.SetActive(false);
                if (estaCompleto)
                    comprarPeon.SetActive(false);

                adentroDeCollider = false;
            }

            if (collision.gameObject.CompareTag("Pawn"))
            {
                if (collision.gameObject.TryGetComponent<Pawn>(out var pawn))
                {
                    pawn.EnableSpriteRender();
                }
            }
        }
    }
}