using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelRiver : MonoBehaviour
{
    public CanvasGroup introPanel; // Use CanvasGroup to control the alpha
    public CanvasGroup secondPanel; // Reference to the second panel
    public Timer timer;
    public GameObject topPanel;
    public float displayDuration = 1f; // Duration the panel is displayed
    public float fadeDuration = 1f; // Duration for the fade in and fade out
    public float intervalBetweenPanels = 1f; // Interval time between the two panels
    public PlayerControllerRiver playerControllerRiver;
    public Animator playerAnimator;
    public AudioSource audioSourcePlayer;
    public AudioSource audioSourceMusicaRiver;
    public Button skipButton; // Button to skip the dialogue
    private bool skipPanel = false; // Flag to check if the player wants to skip

    void Start()
    {
        topPanel.SetActive(false);
        timer.timerRunning = false;
        playerControllerRiver.enabled = false;
        
        skipButton.gameObject.SetActive(true); // Ensure the button is active
        skipButton.onClick.AddListener(SkipDialogue); // Add the skip function to the button listener
        
        StartCoroutine(ShowIntroPanels());
    }

    // This method will be called when the skip button is pressed
    void SkipDialogue()
    {
        skipPanel = true;
    }

    IEnumerator ShowIntroPanels()
    {
        // Show the first panel
        yield return StartCoroutine(ShowIntroPanel(introPanel));

        // Wait for the interval before showing the second panel
        yield return new WaitForSeconds(intervalBetweenPanels);

        // Show the second panel
        yield return StartCoroutine(ShowIntroPanel(secondPanel));

        // Start the game after the second panel
        StartGame();
    }

    IEnumerator ShowIntroPanel(CanvasGroup panel)
    {
        panel.gameObject.SetActive(true); // Ensure the panel is active

        // Fade in
        yield return StartCoroutine(FadeIn(panel, fadeDuration));

        // Check if the panel should be skipped
        float elapsedTime = 0f;
        while (elapsedTime < displayDuration && !skipPanel)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the skip flag for the next panel
        skipPanel = false;

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
        topPanel.SetActive(true);
        audioSourceMusicaRiver.Play();
        playerControllerRiver.enabled = true;
        timer.timerRunning = true;
        Debug.Log("Fase iniciada!");
        skipButton.gameObject.SetActive(false); // Hide the skip button once the game starts
    }
}
