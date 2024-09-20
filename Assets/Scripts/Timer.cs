using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerText; // Texto para exibir o timer
    public AudioSource audioSourcePlayer;
    public Animator playerAnimator;
    public TextMeshProUGUI endText; // Texto para exibir quando o tempo acabar
    public float timeLevel = 120f; // Inicialize o tempo
    public bool timerRunning = true; // Controle se o timer está ativo
    public PlayerController playerController;
    public PlayerControllerRiver playerControllerRiver;
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
                if(SceneManager.GetActiveScene().name == "Fase1_beach"){
                playerController.enabled = false; // Desativa o andar do player
                } else if(SceneManager.GetActiveScene().name == "Fase4_river"){
                playerControllerRiver.enabled = false; // Desativa o andar do player
                }
                playerAnimator.SetInteger("Movimento", 0);
                playerControllerRiver.footStepAudioSource.Stop();
                timeLevel = 0; // Certifica que o tempo não fica negativo
                //endText.gameObject.SetActive(true); // Exibe o texto de fim
                Invoke("RestartLevel", 1.5f); // Reinicia o nível após 3 segundos
            }
        }

        if(SceneManager.GetActiveScene().name == "Fase1_beach"){
                starController.CheckStars(timeLevel); // Verifica o número de estrelas com base no tempo restante
                }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia a cena atual
    }
}
