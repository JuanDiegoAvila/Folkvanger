using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Enemy : MonoBehaviour
{
    public float speed;
    private Transform target;
    public Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        Vector2 direction = target.position - transform.position;
        // Calcular la direcci�n del jugador relativa al enemigo
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (direction.magnitude < 4)
        {
            if (direction.magnitude > 0.8)
            {
                if (angle > -45 && angle <= 45)
                {
                    // El jugador est� a la derecha del enemigo
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (angle > 135 || angle <= -135)
                {
                    // El jugador est� a la izquierda del enemigo
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
                animator.SetBool("isRunning", true);
                isAttacking = false;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                isAttacking = true;
                animator.SetBool("isRunning", false);
            }

            if (isAttacking)
            {

                animator.SetBool("isAttacking", true);
                // Determinar la direcci�n del jugador en relaci�n con el enemigo
                if (angle > -45 && angle <= 45)
                {
                    // El jugador est� a la derecha del enemigo
                    animator.SetInteger("attackType", 2);
                }
                else if (angle > 45 && angle <= 135)
                {
                    // El jugador est� arriba del enemigo
                    animator.SetInteger("attackType", 3);
                }
                else if (angle > 135 || angle <= -135)
                {
                    // El jugador est� a la izquierda del enemigo
                    animator.SetInteger("attackType", 2);
                }
                else
                {
                    // El jugador est� abajo del enemigo
                    animator.SetInteger("attackType", 1);
                }
            }
            else
            {
                animator.SetBool("isAttacking", false);
            }
        }
        else
        {
            isAttacking = false;
            animator.SetBool("isRunning", false);
        }

    }
}
