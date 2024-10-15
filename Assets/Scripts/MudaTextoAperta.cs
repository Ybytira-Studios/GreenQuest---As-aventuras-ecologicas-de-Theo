using TMPro;
using UnityEngine;

public class MudaTextoAperta : MonoBehaviour
{
    public TextMeshProUGUI text1; // Primeiro texto
    public TextMeshProUGUI text2; // Segundo texto
    public string penis = "pt";


    void Start()
    {
        UpdateTexts();
    }

    void UpdateTexts()
    {
        Language languageScript = FindObjectOfType<Language>();

        // Verifica a linguagem atual e atualiza os textos
        switch (languageScript.getLanguage())
        {
            case "pt": // Português
                text1.text = "Pressione espaco para parafusar!";
                text2.text = "Pressione espaco para parafusar!";
                break;
            case "en": // Inglês
                text1.text = "Press space to screw!";
                text2.text = "Press space to screw!";
                break;
            case "es": // Espanhol
                text1.text = "¡Presiona espacio para atornillar!";
                text2.text = "¡Presiona espacio para atornillar!";
                break;
            case "fr": // Francês
                text1.text = "Appuyez sur espace pour visser!";
                text2.text = "Appuyez sur espace pour visser!";
                break;
            default: // Idioma padrão
                text1.text = "Pressione espaço para parafusar!";
                text2.text = "Pressione espaço para parafusar!";
                break;
        }
    }
}
