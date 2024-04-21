using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public float proximityDistance = 3f; // Distância para considerar o objeto dentro da lixeira
    public string trashTag;
    public TrashCounter trashCounter;

    public int trashCount = 11; // Contagem de objetos coletáveis

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(trashTag))
        {
                Destroy(other.gameObject); // Destrói o objeto coletável
                //trashCounter.IncrementCollectedTrash();
                trashCount--; // Decrementa a contagem
                CheckTrashCollected(); // Verifica se todos os lixos foram coletados
                Debug.Log(trashCount);
        }
    }

    void CheckTrashCollected()
    {
        if (trashCount == 0)
        {
            Debug.Log("Todos os lixos foram coletados e destruídos!");
            // Faça algo aqui, como ativar um próximo nível ou exibir uma mensagem de conclusão
        }
    }
}
