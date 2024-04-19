using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public float proximityDistance = 3f; // Distância para considerar o objeto dentro da lixeira
    public string trashTag;

    private int trashCount = 0; // Contagem de objetos coletáveis

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(trashTag))
        {
            Debug.Log("trombou");
            // Verifica se o objeto está suficientemente próximo da lixeira
                Debug.Log("Entrou drento");
                Destroy(other.gameObject); // Destrói o objeto coletável
                trashCount--; // Decrementa a contagem
                CheckTrashCollected(); // Verifica se todos os lixos foram coletados
            
        }
    }

    void CheckTrashCollected()
    {
        if (trashCount <= 0)
        {
            Debug.Log("Todos os lixos foram coletados e destruídos!");
            // Faça algo aqui, como ativar um próximo nível ou exibir uma mensagem de conclusão
        }
    }

    public void IncrementTrashCount()
    {
        trashCount++; // Incrementa a contagem quando um novo objeto coletável é criado
    }
}
