using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private AudioClip gameOverSound;

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
    /// Triggers game over state and plays sound.
    /// </summary>
    public void GameOver()
    {
        if (gameOverSound != null)
        {
            SoundFXManager.Instance.PlaySoundFXClip(gameOverSound, transform, 1f);
        }
        ShowGameOverScreen();
    }

    /// <summary>
    /// Resumes the game by setting time scale to 1.
    /// </summary>
    public void ResumeGame() => Time.timeScale = 1f;

    /// <summary>
    /// Pauses the game by setting time scale to 0.
    /// </summary>
    public void PauseGame() => Time.timeScale = 0f;

    public void ShowHomeScreen()
    {
        if (homeScreen != null)
        {
            homeScreen.SetActive(true);
        }
    }

    public void HideHomeScreen()
    {
        if (homeScreen != null)
        {
            homeScreen.SetActive(false);
        }
    }

    public void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }
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
