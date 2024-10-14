using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLevel1 : MonoBehaviour
{
    public CanvasGroup introPanel; // Painel 1
    public CanvasGroup secondPanel; // Painel 2
    public Timer timer;
    public Image timerIcon;
    public Image trashIcon;
    public TextMeshProUGUI trashText;
    public TextMeshProUGUI welcomeText; // Texto de boas-vindas
    public TextMeshProUGUI memberText; // Texto do membro
    public TextMeshProUGUI metalText; // Texto para metal
    public TextMeshProUGUI plasticText; // Texto para plástico
    public TextMeshProUGUI paperText; // Texto para papel
    public TextMeshProUGUI glassText; // Texto para vidro
    public float displayDuration = 1f; // Duração do display de cada painel
    public float fadeDuration = 1f; // Duração do fade in/out
    public PlayerController playerController;
    public Animator playerAnimator;
    public AudioSource audioSourcePlayer;
    public AudioController audioControllerMusic;
    public Button introSkipButton; // Botão para pular o primeiro painel
    public Button secondSkipButton; // Botão para pular o segundo painel
    public KeyCode keyCodeSkip = KeyCode.Space; // Tecla para pular o painel
    public float skipDelay = 4f; // Tempo mínimo antes de poder pular o painel

    private bool introPanelVisible = true;
    private bool secondPanelVisible = false;
    private bool isGameStarted = false;
    private bool canSkip = false; // Variável para verificar se é possível pular
    private float timeUntilCanSkip = 0f; // Tempo até que o pulo seja permitido
    private string penis = "en"; // Idioma padrão

    void Start()
    {
        timerIcon.gameObject.SetActive(false);
        trashIcon.gameObject.SetActive(false);
        trashText.gameObject.SetActive(false);
        timer.timerRunning = false;
        playerController.enabled = false;

        // Adiciona listeners para os botões de pular
        introSkipButton.onClick.AddListener(SkipIntroPanel);
        secondSkipButton.onClick.AddListener(StartGame);

        // Inicia a música e a sequência de painéis
        audioControllerMusic.PlayBeachSound();
        SetWelcomeText(); // Atualiza o texto de boas-vindas
        SetMemberText();   // Atualiza o texto do membro
        SetSecondPanelText(); // Atualiza os textos do segundo painel
        StartCoroutine(ShowIntroPanels());
    }

    void Update()
    {
        // Verifica se o tempo de espera para pular foi alcançado
        if (timeUntilCanSkip > 0)
        {
            timeUntilCanSkip -= Time.deltaTime;
            if (timeUntilCanSkip <= 0)
            {
                canSkip = true; // Permite pular
            }
        }

        // Permite pular o painel atual usando Espaço ou Enter, se o tempo mínimo foi alcançado
        if (canSkip && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            if (introPanelVisible)
            {
                SkipIntroPanel(); // Pula o painel de introdução
            }
            else if (secondPanelVisible && !isGameStarted)
            {
                StartGame(); // Pula o segundo painel e inicia o jogo
            }
        }
    }

    IEnumerator ShowIntroPanels()
    {
        // Ativa o primeiro painel e impede o pulo até o tempo limite
        canSkip = false;
        yield return StartCoroutine(ShowIntroPanel(introPanel));
        introPanelVisible = false;

        yield return new WaitForSeconds(0.5f); // Pequeno intervalo entre os painéis
        secondPanelVisible = true;

        // Ativa o segundo painel e impede o pulo até o tempo limite
        canSkip = false;
        yield return StartCoroutine(ShowIntroPanel(secondPanel));
    }

    IEnumerator ShowIntroPanel(CanvasGroup panel)
    {
        panel.gameObject.SetActive(true); // Mostra o painel

        // Fade in
        yield return StartCoroutine(FadeIn(panel, fadeDuration));

        // Espera o tempo mínimo antes de permitir o pulo
        timeUntilCanSkip = skipDelay; // Inicia o timer
        yield return new WaitForSeconds(skipDelay);
        
        canSkip = true; // Agora o jogador pode pular

        // Espera até que o tempo de exibição do painel passe ou o jogador pule
        float elapsed = 0f;
        while (elapsed < displayDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Fade out
        yield return StartCoroutine(FadeOut(panel, fadeDuration));

        panel.gameObject.SetActive(false); // Esconde o painel após o fade out
        canSkip = false; // Impede o pulo até o próximo painel ser exibido
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

    // Método para pular do painel 1 para o 2
    void SkipIntroPanel()
    {
        if (!canSkip) return; // Se ainda não pode pular, retorna

        introPanelVisible = false;
        introSkipButton.interactable = false; // Desativa o botão de pulo do primeiro painel
        StopAllCoroutines(); // Para qualquer fade corrente
        StartCoroutine(SkipPanel(introPanel, secondPanel)); // Pula para o segundo painel com fade
    }

    IEnumerator SkipPanel(CanvasGroup currentPanel, CanvasGroup nextPanel)
    {
        // Faz o fade-out do painel atual e o desativa
        yield return StartCoroutine(FadeOut(currentPanel, fadeDuration));
        currentPanel.gameObject.SetActive(false);

        // Ativa e faz o fade-in do próximo painel
        nextPanel.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn(nextPanel, fadeDuration));

        secondSkipButton.interactable = true; // Ativa o botão de pulo do segundo painel
        timeUntilCanSkip = skipDelay; // Inicia o timer para o segundo painel
        canSkip = false; // Não permite pular até que o tempo seja atingido
    }

    // Método para iniciar o jogo
    void StartGame()
    {
        if (!canSkip || isGameStarted) return; // Se não pode pular ou já começou, retorna

        secondPanelVisible = false;
        isGameStarted = true; // Indica que o jogo começou
        secondSkipButton.interactable = false; // Desativa o botão de pulo do segundo painel
        StopAllCoroutines(); // Para qualquer fade corrente
        StartCoroutine(FadeOut(secondPanel, fadeDuration)); // Esconde o segundo painel e inicia o jogo

        timerIcon.gameObject.SetActive(true);
        trashIcon.gameObject.SetActive(true);
        trashText.gameObject.SetActive(true);
        audioControllerMusic.PlayBgMusic();
        playerController.enabled = true;
        timer.timerRunning = true;
        Debug.Log("Fase iniciada!");
    }

    // Função para atualizar o texto de boas-vindas
    void SetWelcomeText()
    {
        //Language languageScript = FindObjectOfType<Language>();
        switch (penis)
        {
            case "pt": // Português
                welcomeText.text = "Sejam muito bem vindos à esta atividade litorânea da LAL aqui na Baia da Aventura! A primeira atitude que temos que entender é: Não basta limparmos e despoluir, o foco principal é parar de poluir e sujar! Depois de entendido isso, temos que limpar essa triste sujeira que o povo fez aqui na cidade!";
                break;
            case "en": // Inglês
                welcomeText.text = "Welcome to this coastal activity of LAL here at Adventure Bay! The first thing we need to understand is: It’s not enough to clean and de-pollute; the main focus is to stop polluting and making a mess! Once we understand this, we need to clean up this sad mess that people have made here in the city!";
                break;
            case "es": // Espanhol
                welcomeText.text = "¡Bienvenidos a esta actividad costera de LAL aquí en la Bahía de la Aventura! ¡La primera actitud que tenemos que entender es: No basta con limpiar y descontaminar; el enfoque principal es dejar de contaminar y ensuciar! Después de entender esto, tenemos que limpiar esta triste suciedad que la gente ha hecho aquí en la ciudad!";
                break;
            case "fr": // Francês
                welcomeText.text = "Bienvenue à cette activité côtière de LAL ici à la Baie de l'Aventure ! La première chose que nous devons comprendre est : il ne suffit pas de nettoyer et de décontaminer ; le principal objectif est d'arrêter de polluer et de salir ! Une fois que nous avons compris cela, nous devons nettoyer ce triste désordre que les gens ont fait ici dans la ville !";
                break;
            // Adicione mais idiomas conforme necessário
        }
    }

    // Função para atualizar o texto do membro
     void SetMemberText()
    {
        //Language languageScript = FindObjectOfType<Language>();
        string memberName = "Leonardo"; // Nome padrão do membro
        switch (penis)
        {
            case "pt":
                memberText.text = "Membro: " + memberName;
                break;
            case "en":
                memberText.text = "Member: " + memberName;
                break;
            case "es":
                memberText.text = "Miembro: " + memberName;
                break;
            case "fr":
                memberText.text = "Membre: " + memberName;
                break;
            default:
                memberText.text = "Membro: " + memberName;
                break;
        }
    }

    // Função para atualizar os textos do segundo painel
    void SetSecondPanelText()
    {
        //Language languageScript = FindObjectOfType<Language>();
        switch (penis)
        {
            case "pt": // Português
                metalText.text = "Metal. Aqui você deve colocar latas de refrigerante, latas de conserva e papel alumínio.";
                plasticText.text = "Plástico. Aqui você deve colocar garrafas PET e embalagens de plástico.";
                paperText.text = "Papel e cartão. Aqui você deve colocar revistas, jornais e caixas de papelão.";
                glassText.text = "Vidro. Aqui você deve colocar garrafas de vidro, potes e frascos de vidro.";
                break;
            case "en": // Inglês
                metalText.text = "Metal. Here you should place soda cans, food cans, and aluminum foil.";
                plasticText.text = "Plastic. Here you should place PET bottles and plastic packaging.";
                paperText.text = "Paper and cardboard. Here you should place magazines, newspapers, and cardboard boxes.";
                glassText.text = "Glass. Here you should place glass bottles, jars, and glass containers.";
                break;
            case "es": // Espanhol
                metalText.text = "Metal. Aquí debes colocar latas de refresco, latas de conserva y papel aluminio.";
                plasticText.text = "Plástico. Aquí debes colocar botellas PET y envases de plástico.";
                paperText.text = "¡Papel y cartón. Aquí debes colocar revistas, periódicos y cajas de cartón.!";
                glassText.text = "¡Vidrio. Aquí debes colocar botellas de vidrio, frascos y tarros de vidrio.";
                break;
            case "fr": // Francês
                metalText.text = "Métal. Ici, vous devez mettre des canettes de soda, des boîtes de conserve et du papier d'aluminium.";
                plasticText.text = "Plastique. Ici, vous devez mettre des bouteilles PET et des emballages en plastique.";
                paperText.text = "Papier et carton. Ici, vous devez mettre des magazines, des journaux et des cartons.";
                glassText.text = "Verre. Ici, vous devez mettre des bouteilles en verre, des pots et des récipients en verre.";
                break;
        }
    }
}
