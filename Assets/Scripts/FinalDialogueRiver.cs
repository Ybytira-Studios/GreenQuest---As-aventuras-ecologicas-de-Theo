using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalDialogueRiver : MonoBehaviour
{
    public CanvasGroup newDialoguePanel; // Painel de diálogo novo
    public TextMeshProUGUI newDialogueText; // Texto do novo diálogo
    public string[] newDialogues; // Array de novos diálogos
    public float newDisplayDuration = 3f; // Duração de cada diálogo
    public float newFadeDuration = 1f; // Duração para fade in/out
    public Button newSkipButton; // Botão para pular o diálogo

    private int currentDialogueIndex = 0;
    private bool dialogueRunning = true;

    void Start()
    {

        newSkipButton.onClick.AddListener(SkipNewDialogue);
        StartCoroutine(ShowNewDialogue());
    }

    public IEnumerator ShowNewDialogue()
    {
        newDialoguePanel.alpha = 0;
        newDialoguePanel.gameObject.SetActive(true);

        while (currentDialogueIndex < newDialogues.Length)
        {
            dialogueRunning = true;

            // Exibe o novo diálogo
            newDialogueText.text = newDialogues[currentDialogueIndex];
            yield return StartCoroutine(FadeIn(newDialoguePanel, newFadeDuration));

            // Espera ou permite pular o diálogo
            float elapsed = 0;
            while (elapsed < newDisplayDuration && dialogueRunning)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Fade out
            yield return StartCoroutine(FadeOut(newDialoguePanel, newFadeDuration));
            currentDialogueIndex++;
        }

        EndNewDialogue();
    }

    void SkipNewDialogue()
    {
        dialogueRunning = false;
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

    void EndNewDialogue()
    {
        newDialoguePanel.gameObject.SetActive(false);
        Debug.Log("Novo diálogo finalizado!");
        print("entrei nessa merda");
        SceneManager.LoadScene("Fase1_beach");
    }
}
