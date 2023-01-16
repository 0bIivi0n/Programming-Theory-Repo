using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerShip : EnemyShip // INHERITANCE
{
    // Start is called before the first frame update
    void Start()
    {
        health = SetHealth(health);
        shield = SetShield(shield);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override int SetHealth(int health) // POLYMORPHISM
    {
        return health * 2;
    }

    public override int SetShield(int shield) // POLYMORPHISM
    {
        return shield * 2;
    }
}
