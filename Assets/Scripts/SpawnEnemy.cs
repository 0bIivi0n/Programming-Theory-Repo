using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    GameManager gameManager;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] int enemiesSpawned = 0;
    [SerializeField] int wave = 1;
    [SerializeField] int enemiesPerWave = 10;
    [SerializeField] int enemiesRemaining;
    [SerializeField] float spawnRate = 4.0f;

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

        UpdateWave();
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
        enemiesPerWave += 10;
        enemiesSpawned = 0;
        spawnRate -= 0.25f;
        InvokeRepeating("SpawnRandEnemy", 3.0f, spawnRate);
    }

    void UpdateWave()
    {
        waveText.text = "Wave: " + wave; 
    }
}
