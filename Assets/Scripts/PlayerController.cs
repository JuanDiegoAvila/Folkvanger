using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador
    public bool isAttacking = false; // Variable para saber si el jugador está atacando

    private Rigidbody2D rb; // Referencia al componente Rigidbody2D

    public Animator animator;

    private AudioSource audioSource;

    void Start()
    {
        // Obtener la referencia al componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Obtener las entradas de movimiento horizontal y vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento basado en las entradas
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

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

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(PerformAttack(moveHorizontal, moveVertical));
        }
    }

    IEnumerator PerformAttack(float moveHorizontal, float moveVertical)
    {
        isAttacking = true;

        if (moveHorizontal != 0 && moveVertical == 0)
        {
            // Activar el Trigger de ataque
            animator.SetTrigger("rightAttack");
            audioSource.Play();
        }
        if (moveVertical > 0 && moveHorizontal == 0)
        {
            // Activar el Trigger de ataque
            animator.SetTrigger("upAttack");
            audioSource.Play();
        }
        if (moveVertical < 0 && moveHorizontal == 0)
        {
            // Activar el Trigger de ataque
            animator.SetTrigger("downAttack");
            audioSource.Play();
        }
        //Ataque en todas direcciones
        if (moveVertical != 0 && moveHorizontal != 0)
        {
            animator.SetTrigger("rightAttack");
            audioSource.Play();
        }
        //Ataque en IDLE
        if (moveVertical == 0 && moveHorizontal == 0)
        {
            animator.SetTrigger("rightAttack");
            audioSource.Play();
        }

        yield return new WaitForSeconds(0.3f); // Asume que el ataque dura 0.5 segundos
        isAttacking = false;
    }

}

