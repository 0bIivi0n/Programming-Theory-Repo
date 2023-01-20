using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParent : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected GameObject missilePrefab;
    protected GameManager gameManager;

    public int health {get; protected set;}
    public int shield {get; protected set;}

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
        health = 200;
        shield = 200;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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

    protected virtual void CheckHealth()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(50);
        }
    }
}
