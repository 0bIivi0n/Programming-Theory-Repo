using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallerShip : EnemyShip // INHERITANCE
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
        // POLYMORPHISM
        health = 50;
        shield = 50;
        speed = 1.5f;
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
