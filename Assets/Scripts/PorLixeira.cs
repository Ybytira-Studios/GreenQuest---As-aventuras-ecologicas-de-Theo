using UnityEngine;

public class PorLixeira : MonoBehaviour
{
    public GameObject panel;
    private bool isPlayerNearby = false; // Verifica se o jogador está perto
    public bool isPanelActive;

    void Start()
    {
        panel.SetActive(false); // Certifica-se de que o painel está desativado no início
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Verifica se o painel não está ativo antes de tentar abrir
            if (!panel.activeSelf)
            {
                isPanelActive = !panel.activeSelf;
                panel.SetActive(isPanelActive); // Ativa ou desativa o painel
                Debug.Log(isPanelActive ? "Painel aberto." : "Painel fechado.");
            }
        }
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
