using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParent : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject missilePrefab;

    [SerializeField] protected int health = 100;
    [SerializeField] protected int shield = 100;

    // Start is called before the first frame update
    void Start()
    {
        // ABSTRACTION
        InitializeShip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void InitializeShip()
    {
        health = 100;
        shield = 100;
    }

    protected void StayInBounds()
    {
        if(transform.position.x > 6.5)
        {
            transform.position = new Vector3(6.5f, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -6.5)
        {
            transform.position = new Vector3(-6.5f, transform.position.y, transform.position.z);
        }
    }
}
