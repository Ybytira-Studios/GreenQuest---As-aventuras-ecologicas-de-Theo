using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Texto para exibir o timer
    public Animator playerAnimator;
    public TextMeshProUGUI endText; // Texto para exibir quando o tempo acabar
    public float timeLevel = 120f; // Inicialize o tempo
    public bool timerRunning = true; // Controle se o timer está ativo
    public PlayerController playerController;
    public PlayerControllerRiver playerControllerRiver;
    public string penis = "pt";

    private Language languageScript; // Declaração da variável languageScript

    void Start()
    {
    }

    void Update()
    {
        if (timerRunning) // Se o timer está ativo, atualiza o tempo
        {
            timeLevel -= Time.deltaTime; // Atualiza o timer
            timerText.text = GetTimerText(); // Exibe o tempo com duas casas decimais

            if (timeLevel <= 0)
            {
                timerRunning = false; // Para o timer
                if (SceneManager.GetActiveScene().name == "Fase1_beach")
                {
                    playerController.enabled = false; // Desativa o andar do player
                }
                else if (SceneManager.GetActiveScene().name == "Fase4_river")
                {
                    playerControllerRiver.enabled = false; // Desativa o andar do player
                }

                playerAnimator.SetInteger("Movimento", 0);
                playerControllerRiver.footStepAudioSource.Stop();
                timeLevel = 0; // Certifica que o tempo não fica negativo
                Invoke("RestartLevel", 1.5f); // Reinicia o nível após 1.5 segundos
            }
        }
    }

    // Função que retorna o texto do timer traduzido
    string GetTimerText()
    {
        string timerString = "";

        // Obter o idioma através de languageScript
        switch (Language.Instance.getLanguage())
        {
            case "pt":
                timerString = "Tempo: ";
                break;
            case "en":
                timerString = "Time: ";
                break;
            case "es":
                timerString = "Tiempo: ";
                break;
            case "fr":
                timerString = "Temps: ";
                break;
            default:
                timerString = "Tempo: ";
                break;
        }

        return timerString + Mathf.Max(timeLevel, 0f).ToString("F2");
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia a cena atual
    }
}
