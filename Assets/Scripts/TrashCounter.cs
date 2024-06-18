using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCounter : MonoBehaviour
{
    public TMPro.TextMeshProUGUI trashCounterText;

    public TMPro.TextMeshProUGUI youWon;

     public string[] tagsToCheck = { "MetalTrash", "GlassTrash", "PlasticTrash", "PaperTrash"};  // Array de tags para verificar

         void Start()
    {
        // Inicialmente, deixe o texto de vitória desativado
        if (youWon != null)
        {
           youWon.gameObject.SetActive(false);
        }
    }

     

    void UpdateTrashCounter()
{

    int totalTrashCount = 0;

    foreach (string tag in tagsToCheck)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        totalTrashCount += objectsWithTag.Length;
    }
        trashCounterText.text = "Lixos restantes: " + totalTrashCount;

        if(totalTrashCount == 0){
            youWon.gameObject.SetActive(true);
        }
}


    void Update()
    {
        UpdateTrashCounter(); // Atualiza o contador a cada frame (ou em intervalos específicos)
    }
}