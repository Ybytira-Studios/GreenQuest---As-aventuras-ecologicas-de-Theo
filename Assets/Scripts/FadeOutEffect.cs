using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Arraste o CanvasGroup aqui no Inspector

    private void Start()
    {
        // Inicie o fade out após 39 segundos
        StartCoroutine(StartFadeOutAfterDelay(50f));
    }

    private IEnumerator StartFadeOutAfterDelay(float delay)
    {
        // Espera pelo tempo especificado
        yield return new WaitForSeconds(delay);
        
        // Inicie o efeito de fade out
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float duration = 1.0f; // Duração do fade
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1 - (elapsedTime / duration); // Reduz a opacidade
            yield return null; // Espera até o próximo frame
        }

        canvasGroup.alpha = 0; // Garante que a opacidade final seja 0
    }
}
