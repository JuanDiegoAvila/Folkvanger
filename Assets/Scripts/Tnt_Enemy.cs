using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt_Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwThrows;
    public float startTimeBtwThrows;

    public GameObject projectile;

    public Animator animator;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        timeBtwThrows = startTimeBtwThrows;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        } else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            animator.SetBool("isRunning", false);
        } else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        { 
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }



        if (timeBtwThrows <= 0) {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwThrows = startTimeBtwThrows;
        } else
        {
            timeBtwThrows -= Time.deltaTime;
        }
    }
}
