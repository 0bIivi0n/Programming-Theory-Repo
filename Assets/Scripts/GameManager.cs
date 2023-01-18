using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject startMenu;
    public GameObject gameOver;
    public Button startButton;
    public Button exitButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public Player player;
    public bool isGameActive = false;
    private GameObject fireParticle;
    
    
    
    private int score = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        fireParticle = GameObject.Find("FireParticle");
        fireParticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();

          if(player.health <= 0)
            {
                player.health = 0;
                Debug.Log("Game Over!");
                isGameActive = false;
                gameOver.SetActive(true);
                fireParticle.SetActive(true);
            }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateHealth()
    {
        healthText.text = "Health: " + player.health;
    }

    public void StartGame()
    {
        isGameActive = true;
        UpdateScore(0);
        UpdateHealth();
        inGameUI.SetActive(true);
        startMenu.SetActive(false);
        gameOver.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
