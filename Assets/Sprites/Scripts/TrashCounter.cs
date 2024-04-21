using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCounter : MonoBehaviour
{
    public Text trashCounterText; // Referência ao objeto Texto que mostrará o contador
    public TrashBin trashBin;
    public int totalTrash = 10; // Número total de lixos na cena

    private int collectedTrash = 0; // Número de lixos coletados

    void Start()
    {
        Debug.Log("Startei");
        UpdateTrashCounter(); // Atualiza o contador quando o jogo começa
    }

    public void UpdateTrashCounter()
    {
        Debug.Log("Lixos coletados: " + trashBin.trashCount + "/" + totalTrash);
        trashCounterText.text = "Lixos coletados: " + trashBin.trashCount + "/" + totalTrash; // Atualiza o texto do contador
    }

    public void IncrementCollectedTrash()
    {
        collectedTrash++; // Incrementa o número de lixos coletados
        UpdateTrashCounter(); // Atualiza o contador
    }

    public void DecrementCollectedTrash()
    {
        collectedTrash--; // Decrementa o número de lixos coletados
        UpdateTrashCounter(); // Atualiza o contador
    }
}
