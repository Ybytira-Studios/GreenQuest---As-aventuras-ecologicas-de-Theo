using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    public static Language Instance { get; private set; } // Instância Singleton
    public string language;

    private void Awake()
    {
        // Se já existir uma instância, destrua este objeto
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantém a instância entre as cenas
    }

    public void setPortuguese() { language = "pt"; }
    public void setEnglish() { language = "en"; }
    public void setEspanish() { language = "es"; }
    public void setFrench() { language = "fr"; }
    public string getLanguage() { return language; }
}
