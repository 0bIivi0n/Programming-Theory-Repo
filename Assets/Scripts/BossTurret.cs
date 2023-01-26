using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject laserPrefab;
    private GameManager gameManager;
    private SpawnEnemy spawnEnemyScript;
    private float rand;
    [SerializeField] private float fireRate;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnEnemyScript = GameObject.Find("EnemySpawnPoint").GetComponent<SpawnEnemy>();

        fireRate = 10.0f / spawnEnemyScript.bossTurretMultiplier;
        rand = Random.Range(2.0f, 5.0f);
        InvokeRepeating("FireLaser", rand, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
       FollowPlayer();
    }


    void FollowPlayer()
    {
    
        transform.LookAt(player.transform);
    
    }

    void FireLaser()
    {
        if(gameManager.isGameActive)
        {
            Instantiate(laserPrefab, transform.position, transform.rotation);
        }
        
    }
}
