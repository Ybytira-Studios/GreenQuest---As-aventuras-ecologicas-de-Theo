using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZoomInEffect : MonoBehaviour
{
    public RectTransform imageRectTransform; // O RectTransform da imagem
    public float zoomDuration = 2f;          // Tempo de duração do zoom
    public Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f); // Escala de destino

    void Start()
    {
        StartCoroutine(ZoomIn(imageRectTransform, targetScale, zoomDuration));
    }

    IEnumerator ZoomIn(RectTransform rectTransform, Vector3 targetScale, float duration)
    {
        Vector3 initialScale = rectTransform.localScale;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = targetScale; // Certificar que a escala final é a correta
    }
}
