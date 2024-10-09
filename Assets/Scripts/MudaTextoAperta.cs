using TMPro;
using UnityEngine;

public class MudaTextoAperta : MonoBehaviour
{
    public TextMeshProUGUI text1; // Primeiro texto
    public TextMeshProUGUI text2; // Segundo texto

    void Start()
    {
        UpdateTexts();
    }

    void UpdateTexts()
    {
        if (Language.Instance == null)
        {
            Debug.LogError("Language.Instance não está inicializada!");
            return;
        }
        // Verifica a linguagem atual e atualiza os textos
        switch (Language.Instance.getLanguage())
        {
            case "pt": // Português
                text1.text = "Pressione espaço para parafusar!".Replace(((char)23).ToString(), "");
                text2.text = "Pressione espaço para parafusar!".Replace(((char)23).ToString(), "");
                break;
            case "en": // Inglês
                text1.text = "Press space to screw!".Replace(((char)23).ToString(), "");
                text2.text = "Press space to screw!".Replace(((char)23).ToString(), "");
                break;
            case "es": // Espanhol
                text1.text = "Presiona espacio para atornillar!".Replace(((char)23).ToString(), "");
                text2.text = "Presiona espacio para atornillar!".Replace(((char)23).ToString(), "");
                break;
            case "fr": // Francês
                text1.text = "Appuyez sur espace pour visser!".Replace(((char)23).ToString(), "");
                text2.text = "Appuyez sur espace pour visser!".Replace(((char)23).ToString(), "");
                break;
            default: // Idioma padrão
                text1.text = "Pressione espaço para parafusar!".Replace(((char)23).ToString(), "");
                text2.text = "Pressione espaço para parafusar!".Replace(((char)23).ToString(), "");
                break;
        }
    }
}
