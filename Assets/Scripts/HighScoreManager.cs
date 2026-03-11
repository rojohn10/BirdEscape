using UnityEngine;

/// <summary>
/// Manages high score persistence using PlayerPrefs.
/// Single responsibility: load, save, and compare high scores.
/// </summary>
public class HighScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    /// <summary>
    /// Singleton instance for global access.
    /// </summary>
    public static HighScoreManager Instance { get; private set; }

    private int highScore;

    /// <summary>
    /// The current persisted high score.
    /// </summary>
    public int HighScore => highScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadHighScore();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    /// <summary>
    /// Checks if the given score beats the current high score.
    /// If so, saves the new high score and returns true.
    /// </summary>
    /// <param name="score">The score to evaluate.</param>
    /// <returns>True if a new high score was set.</returns>
    public bool TrySetHighScore(int score)
    {
        if (score <= 0 || score <= highScore)
            return false;

        highScore = score;
        SaveHighScore();
        return true;
    }

    /// <summary>
    /// Loads the high score from PlayerPrefs.
    /// </summary>
    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    /// <summary>
    /// Saves the current high score to PlayerPrefs.
    /// </summary>
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Resets the high score to zero. Used for testing or player request.
    /// </summary>
    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.DeleteKey(HighScoreKey);
        PlayerPrefs.Save();
    }
}
