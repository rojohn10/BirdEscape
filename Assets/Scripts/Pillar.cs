using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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