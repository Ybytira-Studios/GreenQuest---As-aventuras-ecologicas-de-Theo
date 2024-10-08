using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public float proximityDistance = 3f; // Distância para considerar o objeto dentro da lixeira
    public string trashTag;
    public AudioSource audioSource;
    public AudioClip dropEffect;

    public int trashCount = 11; // Contagem de objetos coletáveis

    void Start(){
        audioSource.clip = dropEffect;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Entrei no triggerEnter do thashbin");
        print("Objeto detectado: "+other.name);
        if (other.CompareTag(trashTag))
        {
            Destroy(other.gameObject); // Destrói o objeto coletável
            print("Destroi o lixo.");
            audioSource.Play();
            trashCount--; // Decrementa a contagem
            CheckTrashCollected(); // Verifica se todos os lixos foram coletado
        }
    }

    void CheckTrashCollected()
    {
        if (trashCount == 0)
        {
            Debug.Log("Todos os lixos foram coletados e destruídos!");
        }
    }
}
