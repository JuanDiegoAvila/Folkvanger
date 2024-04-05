using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f; // Ajusta seg�n necesites
    private Vector3 direction;
    private float lifeTime = 0.5f; // Tiempo de vida de la flecha en segundos

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        // Asegura que la flecha se mueva hacia la direcci�n establecida al instanciar
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void Update()
    {
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifeTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("Le pegu� al enemigo!");
            // Aqu� puedes hacer algo con el enemigo, como restarle vida
            Destroy(gameObject);
        }
    }
}
