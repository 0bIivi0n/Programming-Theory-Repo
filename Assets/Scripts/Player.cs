using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ShipParent
{
    [SerializeField] GameObject playerShield;
    [SerializeField] GameObject shockWave;
    [SerializeField] GameObject missileIcon;
    [SerializeField] GameObject fireBonusIcon;
    [SerializeField] GameObject shockWaveIcon;
    [SerializeField] private GameObject canonAudio;
    
    private AudioSource repairAudio;
    private AudioSource shieldAudio;
    private AudioSource WeaponBonusAudio;
    private AudioSource shockWaveSound;
    private AudioSource motorSound;
    private float canonTimeStamp;
    private float speed = 5.0f;
    private float fireRate = 0.15f; 
    private float horizontalInput;
    private float verticalInput;
    private bool canFire = true;
    [SerializeField] private bool hasFireBonus = false;
    [SerializeField ]private bool hasShockWaveBonus = false;

    // Start is called before the first frame update
    void Start()
    {   
        InitializeShip();
        InitializeReferences();
        
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
            ShowIcons();

            if(shield <= 0)
            {
                shield = 0;
                playerShield.SetActive(false);
            }

            if(Input.GetButtonDown("FireCanonBonus") && hasFireBonus)
            {
                FireBonus();
            }

            if(Input.GetButtonDown("ShockwaveBonus") && hasShockWaveBonus)
            {
                ActivateShockWave();
            }
        }
        
        if(gameManager.isGameActive == false)
        {
            canonAudio.SetActive(false);
        }
    }

    private void InitializeReferences()
    {
        repairAudio = GameObject.Find("Repair Sound").GetComponent<AudioSource>();
        shieldAudio = GameObject.Find("Shield Up Sound").GetComponent<AudioSource>();
        WeaponBonusAudio = GameObject.Find("Weapon Bonus Sound").GetComponent<AudioSource>();
        motorSound = GameObject.Find("Motor Audio").GetComponent<AudioSource>();
        shockWaveSound = GetComponent<AudioSource>();
    }

    protected override void InitializeShip()
    {
        health = 100;
        shield = 0;
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
        else if(verticalInput > 0)
        {
            motorSound.pitch = 1.1f;
        }
        else if(verticalInput < 0)
        {
            motorSound.pitch = 0.9f;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            motorSound.pitch = 1.0f;
        }
    }

    private void FireCanon()
    {
        if(Input.GetButton("Fire1") && Time.time - canonTimeStamp > fireRate)
        {
            canonAudio.SetActive(true);
            Instantiate(projectilePrefab, new Vector3(transform.position.x + 0.22f, transform.position.y + 0.05f, transform.position.z + 1.0f), transform.rotation);
            Instantiate(projectilePrefab, new Vector3(transform.position.x - 0.22f, transform.position.y + 0.05f, transform.position.z + 1.0f), transform.rotation);
            canonTimeStamp = Time.time;
        }

        if(Input.GetButtonUp("Fire1"))
        {
            canonAudio.SetActive(false);
        }

    }

    private void ResetMissile()
    {
        if(Input.GetButtonDown("Fire2") && canFire)
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

    private void ShowIcons()
    {
        if(canFire)
        {
            missileIcon.SetActive(true);
        }
        else
        {
            missileIcon.SetActive(false);
        }

        if(hasFireBonus)
        {
            fireBonusIcon.SetActive(true);
        }
        else
        {
            fireBonusIcon.SetActive(false);
        }

        if(hasShockWaveBonus)
        {
            shockWaveIcon.SetActive(true);
        }
        else
        {
            shockWaveIcon.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Laser"))
        {
            if(shield > 0)
            {
                shield -= 10;
                Destroy(other.gameObject);
            }
            else
            {
                health -= 10;
                Destroy(other.gameObject);
            }
            
        }

        if(other.CompareTag("FireBonus"))
        {
            WeaponBonusAudio.Play();
            hasFireBonus = true;
            Destroy(other.gameObject);
        }

        if(other.CompareTag("RepairBonus"))
        {
            repairAudio.Play();
            RecoverHealth();
            Destroy(other.gameObject);
        }

        if(other.CompareTag("ShieldBonus"))
        {
            shieldAudio.Play();
            ActivateShield();
            Destroy(other.gameObject);
        }

        if(other.CompareTag("ThunderBonus"))
        {
            WeaponBonusAudio.Play();
            hasShockWaveBonus = true;
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

    void FireBonus()
    {
        WeaponBonusAudio.Play();
        fireRate = 0.07f;
        StartCoroutine(StopFireBonus());
        hasFireBonus = false;
    }

    IEnumerator StopFireBonus()
    {
        yield return new WaitForSeconds(10);
        fireRate = 0.15f;
    }

    void RecoverHealth()
    {
        if(health < 100)
        {
            health += 20;
        }
        if(health > 100)
        {
            health = 100;
        }
    }

    void ActivateShield()
    {
        if(shield < 100)
        {
            shield += 20;
            playerShield.SetActive(true);
        }
        if(shield > 100)
        {
            shield = 100;
        }
        
    }

    void ActivateShockWave()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        shockWaveSound.Play();
        shockWave.SetActive(true);
        StartCoroutine(StopShockWave());

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyShip>().health -= 100;
        }

        hasShockWaveBonus = false;
    } 

     IEnumerator StopShockWave()
    {
        yield return new WaitForSeconds(2);
        shockWave.SetActive(false);
    }
}
