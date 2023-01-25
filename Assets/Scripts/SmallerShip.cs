using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallerShip : EnemyShip // INHERITANCE
{
    // Start is called before the first frame update
    void Start()
    {
        InitializeShip();
        player = GameObject.Find("Player");
        InvokeRepeating("FireLaser", 2.0f, 7.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        MoveForward();
        StayInBounds();
        CheckHealth();
        CheckGameOver();
    }

   protected override void InitializeShip()
    {
        // POLYMORPHISM
        health = 100;
        shield = 100;
        speed = 1.5f;
        value = 25;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            health -= 5;
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Missile"))
        {
            health -= 100;
            Destroy(other.gameObject);
        }
    }

    void DropBonus()
    {
        int rng = Random.Range(0, 100);
            
        if(rng >= 20 && rng <= 30)
        {
            Instantiate(bonusPrefab, transform.position, transform.rotation);
        }
    }     
}
