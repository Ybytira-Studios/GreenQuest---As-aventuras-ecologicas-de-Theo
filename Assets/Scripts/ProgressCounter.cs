using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressCounter : MonoBehaviour
{
    public int miniGamesCompletedCounter = 0;
    public int quantidadeMinigames = 2;
    private int quantidadeInversaParaExibir;
    public TMPro.TextMeshProUGUI trashCounterText;
    public TMPro.TextMeshProUGUI trashBinCounterText;
    public Timer timer;
    public Image timerIcon;
    public PlayerControllerRiver playerControllerRiver;
    public GameObject finishLevel;
    public TMPro.TextMeshProUGUI finalTimer;
    public Animator playerAnimator;
    public AudioSource audioSourcePlayer;

    public string tagToCheck = "Trash"; //tag para verificar

    void Start()
    {
         //Inicialmente, deixe o texto de vitória desativado
        if (finishLevel != null)
        {
           finishLevel.SetActive(false);
        }
    }


    void UpdateTrashCounter()
{

    int totalTrashCount = 0;
   
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToCheck);
        totalTrashCount += objectsWithTag.Length;
        quantidadeInversaParaExibir = quantidadeMinigames - miniGamesCompletedCounter;
        trashBinCounterText.text = "Lixeiras restantes: "+ quantidadeInversaParaExibir;
        trashCounterText.text = "Lixos restantes: " + totalTrashCount;

        if (totalTrashCount == 0 && timer.timerRunning && miniGamesCompletedCounter == quantidadeMinigames) 
        {
            timer.timerRunning = false; // Para o timer
            Debug.Log("Timer parado, todos os objetos foram coletados e minigames completados.");
            playerControllerRiver.enabled = false;
            timerIcon.gameObject.SetActive(false);
            finishLevel.SetActive(true);
            finalTimer.text = "Tempo restante: " + timer.timeLevel.ToString("F1") + "s";
            playerControllerRiver.footStepAudioSource.Stop();
            audioSourcePlayer.Stop();
        }
}


    void Update()
    {
        UpdateTrashCounter();
    }
}
