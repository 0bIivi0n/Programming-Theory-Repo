using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandEnemy", 3.0f, 6.0f);
    }

    private void SpawnRandEnemy()
    {
        int index = Random.Range(0, enemies.Length);
        float randPosX = Random.Range(-8, 8);
        Instantiate(enemies[index], new Vector3(randPosX, transform.position.y, transform.position.y), transform.rotation);
    }
}
