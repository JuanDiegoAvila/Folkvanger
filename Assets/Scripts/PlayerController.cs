using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador

    private Rigidbody2D rb; // Referencia al componente Rigidbody2D

    void Start()
    {
        // Obtener la referencia al componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
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
    }

}

