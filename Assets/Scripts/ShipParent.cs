using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParent : MonoBehaviour
{
    public GameObject projectilePrefab;

    [SerializeField] protected int health = 100;
    [SerializeField] protected int shield = 100;

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

    public virtual int SetHealth(int health)
    {
        health *= 1;
        return health;
    }

    public virtual int SetShield(int shield)
    {
        shield *= 1;
        return shield;
    }
}