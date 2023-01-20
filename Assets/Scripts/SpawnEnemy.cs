using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    GameManager gameManager;
    [SerializeField] int enemiesSpawned = 0;
    [SerializeField] int wave = 1;
    [SerializeField] int enemiesPerWave = 10;
    [SerializeField] int enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnRandEnemy", 3.0f, 4.0f);
    }

    void Update()
    {
        if(enemiesSpawned == enemiesPerWave)
        {
            CancelInvoke();
            enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
            
            if(enemiesRemaining == 0)
            {
                StartNewWave();
            }
        }
    }

    private void SpawnRandEnemy()
    {
        if(gameManager.isGameActive)
        {
            int index = Random.Range(0, enemies.Length);
            float randPosX = Random.Range(-8, 8);
            Instantiate(enemies[index], new Vector3(randPosX, transform.position.y, transform.position.y), transform.rotation);
            enemiesSpawned++;
        }
    }

    void StartNewWave()
    {
        wave++;
        enemiesPerWave *= 2;
        enemiesSpawned = 0;
        InvokeRepeating("SpawnRandEnemy", 3.0f, 4.0f);
    }
}
