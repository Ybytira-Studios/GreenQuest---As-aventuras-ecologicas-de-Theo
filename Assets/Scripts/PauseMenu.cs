using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar a cena do menu principal

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false; // Controle de estado de pausa
    public PlayerControllerRiver playerControllerRiver; // Referência ao PlayerController
    public PlayerController playerController;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Continue(); // Retorna ao jogo se já estiver pausado
            }
            else
            {
                Pause(); // Pausa o jogo
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Pausa o tempo
        isPaused = true;

        // Pausa o áudio e animação do player
        if (SceneManager.GetActiveScene().name == "Fase4_river")
        {
            if (playerControllerRiver.footStepAudioSource.isPlaying)
            {
                playerControllerRiver.footStepAudioSource.Pause(); // Para o áudio de passos
            }
            playerControllerRiver.playerAnimator.enabled = false; // Pausa a animação do player
            playerControllerRiver.canMove = false; // Impede o movimento
        }
        else if (SceneManager.GetActiveScene().name == "Fase1_beach")
        {
            if (playerController.footStepAudioSource.isPlaying)
            {
                playerController.footStepAudioSource.Pause(); // Para o áudio de passos
            }
            playerController.playerAnimator.enabled = false; // Pausa a animação do player
            playerController.canMove = false; // Impede o movimento
        }
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Retorna o tempo ao normal
        isPaused = false;

        // Retoma o áudio e animação do player
        if (SceneManager.GetActiveScene().name == "Fase4_river")
        {
            playerControllerRiver.footStepAudioSource.UnPause(); // Retoma o áudio de passos
            playerControllerRiver.playerAnimator.enabled = true; // Retoma a animação do player
            playerControllerRiver.canMove = true; // Permite o movimento novamente
        }
        else if (SceneManager.GetActiveScene().name == "Fase1_beach")
        {
            playerController.footStepAudioSource.UnPause(); // Retoma o áudio de passos
            playerController.playerAnimator.enabled = true; // Retoma a animação do player
            playerController.canMove = true; // Permite o movimento novamente
        }
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1; // Certifique-se de restaurar o tempo antes de sair
        SceneManager.LoadScene("MainMenu"); // Carrega a cena do menu principal
    }
}
