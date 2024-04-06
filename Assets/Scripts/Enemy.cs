using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //[SerializeField] float health, maxHealth = 3f;

    //[SerializeField] HealthEnemy healthbar;

    //private void Awake()
    //{
    //    healthbar = GetComponentInChildren<HealthEnemy>();
    //}
    // Start is called before the first frame update

    private PlayerController playerController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController.isAttacking)
            {
                Destroy(gameObject);
            }
        }
    }
}
