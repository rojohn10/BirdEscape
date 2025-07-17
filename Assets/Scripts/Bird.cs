using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controls the bird's movement, input, and collision behavior.
/// </summary>
public class Bird : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private LogicScript logicScript;
    [SerializeField] private AudioClip flapSound;
    [SerializeField] private float startingSpeed = 5f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float rotationSpeed = 300f;

    private float currentSpeed = 0f;
    private bool goingUp = false;
    private bool birdIsAlive = true;

    /// <summary>
    /// Indicates if the bird is alive.
    /// </summary>
    public bool BirdIsAlive => birdIsAlive;

    private void Update()
    {
        if (!birdIsAlive)
            return;

        HandleInput();
        HandleRotation();
        currentSpeed -= deceleration * Time.deltaTime;
    }

    /// <summary>
    /// Handles player input for bird movement.
    /// </summary>
    private void HandleInput()
    {
        // Touch input (for mobile)
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            goingUp = true;
            logicScript?.HideHomeScreen();
            logicScript?.ResumeGame();
            GoUp();
        }
        // Optional: Add keyboard input for testing in editor
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            logicScript?.HideHomeScreen();
            logicScript?.ResumeGame();
            GoUp();
        }
#endif
    }

    /// <summary>
    /// Handles the bird's rotation based on its movement.
    /// </summary>
    private void HandleRotation()
    {
        if (goingUp)
        {
            if (currentSpeed < 0)
                goingUp = false;

            Quaternion targetRotation = Quaternion.Euler(0, 0, 20);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (currentSpeed < -1)
            {
                Quaternion targetRotation = Quaternion.Euler(0, 0, -20);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// Makes the bird go up, play sound, and set velocity.
    /// </summary>
    public void GoUp()
    {
        if (!birdIsAlive || rigidBody == null)
            return;

        goingUp = true;
        if (flapSound != null)
            SoundFXManager.Instance?.PlaySoundFXClip(flapSound, transform, 1f);

        rigidBody.velocity = Vector2.up * startingSpeed;
        currentSpeed = startingSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!birdIsAlive)
            return;

        logicScript?.PauseGame();
        birdIsAlive = false;
        logicScript?.GameOver();
    }
}