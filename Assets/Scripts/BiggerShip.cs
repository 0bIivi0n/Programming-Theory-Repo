using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerShip : EnemyShip // INHERITANCE
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
        health = 400;
        shield = 400;
        speed = 0.5f;
        value = 100;
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
            
        if(rng >= 90)
        {
            Instantiate(bonusPrefab, transform.position, transform.rotation);
        }
    }     
}
