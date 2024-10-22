using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private bool isGameStarted;
    private bool isGameOver;
    public GameObject MenuUI;
    public GameObject GamePlayUI;
    private AudioSource musicAudio;
    public GameObject player;
    public GameObject playerPrefab;
    private SpawnManager spawnManager;
    public GameObject GameOverUI;
    public Vector3 playerSpawnPosition = new Vector3(0, 0.573f, 17.88f);



    void Start()
    {
        
        
        spawnManager = GameObject.FindObjectOfType<SpawnManager>();
        musicAudio = Camera.main.GetComponent<AudioSource>();
        UpdateScoreDisplay();
        player.SetActive(false); // player not visible in start menu
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned in the GameManager.");
        }
    }


    public void StartGame()
    {

        // Debugging to check variable states
        Debug.Log("Player: " + player);
        Debug.Log("Music Audio: " + musicAudio);
        Debug.Log("Menu UI: " + MenuUI);
        Debug.Log("Game Play UI: " + GamePlayUI);
        Debug.Log("Spawn Manager: " + spawnManager);

        isGameStarted = true;
        player.SetActive(true);
        musicAudio.Play();
        MenuUI.SetActive(false);
        GamePlayUI.SetActive(true);
        spawnManager.StartSpawning();
    }

    public void GameOver()
    {
        isGameOver = true;
        player.SetActive(false);
        GamePlayUI.SetActive(false);
        GameOverUI.SetActive(true);
        spawnManager.StopSpawning();  
        musicAudio.Stop();
    }

    public void RestartGame()
    {
        score = 0;
        isGameOver = false;
        GameOverUI.SetActive(false);

        GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
        player = newPlayer;

        StartGame();  
    }
}
