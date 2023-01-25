using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParent : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected GameObject missilePrefab;
    [SerializeField] protected GameObject bonusPrefab;
    protected GameManager gameManager;

    public float health;
    public float shield;
    public float value;

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
        shield = 0;
        value = 50;
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
            DropBonus();
            Destroy(gameObject);
            gameManager.UpdateScore(value);
        }
    }

    void DropBonus()
    {
        int rng = Random.Range(0, 100);
            
        if(rng >= 60 && rng <= 70)
        {
            Instantiate(bonusPrefab, transform.position, transform.rotation);
        }
    }          
}
