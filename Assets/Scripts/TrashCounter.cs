using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCounter : MonoBehaviour
{
    public TMPro.TextMeshProUGUI trashCounterText;
    public Timer timer;
    public PlayerController playerController;
    public GameObject finishLevel;
    public TMPro.TextMeshProUGUI finalTimer;
    public Animator playerAnimator;

     public string[] tagsToCheck = { "MetalTrash", "GlassTrash", "PlasticTrash", "PaperTrash"};  // Array de tags para verificar

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

    foreach (string tag in tagsToCheck)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        totalTrashCount += objectsWithTag.Length;
    }
        trashCounterText.text = "Lixos restantes: " + totalTrashCount;

        if (totalTrashCount == 0 && timer.timerRunning) 
        {
            timer.timerRunning = false; // Para o timer
            Debug.Log("Timer parado, todos os objetos foram coletados.");
            playerController.enabled = false;
            timer.timerText.gameObject.SetActive(false);
            finishLevel.gameObject.SetActive(true);
            finalTimer.text = "Tempo restante: " + timer.timeLevel.ToString("F2");
            playerController.footStepAudioSource.Stop();
            playerAnimator.SetInteger("Movimento", 0);
        }
}


    void Update()
    {
        UpdateTrashCounter(); // Atualiza o contador a cada frame (ou em intervalos específicos)
    }
}