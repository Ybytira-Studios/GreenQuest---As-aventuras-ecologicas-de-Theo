using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelRiver : MonoBehaviour
{
    public CanvasGroup dialoguePanel; // Painel de diálogo
    public TextMeshProUGUI dialogueText; // Texto do diálogo
    public GameObject topPanel;
    public string[] dialogues; // Array de diálogos
    public float displayDuration = 3f; // Duração de cada diálogo
    public float fadeDuration = 1f; // Duração para fade in/out
    public Button skipButton; // Botão para pular o diálogo
    public Timer timer;
    public PlayerControllerRiver playerControllerRiver;
    public AudioSource audioSourceMusicaRiver;

    private int currentDialogueIndex = 0;
    private bool dialogueRunning = true;

    void Start()
    {
        topPanel.SetActive(false);
        playerControllerRiver.enabled = false;
        timer.timerRunning = false;
        skipButton.onClick.AddListener(SkipDialogue);
        StartCoroutine(ShowDialogue());
    }

      void Update()
    {
        // Verifica se a tecla "Espaço" ou "Enter" foi pressionada
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            SkipDialogue();
        }
    }

   public IEnumerator ShowDialogue()
    {
        dialoguePanel.alpha = 0;
        dialoguePanel.gameObject.SetActive(true);

        while (currentDialogueIndex < dialogues.Length)
        {
            dialogueRunning = true; // Reseta para permitir o próximo diálogo

            // Exibe o diálogo atual
            dialogueText.text = dialogues[currentDialogueIndex];
            yield return StartCoroutine(FadeIn(dialoguePanel, fadeDuration));

            // Espera ou até o tempo passar ou o botão ser clicado
            float elapsed = 0;
            while (elapsed < displayDuration && dialogueRunning)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Faz fade out antes de passar ao próximo diálogo
            yield return StartCoroutine(FadeOut(dialoguePanel, fadeDuration));
            currentDialogueIndex++;
        }

        EndDialogue(); // Finaliza o diálogo
    }

    void SkipDialogue()
    {
        print("clickou");
        dialogueRunning = false; // Permite o avanço ao próximo diálogo
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

    void EndDialogue()
    {
        topPanel.SetActive(true);
        dialoguePanel.gameObject.SetActive(false);
        playerControllerRiver.enabled = true;
        timer.timerRunning = true;
        audioSourceMusicaRiver.Play();
        Debug.Log("Diálogo finalizado e jogo iniciado!");
    }
}
