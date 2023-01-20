using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : ShipParent
{
    [SerializeField] protected GameObject laserPrefab;
    protected GameObject player;
    protected float speed = 1.0f;

    void Start()
    {
        InitializeShip();
        player = GameObject.Find("Player");
        InvokeRepeating("FireLaser", 2.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        MoveForward();
        StayInBounds();
        CheckHealth();
        //CheckGameOver();
    }

    protected void MoveForward()
    {
        if(transform.position.z > -13)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if(transform.position.y > 2)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if(transform.position.y < 2)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
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
            Destroy(other.gameObject);
        } 
        else if(other.CompareTag("Missile"))
        {
            health -= 100;
            Destroy(other.gameObject);
        }
    }

    protected void CheckGameOver()
    {
        if(gameManager.isGameActive == false)
        {
            Destroy(gameObject);
        }
    }
}
