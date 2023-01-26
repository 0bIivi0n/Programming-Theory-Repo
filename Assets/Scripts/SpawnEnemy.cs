using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject bossPrefab;
    GameManager gameManager;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] int enemiesSpawned = 0;
    [SerializeField] int wave = 1;
    [SerializeField] int enemiesPerWave = 10;
    [SerializeField] int enemiesRemaining;
    [SerializeField] float spawnRate = 4.0f;
    private bool bossSpawned = false;
    public float bossMultiplier {get; private set;}
    public float bossTurretMultiplier {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bossMultiplier = 1.0f;
        bossTurretMultiplier = 1.0f;
        InvokeRepeating("SpawnRandEnemy", 3.0f, 4.0f);
        
    }

    void Update()
    {
        ChangeWave();
    }

    void ChangeWave()
    {
        if(enemiesSpawned == enemiesPerWave)
        {
            CancelInvoke();
            enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if(enemiesRemaining == 1 && bossSpawned == false)
            {
                Instantiate(bossPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), transform.rotation);
                bossSpawned = true;
            }
            
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
            Instantiate(enemies[index], new Vector3(randPosX, transform.position.y, transform.position.z), transform.rotation);
            enemiesSpawned++;
        }
    }

    void StartNewWave()
    {
        wave++;
        bossMultiplier += 0.25f;
        bossTurretMultiplier += 0.1f;
        enemiesPerWave += 10;
        enemiesSpawned = 0;
        bossSpawned = false;
        
        if(spawnRate > 2)
        {
            spawnRate -= 0.25f;
        }
        
        InvokeRepeating("SpawnRandEnemy", 3.0f, spawnRate);
    }

    void UpdateWave()
    {
        waveText.text = "Wave: " + wave; 
    }
}
