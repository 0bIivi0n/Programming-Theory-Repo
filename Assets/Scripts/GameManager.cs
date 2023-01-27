using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject startMenu;
    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button retryButton;
    [SerializeField] GameObject gameOver;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] Player player;
    
    private GameObject fireParticle;
     
    public bool isGameActive {get; private set;}
   
    private int score = 0;
    private int bestScore = 01;
    private string playerName;
    private string bestPlayerName = "Zeub";
    private int health;
    private int shield;
    
    private void Awake()
    {
        LoadBestScore();
        isGameActive = false;
    }

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
        CheckPlayerName();
        UpdateName();
        UpdateBestScore();
        CheckPlayerHealth(); 
        UpdateHealth();
    }


    // Functions:

    private void CheckPlayerHealth()
    {
        if(player.health <= 0)
        {
            health = 0;
            isGameActive = false;
            gameOver.SetActive(true);
            fireParticle.SetActive(true);
        }
    }

    public void UpdateScore(float scoreToAdd)
    {
        score += (int) scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    private void UpdateHealth()
    {
        health = (int) player.health;
        shield = (int) player.shield;
        
        if(health <= 0)
        {
            health = 0;
        }
        if(shield <= 0)
        {
            shield = 0;
        }

        healthText.text = "Health: " + health;
        shieldText.text = "Shield: " + shield;
    }

    private void UpdateName()
    {
        playerNameText.text = playerName;
    }

    private void UpdateBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            bestPlayerName = playerName;
        }

        bestScoreText.text = "Best Score \n" + bestPlayerName + ": " + bestScore;
        SaveBestScore();
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

    private void SetPlayerName()
    {
        playerName = playerNameInput.text;
    }

    private void CheckPlayerName()
    {
        if(playerNameInput.text != "")
        {
            startButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            startButton.GetComponent<Button>().interactable = false;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void SaveBestScore()
    {
        SaveData data = new SaveData();

        data.HighScore = bestScore;
        data.BestPlayer = bestPlayerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.BestPlayer;
            bestScore = data.HighScore;
        }
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string BestPlayer;
    }
}


