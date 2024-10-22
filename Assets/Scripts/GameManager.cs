using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject MenuUI;
    public GameObject GamePlayUI;
    public GameObject GameOverUI;
    public GameObject player;
    public Vector3 playerSpawnPosition = new Vector3(0, 0.573f, 17.88f);
    private Quaternion playerSpawnRotation;

    private int score = 0;
    private bool isGameStarted;
    private bool isGameOver;
    private AudioSource musicAudio;
    private SpawnManager spawnManager;
    private Health playerHealth;

    void Start()
    {
        playerSpawnRotation = player.transform.rotation;
        spawnManager = GameObject.FindObjectOfType<SpawnManager>();
        musicAudio = Camera.main.GetComponent<AudioSource>();
        playerHealth = player.GetComponent<Health>();
        UpdateScoreDisplay();
        player.SetActive(false);
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

        // Reset player position
        player.SetActive(true);
        player.transform.position = playerSpawnPosition;
        player.transform.rotation = playerSpawnRotation;

        // Reset health
        playerHealth.ResetHealth();
        UpdateScoreDisplay();

        // Reset the spawn manager
        spawnManager.ResetSpawner();

        StartGame();
    }
}
