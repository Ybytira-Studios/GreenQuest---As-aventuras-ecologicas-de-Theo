using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerText; // Texto para exibir o timer
    public TMPro.TextMeshProUGUI endText; // Texto para exibir quando o tempo acabar
    public float timeLevel = 120f; // Inicialize o tempo
    public bool timerRunning = true; // Controle se o timer está ativo
    public MonoBehaviour playerController;
    public StarController starController;

    void Update()
    {
        if (timerRunning) // Se o timer está ativo, atualiza o tempo
        {
            timeLevel -= Time.deltaTime; // Atualiza o timer
            timerText.text = "Tempo: " + Mathf.Max(timeLevel, 0f).ToString("F2"); // Exibe o tempo com duas casas decimais

            if (timeLevel <= 0)
            {
                timerRunning = false; // Para o timer
                timeLevel = 0; // Certifica que o tempo não fica negativo
                endText.gameObject.SetActive(true); // Exibe o texto de fim
                Invoke("RestartLevel", 3f); // Reinicia o nível após 3 segundos
            }
        }

        starController.CheckStars(timeLevel); // Verifica o número de estrelas com base no tempo restante
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia a cena atual
    }
}
