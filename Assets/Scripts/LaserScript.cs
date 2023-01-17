using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyLaser();
        MoveLaser();
    }

    private void MoveLaser()
    {
        // ABSTRACTION
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void DestroyLaser()
    {
        if(transform.position.z < -30)
        {
            Destroy(gameObject);
        }
    }
}
