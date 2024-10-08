using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage;           // Referência à imagem do fade
    public AudioSource audioSource;   // Referência ao AudioSource
    public string nextScene;          // Nome da próxima cena
    public float fadeDuration = 2f;   // Duração do efeito de fade
    public float displayDuration = 4f; // Duração em que a imagem será exibida

    private void Start()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        // Fade in da imagem e do som
        yield return StartCoroutine(FadeOut());

        // Esperar a duração da imagem (4 segundos)
        yield return new WaitForSeconds(displayDuration);

        // Fade out da imagem e do som
        yield return StartCoroutine(FadeIn());

        // Carregar a próxima cena
        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f; // Começa completamente visível
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Reduzir a opacidade da imagem
            color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            fadeImage.color = color;

            // Reduzir o volume da música
            audioSource.volume = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            // Parar a música ao final do fade out
            audioSource.Stop();
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        // Começar o volume em 0 e tocar a música
        audioSource.volume = 0f;
        audioSource.Play();

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Aumentar a opacidade da imagem
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;

            // Aumenta o volume da música
            audioSource.volume = Mathf.Clamp01(elapsedTime / fadeDuration) + 3f;
            yield return null;

        }

        
    }
}
