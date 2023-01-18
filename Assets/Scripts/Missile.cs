using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public bool isFired;
    private float speed;
    private float ascensionSpeed;
    private float horizontalInput;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isFired = false;
        speed = 5.0f;
        ascensionSpeed = 0.1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive)
        {
            MoveMissile();

            if(Input.GetButtonDown("Fire1"))
            {
                isFired = true;
            }

            FireMissile();
            DestroyMissile();
            StayInBounds();
        }
    }

    private void MoveMissile()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(!isFired)
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
    }

    private void FireMissile()
    {
        if(isFired)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if(transform.position.y < 2)
            {
                transform.Translate(Vector3.up * Time.deltaTime * ascensionSpeed);
            }
        }
    }

    private void DestroyMissile()
    {
        if(transform.position.z >= 25)
        {
            Destroy(gameObject);
        }
    }

    protected void StayInBounds()
    {
        if(transform.position.x > 6.5)
        {
            transform.position = new Vector3(6.5f, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -6.5)
        {
            transform.position = new Vector3(-6.5f, transform.position.y, transform.position.z);
        }
    }
}
