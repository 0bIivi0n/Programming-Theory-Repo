using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip : EnemyShip
{

    private SpawnEnemy spawnEnemyScript;
    private float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        spawnEnemyScript = GameObject.Find("EnemySpawnPoint").GetComponent<SpawnEnemy>();
        multiplier = spawnEnemyScript.bossMultiplier;
        InitializeShip();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        MoveForward();
        StayInBounds();
        CheckHealth();
        CheckGameOver();
    }

    protected override void InitializeShip()
    {
        health = 1500 * multiplier;
        shield = 1500 * multiplier;
        speed = 0.5f;
        value = 2500 * multiplier;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    protected override void CheckHealth()
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
        Instantiate(bonusPrefab, transform.position, transform.rotation);
    }


    protected override void MoveForward()
    {
        if(transform.position.z > -10)
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
}
