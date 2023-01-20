using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;

    private bool isFired;
    private float speed;
    private float ascensionSpeed;
    private float horizontalInput;
    private float verticalInput;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isFired = false;
        speed = 7.5f;
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
        }
    }

    private void MoveMissile()
    {
        if(!isFired)
        {
            transform.position = new Vector3(player.transform.position.x, (player.transform.position.y - 0.1f), player.transform.position.z);
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
        if(transform.position.z >= 0)
        {
            Destroy(gameObject);
        }
    }
}
