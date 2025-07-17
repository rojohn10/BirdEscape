using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Pillar : MonoBehaviour
//{
//    float maxY = -2f; // Maximum Y position for the pillar
//    float minY = -6f; // Minimum Y position for the pillar
//    public float speed = 3f; // Speed at which the pillar moves down    
//    public float spawnRate = 1f; // Rate at which pillars are spawned
//    public GameObject topPillar; // Reference to the top pillar GameObject
//    public GameObject bottomPillar; // Reference to the bottom pillar GameObject
//    //public GameObject middlePillar;

//    // Start is called before the first frame update
//    void Start()
//    {
//        SetPillars();
//    }

//    public void SetPillars()
//    {
//        float botY = Random.Range(minY, maxY);
//        float gap = Random.Range(10f, 10f);

//        bottomPillar.transform.localPosition = new Vector3(0, botY, 0);
//        topPillar.transform.localPosition = new Vector3(0, botY + gap, 0);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        transform.position += Vector3.left * Time.deltaTime * speed;
//        if (transform.position.x < -3.5f)
//        {
//            DestroyImmediate(gameObject); // Destroy the pillar when it goes off-screen
//        }
//    }
//}

/// <summary>
/// Controls the movement and positioning of a pillar pair.
/// </summary>
public class Pillar : MonoBehaviour
{
    [SerializeField] private float maxY = -2f;
    [SerializeField] private float minY = -6f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private GameObject topPillar;
    [SerializeField] private GameObject bottomPillar;

    private const float OffScreenX = -3.5f;
    private const float Gap = 10f;

    private void Start()
    {
        SetPillars();
    }

    /// <summary>
    /// Randomizes the vertical positions of the top and bottom pillars to create a gap.
    /// </summary>
    public void SetPillars()
    {
        if (topPillar == null || bottomPillar == null)
            return;

        float botY = Random.Range(minY, maxY);
        bottomPillar.transform.localPosition = new Vector3(0, botY, 0);
        topPillar.transform.localPosition = new Vector3(0, botY + Gap, 0);
    }

    public void Update()
    {
        if (Time.timeScale == 0f)
            return;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < OffScreenX)
        {
            Destroy(gameObject);
        }
    }
}