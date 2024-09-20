using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class PorLixeira : MonoBehaviour
{
    public GameObject panel;
    public ApertaMuito apertaMuito;
    public GameObject imageKeyE;
    public PlayerControllerRiver playerControllerRiver;
    private bool isPlayerNearby = false; // Verifica se o jogador está perto
    public bool isPanelActive = false; // Estado do painel

    void Start()
    {
        imageKeyE.SetActive(false);
    }

    void Update()
    {
        if(apertaMuito.isCompleted)
        {
            imageKeyE.SetActive(false);
        }
        else 
        {
            imageKeyE.SetActive(isPlayerNearby);
        }

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if(!apertaMuito.isCompleted)
            {
                // Alterna o estado do painel
                TogglePanel();
            }
        }
    }

    void TogglePanel()
    {
        isPanelActive = !isPanelActive; // Alterna o estado do painel

        panel.SetActive(isPanelActive); // Ativa ou desativa o painel com base no novo estado

        if (playerControllerRiver != null)
        {
            playerControllerRiver.canMove = !isPanelActive; // Impede o jogador de se mover se o painel estiver ativo
        }

        Debug.Log(isPanelActive ? "Painel aberto." : "Painel fechado.");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true; // O jogador está perto
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false; // O jogador se afastou
        }
    }
}