using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;

public class Torre : MonoBehaviour
{
    private Estado estado = new Estado();

    private ResourceManager manager;
    public GameObject objetoComprar;
    public GameObject TextoPrecio;
    public GameObject Arquero;

    private int Precio;
    private Animator animator;
    private bool adentroDeCollider;
    private bool estaCompleto;

    private enum Estado
    {
        idle = 1,
        mid = 2,
        completo = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        estado = Estado.idle;
        estaCompleto = false;

        // Obtener el precio de la mejora.
        var textoPrecio = TextoPrecio.GetComponent<TextMeshPro>();
        manager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        animator = GetComponentInParent<Animator>();
        Precio = Convert.ToInt32(textoPrecio.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (adentroDeCollider && Input.GetKeyDown(KeyCode.C))
        {
            ComprarMejora();
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
            switch (estado)
            {
                case Estado.idle:
                    estado = Estado.mid;
                    Precio = 200;
                    TextoPrecio.GetComponent<TextMeshPro>().text = "200";
                    break;
                case Estado.mid:
                    estado = Estado.completo;
                    objetoComprar.SetActive(false);
                    estaCompleto = true;

                    // Activar arqueros 
                    Arquero.SetActive(true); 

                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!estaCompleto)
                objetoComprar.SetActive(true);

            adentroDeCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objetoComprar.SetActive(false);
            adentroDeCollider = false;
        }
    }
}
