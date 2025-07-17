using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the main game loop, pillar spawning, and singleton access.
/// </summary>
public class GameController : MonoBehaviour
{
    //public static GameController main;

    [SerializeField] public Bird bird;
    [SerializeField] private Pillar pillarPrefab;
    [SerializeField] private int frequency = 50;

    private int frameCounter;

    /// <summary>
    /// Singleton instance of the GameController.
    /// </summary>
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        // Ensure that there is only one instance of GameController
        if (Instance == null)
        {
            Instance = this;
            Application.targetFrameRate = 60;
            frameCounter = 0;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Spawns a new pillar at the prefab's position and rotation.
    /// </summary>
    public void SpawnPillar()
    {
        if (pillarPrefab != null)
        {
            Instantiate(pillarPrefab, pillarPrefab.transform.position, pillarPrefab.transform.rotation);
        }
    }

    private void Update()
    {
        if (Time.timeScale != 1) return;

        frameCounter++;
        if (frameCounter >= frequency)
        {
            SpawnPillar();
            frameCounter = 0;
        }
    }
}
