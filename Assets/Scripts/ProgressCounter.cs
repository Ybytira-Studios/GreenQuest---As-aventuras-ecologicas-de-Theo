using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCounter : MonoBehaviour
{
    public int miniGamesCompletedCounter = 0;
    public int quantidadeMinigames = 2;
    private int quantidadeInversaParaExibir;
    public TextMeshProUGUI trashCounterText;
    public TextMeshProUGUI trashBinCounterText;
    public Timer timer;
    public Image timerIcon;
    public PlayerControllerRiver playerControllerRiver;
    public GameObject finishLevel;
    public TextMeshProUGUI finalTimer;
    public Animator playerAnimator;
    public AudioSource audioSourcePlayer;

    public string tagToCheck = "Trash"; // tag para verificar

    void Start()
    {
        if (finishLevel != null)
        {
            finishLevel.SetActive(false); // Inicialmente, deixe o texto de vitória desativado
        }
        UpdateTrashCounterText(); // Atualizar os textos com base no idioma
    }

    void UpdateTrashCounter()
    {
        int totalTrashCount = 0;
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToCheck);
        totalTrashCount += objectsWithTag.Length;
        quantidadeInversaParaExibir = quantidadeMinigames - miniGamesCompletedCounter;

        // Atualizar textos com base no idioma
        UpdateTrashCounterText();

        if (totalTrashCount == 0 && timer.timerRunning && miniGamesCompletedCounter == quantidadeMinigames)
        {
            timer.timerRunning = false; // Para o timer
            playerControllerRiver.enabled = false;
            timerIcon.gameObject.SetActive(false);
            finishLevel.SetActive(true);
            finalTimer.text = GetFinalTimerText(); // Texto traduzido para o tempo final
            playerControllerRiver.footStepAudioSource.Stop();
            audioSourcePlayer.Stop();
        }
    }

    // Função que atualiza os textos com base no idioma
    void UpdateTrashCounterText()
    {
        string trashCounterString = "";
        string trashBinCounterString = "";
        string language = Language.Instance.getLanguage();

        switch (language)
        {
            case "pt":
                trashCounterString = "Lixos restantes: ";
                trashBinCounterString = "Lixeiras restantes: ";
                break;
            case "en":
                trashCounterString = "Remaining trash: ";
                trashBinCounterString = "Remaining trash bins: ";
                break;
            case "es":
                trashCounterString = "Basuras restantes: ";
                trashBinCounterString = "Papelera restante: ";
                break;
            case "fr":
                trashCounterString = "Déchets restants : ";
                trashBinCounterString = "Poubelles restantes : ";
                break;
            default:
                trashCounterString = "Lixos restantes: ";
                trashBinCounterString = "Lixeiras restantes: ";
                break;
        }

        trashCounterText.text = trashCounterString + GameObject.FindGameObjectsWithTag(tagToCheck).Length;
        trashBinCounterText.text = trashBinCounterString + quantidadeInversaParaExibir;
    }

    // Função que obtém o texto final do timer traduzido
    string GetFinalTimerText()
    {
        string finalTimerString = "";
        string language = Language.Instance.getLanguage();

        switch (language)
        {
            case "pt":
                finalTimerString = "Tempo restante: ";
                break;
            case "en":
                finalTimerString = "Remaining time: ";
                break;
            case "es":
                finalTimerString = "Tiempo restante: ";
                break;
            case "French":
                finalTimerString = "Temps restant : ";
                break;
            default:
                finalTimerString = "Tempo restante: ";
                break;
        }

        return finalTimerString + timer.timeLevel.ToString("F1") + "s";
    }

    void Update()
    {
        UpdateTrashCounter();
    }
}
