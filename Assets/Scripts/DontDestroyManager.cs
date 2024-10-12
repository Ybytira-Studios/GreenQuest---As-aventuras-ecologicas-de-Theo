using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    // Lista de objetos que você deseja manter entre as cenas
    [SerializeField] private List<GameObject> objectsToPersist;

    void Awake()
    {
        // Itera sobre todos os objetos na lista e aplica DontDestroyOnLoad
        foreach (GameObject obj in objectsToPersist)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
