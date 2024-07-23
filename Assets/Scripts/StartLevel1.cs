using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLevel1 : MonoBehaviour
{
    public CanvasGroup introPanel; // Use CanvasGroup to control the alpha
    public CanvasGroup secondPanel; // Reference to the second panel
    public Timer timer;
    public Image timerIcon;
    public Image trashIcon;
    public TextMeshProUGUI trashText;
    public float displayDuration = 1f; // Duration the panel is displayed
    public float fadeDuration = 1f; // Duration for the fade in and fade out
    public float intervalBetweenPanels = 1f; // Interval time between the two panels
    public PlayerController playerController;
    public Animator playerAnimator;
    public AudioSource audioSourcePlayer;
    public AudioController audioControllerMusic;
    private bool canSkip;
    public KeyCode keyCodeSkip = KeyCode.Space;

    void Start()
    {
        timerIcon.gameObject.SetActive(false);
        trashIcon.gameObject.SetActive(false);
        trashText.gameObject.SetActive(false);
        timer.timerRunning = false;
        playerController.enabled = false;
        StartCoroutine(ShowIntroPanels());
        audioControllerMusic.PlayBeachSound();
    }

    IEnumerator ShowIntroPanels()
    {
        yield return StartCoroutine(ShowIntroPanel(introPanel));
        yield return new WaitForSeconds(intervalBetweenPanels);
        yield return StartCoroutine(ShowIntroPanel(secondPanel));
        StartGame(); // Start the game after the second panel
    }

    IEnumerator ShowIntroPanel(CanvasGroup panel)
    {
        panel.gameObject.SetActive(true); // Ensure the panel is active

        // Fade in
        yield return StartCoroutine(FadeIn(panel, fadeDuration));

        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        yield return StartCoroutine(FadeOut(panel, fadeDuration));

        panel.gameObject.SetActive(false); // Hide the panel after fade out
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, counter / duration);
            yield return null;
        }
    }

    IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, counter / duration);
            yield return null;
        }
    }

    void StartGame()
    {
        timerIcon.gameObject.SetActive(true);
        trashIcon.gameObject.SetActive(true);
        trashText.gameObject.SetActive(true);
        audioControllerMusic.PlayBgMusic();
        playerController.enabled = true;
        timer.timerRunning = true;
        Debug.Log("Fase iniciada!");
    }
}
