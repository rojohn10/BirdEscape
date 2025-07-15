using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//public class SoundFXManager : MonoBehaviour
//{
//    public static SoundFXManager instance;
//    [SerializeField] private AudioSource soundFXObject;

//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//        }
//    }

//    public void PlaySoundFXClip(AudioClip audioClip, Transform transform, float volume)
//    {
//        //spawn a new AudioSource at the given transform's position
//        AudioSource audioSource = Instantiate(soundFXObject, transform.position, quaternion.identity);

//        //assign the audio clip and volume to the AudioSource
//        audioSource.clip = audioClip;

//        //assign the volume to the AudioSource
//        audioSource.volume = volume;

//        //play the audio clip
//        audioSource.Play();

//        // Get the length of the audio clip
//        float clipLength = audioSource.clip.length;

//        //destroy the AudioSource after it finishes playing
//        Destroy(audioSource.gameObject, clipLength); // Destroy the audio source after the clip finishes playing
//    }
//}

/// <summary>
/// Manages and plays sound effects in the game.
/// </summary>
public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance { get; private set; }

    [SerializeField] private AudioSource soundFXPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Uncomment the next line if you want this manager to persist across scenes
            // DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Plays a sound effect at the given transform's position.
    /// </summary>
    /// <param name="audioClip">The audio clip to play.</param>
    /// <param name="transform">The transform at which to play the sound.</param>
    /// <param name="volume">The volume of the sound.</param>
    public void PlaySoundFXClip(AudioClip audioClip, Transform transform, float volume)
    {
        if (soundFXPrefab == null || audioClip == null || transform == null)
            return;

        // Spawn a new AudioSource at the given transform's position
        AudioSource audioSource = Instantiate(soundFXPrefab, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(audioSource.gameObject, audioClip.length);
    }
}
