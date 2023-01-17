using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerShip : EnemyShip // INHERITANCE
{
    // Start is called before the first frame update
    void Start()
    {
       InitializeShip();
       InvokeRepeating("FireLaser", 2.0f, 7.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        MoveForward();
        StayInBounds();
        CheckHealth();
    }

  protected override void InitializeShip()
    {
        health = 200;
        shield = 200;
        speed = 0.5f;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            health -= 5;
            Destroy(other);
        }
    }
}
