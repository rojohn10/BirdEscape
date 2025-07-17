using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the floor's movement and looping behavior.
/// </summary>
public class FloorScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.08f;
    [SerializeField] private float resetPositionX = -5.32f;
    [SerializeField] private float startPositionX = 0f;

    public void Update()
    {
        if (Time.timeScale != 1f)
            return;

        // Move the floor left
        transform.Translate(-moveSpeed, 0f, 0f);

        // Reset position if it goes past the threshold
        if (transform.position.x <= resetPositionX)
        {
            Vector3 pos = transform.position;
            pos.x = startPositionX;
            transform.position = pos;
        }
    }
}