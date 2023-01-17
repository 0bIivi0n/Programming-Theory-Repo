using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : ShipParent
{
    public GameObject laserPrefab;
    public GameObject player;
    protected float speed = 1.0f;

    void Start()
    {
        InvokeRepeating("FireLaser", 2.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        MoveForward();
        StayInBounds();
        CheckHealth();
        
    }

    protected void MoveForward()
    {
        if(transform.position.z > -13)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    protected void FireLaser()
    {
        Instantiate(laserPrefab, transform.position, transform.rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            health -= 5;
            Destroy(other);
        }
    }

    protected void CheckHealth()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
