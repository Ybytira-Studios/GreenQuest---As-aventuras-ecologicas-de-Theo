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
    public string penis = "pt"; // Isso parece um nome de variável não intencional, considere renomear

    private int currentDialogueIndex = 0;
    private bool dialogueRunning = true;

    void Start()
    {
        topPanel.SetActive(false);
        playerControllerRiver.enabled = false;
        timer.timerRunning = false;
        skipButton.onClick.AddListener(SkipDialogue);
        
        switch (Language.Instance.getLanguage())
        {
            case "pt":
                dialogues = new string[]
                {
                    "Nós podemos ajudar a população local a retirar os dejetos do rio. Você não pode entrar no córrego como os profissionais, filho, pois é perigoso! Ao invés disso, use a garra limpa-limpa para pegar o lixo estando na margem!".Replace(((char)23).ToString(), ""),
                    "Além disso, precisamos instalar lixeiras por aqui também, pois isso estimula as pessoas a não jogarem lixo na natureza." // Posição 1
                };
                break;

            case "en":
                dialogues = new string[]
                {
                    "We can help the local community by removing debris from the river. You can't go into the stream like the professionals, son, because it's dangerous! Instead, use the clean-clean claw to grab the trash from the shore!",
                    "Also, we need to install trash bins around here, as this encourages people not to litter in nature."
                };
                break;

            case "es":
                dialogues = new string[]
                {
                    "Podemos ayudar a la comunidad local retirando los desechos del río. No puedes entrar en el arroyo como los profesionales, hijo, ¡porque es peligroso! En lugar de eso, usa la garra limpia-limpa para recoger la basura desde la orilla.",
                    "Además, tenemos que instalar contenedores de basura aquí, ya que esto anima a la gente a no tirar basura en la naturaleza."
                };
                break;

            case "fr":
                dialogues = new string[]
                {
                    "Nous pouvons aider la communauté locale en retirant les débris de la rivière. Tu ne peux pas entrer dans le ruisseau comme les professionnels, mon fils, car c'est dangereux ! Utilise plutôt la pince propre-propre pour ramasser les déchets depuis la rive !",
                    "De plus, nous devons installer des poubelles ici aussi, car cela encourage les gens à ne pas jeter leurs déchets dans la nature."
                };
                break;

            default:
                dialogues = new string[]
                {
                    "Idioma não suportado."
                };
                break;
        }

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
        print("Clique para pular o diálogo");
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
