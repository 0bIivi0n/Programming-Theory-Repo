using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        // ABSTRACTION
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
