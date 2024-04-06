using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;


    public GameObject explosion;


    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);    
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            GameObject explosiones = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            source.Play();
            DestroyProjectile();
            Destroy(explosiones, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject explosiones = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            source.Play();
            DestroyProjectile();

            Destroy(explosiones, 1f);
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
