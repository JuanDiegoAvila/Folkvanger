using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MinaDeOro : MonoBehaviour
{
    private readonly float tiempoProduccion = 3;
    private float tiempoActual;
    private int oroProducido;
    private readonly int limiteOro = 200;
    float tiempoParaAgregarOro = 0f;

    private Estado estado = new Estado();

    public ResourceManager manager;
    public GameObject objetoComprar;
    public GameObject TextoPrecio;
    public GameObject Recolectar;
   
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
        tiempoActual = 0;
        oroProducido = 0;
        estaCompleto = false;

        // Obtener el precio de la mejora.
        var textoPrecio = TextoPrecio.GetComponent<TextMeshPro>();
        animator = GetComponentInParent<Animator>();
        Precio = Convert.ToInt32(textoPrecio.text);
    }

    // Update is called once per frame
    void Update()
    {
        tiempoActual += Time.deltaTime;
        tiempoParaAgregarOro += Time.deltaTime;

        if (estado.Equals(Estado.completo))
        {
            if (tiempoActual < tiempoProduccion)
            {
                if (oroProducido < limiteOro && tiempoParaAgregarOro >= 1f)
                {
                    oroProducido += 1;
                    tiempoParaAgregarOro -= 1f;
                }
            }
            else
            {
                Recolectar.SetActive(true);
            }
        }

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

    public void RecogerOro()
    {
        manager.AddGold(oroProducido);
        tiempoActual = 0;
        oroProducido = 0;
        Recolectar.SetActive(false);
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
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!estaCompleto)
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
