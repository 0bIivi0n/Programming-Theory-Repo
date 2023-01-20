using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ShipParent
{
    private float canonTimeStamp;
    private float speed = 5.0f;
    private float horizontalInput;
    private float verticalInput;
    private bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        
        InitializeShip();
        canonTimeStamp = Time.time;
        Instantiate(missilePrefab, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), missilePrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive)
        {
            FireCanon();
            ResetMissile();
            MovePlayer();
            StayInBounds();
            StayInBoundsZ();
        }
        
    }

    protected override void InitializeShip()
    {
        health = 100;
        shield = 100;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void MovePlayer()
    {
        // Basic moves
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed, Space.World);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed, Space.World);
        
        if(horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -10);
        } 
        else if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 10);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FireCanon()
    {
        if(Input.GetButton("Fire2") && Time.time - canonTimeStamp > 0.15f)
        {
            Instantiate(projectilePrefab, new Vector3(transform.position.x + 0.22f, transform.position.y + 0.05f, transform.position.z + 1.0f), transform.rotation);
            Instantiate(projectilePrefab, new Vector3(transform.position.x - 0.22f, transform.position.y + 0.05f, transform.position.z + 1.0f), transform.rotation);
            canonTimeStamp = Time.time;
        }
    }

    private void ResetMissile()
    {
        if(Input.GetButtonDown("Fire1") && canFire)
        {
            StartCoroutine("SpawnMissile");
            canFire = false;
        }
    }

    IEnumerator SpawnMissile()
    {
        yield return new WaitForSeconds(10);
        Instantiate(missilePrefab, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), missilePrefab.transform.rotation);
        canFire = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Laser"))
        {
            health -= 10;
            Debug.Log("Touch");
            Destroy(other.gameObject);
        }
    }

    void StayInBoundsZ()
    {
        if(transform.position.z <= -20)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -20.0f);
        }
        if(transform.position.z >= -5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5.0f);
        }
    }
}
