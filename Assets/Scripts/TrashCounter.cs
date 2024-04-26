using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCounter : MonoBehaviour
{
    public TMPro.TextMeshProUGUI trashCounterText; // Referência ao objeto de texto na UI
      public string[] tagsToCheck = { "MetalTrash", "GlassTrash"};  // Array de tags para verificar

    void UpdateTrashCounter()
{

    int totalTrashCount = 0;

    foreach (string tag in tagsToCheck)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        totalTrashCount += objectsWithTag.Length;
    }
        trashCounterText.text = "Lixos restantes: " + totalTrashCount;
}
    void Update()
    {
        UpdateTrashCounter(); // Atualiza o contador a cada frame (ou em intervalos específicos)
    }
}