using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Enemy : MonoBehaviour
{
    public float speed;
    private Transform target;
    private Animator animator;


    //Attacking Logic
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public float attackRate = 2f;

    public float attackCooldown = 2.0f; // Tiempo entre ataques en segundos
    private float attackTimer = 0;      // Temporizador para rastrear el tiempo desde el �ltimo ataque

    private int angleAnimation;

    // Knockback
    public float knockbackStrength = 5f;
    public float knockbackDuration = 0.2f;
    private bool isKnockedBack = false;
    private Vector2 knockbackDirection;
    private float knockbackEndTime;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player") != null ? GameObject.FindGameObjectWithTag("Player").transform : null;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (isKnockedBack)
        {
            if (Time.time >= knockbackEndTime)
            {
                isKnockedBack = false;
            }
            else
            {
                transform.Translate(knockbackDirection * knockbackStrength * Time.deltaTime);
                return;
            }
        }

        attackTimer += Time.deltaTime; // Incrementar el temporizador basado en el tiempo transcurrido

        if(target == null)
        {
            return;
        }


        Vector2 direction = target.position - transform.position;

        // Calcular la direcci�n del jugador relativa al enemigo
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Is moving
        if (direction.magnitude < 4)
        {
            // Is chasing
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
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            // Is in range of attack
            else
            {

                if (angle > -45 && angle <= 45)
                {
                    // El jugador est� a la derecha del enemigo
                    angleAnimation = 2;
                }
                else if (angle > 45 && angle <= 135)
                {
                    // El jugador est� arriba del enemigo
                    angleAnimation = 3;
                }
                else if (angle > 135 || angle <= -135)
                {
                    // El jugador est� a la izquierda del enemigo
                    angleAnimation = 2;
                }
                else
                {
                    // El jugador est� abajo del enemigo
                    angleAnimation = 1;
                }


                // Verificar si el temporizador ha superado el tiempo de enfriamiento antes de atacar
                if (attackTimer >= attackCooldown)
                {
                    performAttack(angleAnimation);
                    attackTimer = 0; // Resetear el temporizador despu�s de atacar
                    animator.SetBool("isRunning", false);
                }
            }

        }

        // Is Idle
        else
        {
            animator.SetBool("isRunning", false);
        }

    }

    public void performAttack ( int angleAnimation )
    {


        switch (angleAnimation)
        {
            case 1:
                animator.SetTrigger("attackDown");
                break;
            case 2:
                animator.SetTrigger("attackHor");
                break;
            case 3:
                animator.SetTrigger("attackUp");
                break;
        }


        animator.SetInteger("attackType", angleAnimation);

        StartCoroutine(DelayDamage());
    }

    IEnumerator DelayDamage()
    {
        // Esperar un tiempo antes de continuar
        yield return new WaitForSeconds(0.5f);  // Ajusta el tiempo seg�n la duraci�n de la animaci�n

        // Detecta si hay un enemigo en rango de ataque
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);

        // Aplica da�o si se encontr� un enemigo
        if (hitPlayer != null)
        {
            hitPlayer.GetComponent<IDamageable>().TakeDamage(20, new Vector2());
        }
    }

    public void ApplyKnockback(Vector2 direction)
    {

        print("Knockback");
        isKnockedBack = true;
        knockbackDirection = direction.normalized;
        knockbackEndTime = Time.time + knockbackDuration;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
