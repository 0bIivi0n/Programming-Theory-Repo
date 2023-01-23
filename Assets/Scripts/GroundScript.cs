using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    [SerializeField] GameObject groundPrefab;
    float speed = 1.0f;
    bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if(transform.position.z <= -25 && !hasSpawned)
        {
            Instantiate(groundPrefab, new Vector3(0.0f, 0.0f, 25.0f), groundPrefab.transform.rotation);
            hasSpawned = true;
        }

        if(transform.position.z <= -50)
        {
            Destroy(gameObject);
            hasSpawned = false;
        }
    }
}
