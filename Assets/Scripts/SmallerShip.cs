using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallerShip : EnemyShip // INHERITANCE
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
        health /= 2;
        return health;
    }

    public override int SetShield(int shield) // POLYMORPHISM
    {
        shield /= 2;
        return shield;
    }
}
