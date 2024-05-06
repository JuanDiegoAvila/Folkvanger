using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playerController = collision.gameObject.GetComponent<PlayerController>();

    //        if (playerController.isAttacking)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    public void TakeDamage(int damage)
    {
        //Instantiate(hitEffect, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("Damage taken");
    }
}
