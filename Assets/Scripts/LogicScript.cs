using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles game logic, UI, and state transitions.
/// </summary>
public class LogicScript : MonoBehaviour
{
    [SerializeField] private int playerScore = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject newHighScoreIndicator;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private AudioClip gameOverSound;

    private bool isGameOver;

    private void Start()
    {
        UpdateHighScoreUI();
    }

    /// <summary>
    /// Adds to the player's score and updates the UI.
    /// </summary>
    public void AddScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        UpdateScoreUI();
    }

    /// <summary>
    /// Restarts the current scene and resets UI.
    /// </summary>
    public void RestartGame()
    {
        ShowHomeScreen();
        HideGameOverScreen();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }

    /// <summary>
    /// Triggers game over state, checks high score, and plays sound.
    /// </summary>
    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        if (gameOverSound != null)
        {
            SoundFXManager.Instance?.PlaySoundFXClip(gameOverSound, transform, 1f);
        }

        bool isNewHighScore = HighScoreManager.Instance != null
            && HighScoreManager.Instance.TrySetHighScore(playerScore);

        UpdateHighScoreUI();

        if (newHighScoreIndicator != null)
            newHighScoreIndicator.SetActive(isNewHighScore);

        ShowGameOverScreen();
    }

    /// <summary>
    /// Gets the current player score.
    /// </summary>
    public int PlayerScore => playerScore;

    /// <summary>
    /// Updates the high score UI text.
    /// </summary>
    private void UpdateHighScoreUI()
    {
        if (highScoreText != null && HighScoreManager.Instance != null)
            highScoreText.text = "Best: " + HighScoreManager.Instance.HighScore;
    }

    /// <summary>
    /// Resumes the game by setting time scale to 1.
    /// </summary>
    public void ResumeGame() => Time.timeScale = 1f;

    /// <summary>
    /// Pauses the game by setting time scale to 0.
    /// </summary>
    public void PauseGame() => Time.timeScale = 0f;

    /// <summary>
    /// Shows the home screen UI.
    /// </summary>
    public void ShowHomeScreen()
    {
        if (homeScreen != null)
        {
            homeScreen.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the home screen UI.
    /// </summary>
    public void HideHomeScreen()
    {
        if (homeScreen != null)
        {
            homeScreen.SetActive(false);
        }
    }

    /// <summary>
    /// Shows the game over screen UI.
    /// </summary>
    public void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the game over screen UI.
    /// </summary>
    public void HideGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = playerScore.ToString();
    }
}
