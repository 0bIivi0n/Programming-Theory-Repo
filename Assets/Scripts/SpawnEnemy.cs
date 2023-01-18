using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnRandEnemy", 3.0f, 6.0f);
    }

    private void SpawnRandEnemy()
    {
        if(gameManager.isGameActive)
        {
            int index = Random.Range(0, enemies.Length);
            float randPosX = Random.Range(-8, 8);
            Instantiate(enemies[index], new Vector3(randPosX, transform.position.y, transform.position.y), transform.rotation);
        } 
    }
}
