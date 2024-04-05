using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private readonly float tiempoRecuperacion = 30f;
    private float tiempoActual;

    private readonly float tiempoGolpe = 1f;
    private float tiempoActualGolpe = 0;

    public ResourceManager manager;
    private Animator animator;
    private PlayerController playerController;

    public int hits = 3;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        tiempoActual += Time.deltaTime;
        if (tiempoActual >= tiempoRecuperacion)
        {
            animator.SetBool("isChopped", false);

            gameObject.GetComponent<Collider2D>().enabled = true;
            tiempoActual = 0;
            hits = 3;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController.isAttacking)
            {
                Chop();
            }
        }
    }

    private void Chop()
    {
        if(tiempoActualGolpe < tiempoGolpe)
        {
            manager.AddWood(5);
            hits--;
            if (hits <= 0)
            {
                animator.SetBool("isChopped", true);
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                animator.SetTrigger("isHit");
            }
            tiempoActualGolpe = 0;
        }

        tiempoActualGolpe += Time.deltaTime;
    }
}
