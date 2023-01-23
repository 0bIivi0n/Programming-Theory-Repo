using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private GameManager gameManager;
    float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
         gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime, Space.World);
    }

    void DestroyOnGameOver()
    {
        if(gameManager.isGameActive == false)
        {
            Destroy(gameObject);
        }
    }
}
