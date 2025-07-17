using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class FloorScript : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Time.timeScale == 1)
//        {
//            transform.Translate(-0.08f, 0, 0);
//        }

//        if (transform.position.x <= -5.32f)
//        {
//            Vector2 vector2 = transform.position;
//            vector2.x = 0;
//            transform.position = vector2;
//        }
//    }
//}


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