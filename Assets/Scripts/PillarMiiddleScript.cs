using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarMiiddleScript : MonoBehaviour
{
    public LogicScript logicScript;
    [SerializeField] private AudioClip scoreSound;

    // Start is called before the first frame update
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logicScript.AddScore(1);
            PlayScoreSound();
        }
    }

    private void PlayScoreSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip(scoreSound, transform, 1f);
    }
}
    