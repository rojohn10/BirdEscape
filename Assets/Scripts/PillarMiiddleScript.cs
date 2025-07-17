using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class PillarMiiddleScript : MonoBehaviour
//{
//    public LogicScript logicScript;
//    [SerializeField] private AudioClip scoreSound;

//    // Start is called before the first frame update
//    void Start()
//    {
//        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.layer == 3)
//        {
//            logicScript.AddScore(1);
//            PlayScoreSound();
//        }
//    }

//    private void PlayScoreSound()
//    {
//        SoundFXManager.Instance.PlaySoundFXClip(scoreSound, transform, 1f);
//    }
//}


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