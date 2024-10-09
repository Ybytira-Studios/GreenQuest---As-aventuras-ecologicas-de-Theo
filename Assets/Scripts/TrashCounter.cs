using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCounter : MonoBehaviour
{
    public TMPro.TextMeshProUGUI trashCounterText;
    public Timer timer;
    public Image timerIcon;
    public PlayerController playerController;
    public GameObject finishLevel;
    public TMPro.TextMeshProUGUI finalTimer;
    public Animator playerAnimator;
    public AudioSource audioSourcePlayer;

    public string[] tagsToCheck = { "MetalTrash", "GlassTrash", "PlasticTrash", "PaperTrash" };  // Array de tags para verificar

    private string remainingTrashText;
    private string timeRemainingText;

    void Start()
    {
        // Inicialmente, deixe o texto de vitória desativado
        if (finishLevel != null)
        {
            finishLevel.SetActive(false);
        }

        // Inicializa os textos de acordo com a linguagem selecionada
        UpdateLocalization();
    }

    void UpdateLocalization()
    {
        switch (Language.Instance.getLanguage())
        {
            case "pt":
                remainingTrashText = "Lixos restantes: ";
                timeRemainingText = "Tempo restante: ";
                break;
            case "en":
                remainingTrashText = "Remaining trash: ";
                timeRemainingText = "Time remaining: ";
                break;
            case "es":
                remainingTrashText = "Basura restante: ";
                timeRemainingText = "Tiempo restante: ";
                break;
            case "fr":
                remainingTrashText = "Déchets restants: ";
                timeRemainingText = "Temps restant: ";
                break;
            default:
                remainingTrashText = "Remaining trash: ";
                timeRemainingText = "Time remaining: ";
                break;
        }
    }

    void UpdateTrashCounter()
    {
        int totalTrashCount = 0;

        foreach (string tag in tagsToCheck)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
            totalTrashCount += objectsWithTag.Length;
        }

        trashCounterText.text = remainingTrashText + totalTrashCount;

        if (totalTrashCount == 0 && timer.timerRunning)
        {
            timer.timerRunning = false; // Para o timer
            Debug.Log("Timer parado, todos os objetos foram coletados.");
            playerController.enabled = false;
            timer.gameObject.SetActive(false);
            timerIcon.gameObject.SetActive(false);
            finishLevel.SetActive(true);
            finalTimer.text = timeRemainingText + timer.timeLevel.ToString("F1") + "s";
            playerController.footStepAudioSource.Stop();
            audioSourcePlayer.Stop();
        }
    }

    void Update()
    {
        UpdateTrashCounter();
    }
}
