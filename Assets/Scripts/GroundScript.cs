using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    [SerializeField] GameObject groundPrefab;
    float speed = 1.0f;


    // Update is called once per frame
    void Update()
    {
        if(transform.position.z <= -50)
        {   
            transform.position = new Vector3(0.0f, 0.0f, 49.9f);
        }

        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void LateUpdate()
    {
        
    }
}
