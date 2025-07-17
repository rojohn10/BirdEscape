using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Blinks a UI Text component by toggling its visibility at a set interval.
/// </summary>
public class TextBlink : MonoBehaviour
{
    [SerializeField] private Text uiText;         // Assign in Inspector
    [SerializeField] private float blinkInterval = 0.5f;

    private void Start()
    {
        if (uiText != null)
            StartCoroutine(Blink());
    }

    private System.Collections.IEnumerator Blink()
    {
        while (true)
        {
            uiText.enabled = !uiText.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}