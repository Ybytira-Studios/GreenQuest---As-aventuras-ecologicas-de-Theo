using UnityEngine;

public class PorLixeira : MonoBehaviour
{
    public GameObject panel;
    public ApertaMuito apertaMuito;
    public PlayerControllerRiver playerControllerRiver;
    private bool isPlayerNearby = false; // Verifica se o jogador está perto
    public bool isPanelActive = false; // Estado do painel

    void Start()
    {
        //panel.SetActive(false); // Certifica-se de que o painel está desativado no início
    }

    void Update()
    {
        // Verifica se o jogador está perto e pressionou a tecla "E"
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if(!apertaMuito.isCompleted){
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // O jogador está perto
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // O jogador se afastou
        }
    }
}
