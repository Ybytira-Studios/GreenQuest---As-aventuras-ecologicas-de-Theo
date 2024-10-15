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
    public string penis = "pt";
    public Image blackScreen; // Imagem preta para fade out
    public float fadeToBlackDuration = 2f; // Duração do fade to black

    private int currentDialogueIndex = 0;
    private bool dialogueRunning = true;

    void Start()
    {
        blackScreen.gameObject.SetActive(false); // Certifique-se de que a tela preta esteja desativada inicialmente
        newSkipButton.onClick.AddListener(SkipNewDialogue);
        Language languageScript = FindObjectOfType<Language>();

        // Definindo os diálogos com base na linguagem
        switch (languageScript.getLanguage())
        {
            case "pt":
                newDialogues = new string[]
                {
                    "Muito bem Theo! Você foi muito eficiente para pegar o lixo e instalar as lixeiras!", // Posição 0
                    "Vamos para casa que amanhã a LAL estará na praia da ampulheta e nós vamos acompanhá-los." // Posição 1
                };
                break;

            case "en":
                newDialogues = new string[]
                {
                    "Well done Theo! You were very efficient in collecting trash and installing the bins!", // Posição 0
                    "Let's go home, tomorrow the LAL will be at Hourglass Beach and we'll join them." // Posição 1
                };
                break;

            case "es":
                newDialogues = new string[]
                {
                    "¡Muy bien Theo! ¡Fuiste muy eficiente para recoger la basura e instalar los contenedores!", // Posição 0
                    "Vamos a casa, mañana la LAL estará en la Playa del Reloj de Arena y los acompañaremos." // Posição 1
                };
                break;

            case "fr":
                newDialogues = new string[]
                {
                    "Bien joué Theo ! Tu as été très efficace pour ramasser les déchets et installer les poubelles !", // Posição 0
                    "Allons à la maison, demain la LAL sera à la plage du sablier et nous les accompagnerons." // Posição 1
                };
                break;

            default:
              newDialogues = new string[]
                {
                    "Muito bem Theo! Você foi muito eficiente para pegar o lixo e instalar as lixeiras!", // Posição 0
                    "Vamos para casa que amanhã a LAL estará na praia da ampulheta e nós vamos acompanhá-los." // Posição 1
                };
                break;
        }

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

        yield return StartCoroutine(FadeToBlack()); // Aplica o fade to black antes de trocar a cena
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

    IEnumerator FadeToBlack()
    {
        blackScreen.gameObject.SetActive(true); // Ativa a tela preta
        Color screenColor = blackScreen.color;
        float counter = 0f;

        // Faz o fade in da tela preta
        while (counter < fadeToBlackDuration)
        {
            counter += Time.deltaTime;
            screenColor.a = Mathf.Lerp(0, 1, counter / fadeToBlackDuration);
            blackScreen.color = screenColor;
            yield return null;
        }
    }

    void EndNewDialogue()
    {
        // Carrega a nova cena após o fade to black
        SceneManager.LoadScene("Fase1_beach");
    }
}
