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
    [SerializeField] TMP_InputField playerNameInput;
    public Button startButton;
    public Button exitButton;
    public GameObject gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI bestScoreText;
    public Player player;
    public bool isGameActive = false;
    private GameObject fireParticle;
    
    private int score = 0;
    private int bestScore = 500;
    public string playerName;
    private string bestPlayerName = "ObiWan";
    
    private void Awake()
    {
        LoadBestScore();
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
        UpdateHealth();
        UpdateName();
        UpdateBestScore();

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

    public void UpdateName()
    {
        playerNameText.text = playerName;
    }

    public void UpdateBestScore()
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

    public void SetPlayerName()
    {
        playerName = playerNameInput.text;
    }

    void CheckPlayerName()
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

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();

        data.HighScore = bestScore;
        data.BestPlayer = bestPlayerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
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

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string BestPlayer;
    }
}


