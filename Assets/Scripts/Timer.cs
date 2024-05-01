using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerText; // Texto para exibir o timer
    public string[] tagsToCheck = { "MetalTrash", "GlassTrash" }; // Array de tags para verificar

    private float timeLevel = 0f; // Inicialize o tempo
    private bool timerRunning = true; // Controle se o timer está ativo

    void Update()
    {
        if (timerRunning) // Se o timer está ativo, atualiza o tempo
        {
            timeLevel += Time.deltaTime; // Atualiza o timer
            timerText.text = "Tempo: " + timeLevel.ToString("F2"); // Exibe o tempo com duas casas decimais
        }

        int totalTrashCount = 0; // Reinicialize a contagem para zero

        // Soma todos os objetos com as tags especificadas
        foreach (string tag in tagsToCheck)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag); // Encontra objetos com a tag
            totalTrashCount += objectsWithTag.Length; // Soma ao total
        }

        // Se a contagem total for zero, pare o timer
        if (totalTrashCount == 0 && timerRunning) 
        {
            timerRunning = false; // Para o timer
            Debug.Log("Timer parado, todos os objetos foram coletados.");
        }
    }
}
