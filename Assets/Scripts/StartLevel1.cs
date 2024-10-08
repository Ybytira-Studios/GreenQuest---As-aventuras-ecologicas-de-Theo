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
}
  