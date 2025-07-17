using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles scoring when the bird passes through the middle of a pillar.
/// </summary>
public class PillarMiiddleScript : MonoBehaviour
{
    [SerializeField] private LogicScript logicScript;
    [SerializeField] private AudioClip scoreSound;

    private void Awake()
    {
        // Find LogicScript if not assigned in Inspector
        if (logicScript == null)
        {
            GameObject logicObj = GameObject.FindGameObjectWithTag("Logic");
            if (logicObj != null)
                logicObj.TryGetComponent(out logicScript);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Layer 3 is assumed to be the bird/player layer
        if (collision.gameObject.layer == 3 && logicScript != null)
        {
            logicScript.AddScore(1);
            PlayScoreSound();
        }
    }

    /// <summary>
    /// Plays the score sound effect.
    /// </summary>
    private void PlayScoreSound()
    {
        if (scoreSound != null)
            SoundFXManager.Instance?.PlaySoundFXClip(scoreSound, transform, 1f);
    }
}