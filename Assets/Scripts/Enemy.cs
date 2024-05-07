using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;

    //public int currentHealth;
    public Slider slider;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (currentHealth <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
        
    //}

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
        
        // move slider to show damage 
        
        //currentHealth -= damage;
        //slider.value = (currentHealth / 100);
        //Debug.Log("Damage taken "+ slider.value);

        currentHealth -= damage;

        // Play hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }

        // Update health bar
        slider.value = currentHealth / (float) maxHealth;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
