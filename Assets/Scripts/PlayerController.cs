using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador

    private Rigidbody2D rb; // Referencia al componente Rigidbody2D

    public Animator animator;

    void Start()
    {
        // Obtener la referencia al componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (moveHorizontal!=0 && moveVertical == 0)
            {
                // Activar el Trigger de ataque
                animator.SetTrigger("rightAttack");
            }
            if (moveVertical>0 && moveHorizontal == 0)
            {
                // Activar el Trigger de ataque
                animator.SetTrigger("upAttack");
            }
            if (moveVertical < 0 && moveHorizontal == 0)
            {
                // Activar el Trigger de ataque
                animator.SetTrigger("downAttack");
            }
            //Ataque en todas direcciones
            if (moveVertical != 0 && moveHorizontal != 0)
            {
                animator.SetTrigger("rightAttack");
            }
            //Ataque en IDLE
            if (moveVertical == 0 && moveHorizontal == 0)
            {
                animator.SetTrigger("rightAttack");
            }
        }
    }

}

