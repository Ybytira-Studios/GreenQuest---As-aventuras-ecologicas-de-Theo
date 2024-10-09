using TMPro;
using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public TMP_Dropdown languageDropdown; // Referência ao Dropdown
    public Language language;

    void Start()
    {
        // Adiciona um listener para quando o valor do dropdown mudar
        languageDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(languageDropdown);
        });
    }

    // Método chamado quando o usuário muda o valor
    void DropdownValueChanged(TMP_Dropdown change)
    {
        // Verifica o valor selecionado
        switch (change.value)
        {
            case 0:
                Debug.Log("Idioma selecionado: Português");
                ChangeLanguage("pt");
                language.language = "pt";
                language.setPortuguese();
                break;
            case 1:
                Debug.Log("Idioma selecionado: Inglês");
                ChangeLanguage("en");
                language.language = "en";
                language.setEnglish();
                break;
            case 2:
                Debug.Log("Idioma selecionado: Espanhol");
                ChangeLanguage("es");
                language.language = "es";
                language.setEspanish();
                break;
            case 3:
                Debug.Log("Idioma selecionado: Francês");
                ChangeLanguage("fr");
                language.language = "fr";
                language.setFrench();
                break;
            default:
                ChangeLanguage("pt");
                language.language = "pt";
                language.setPortuguese();
                Debug.Log("Idioma desconhecido");
                break;
        }
    }

    // Função para trocar a língua globalmente
    void ChangeLanguage(string languageCode)
    {
        switch (languageCode)
        {
            case "pt":
                Language.Instance.setPortuguese();
                language.language = "pt";
                language.setPortuguese();
                break;
            case "en":
                Language.Instance.setEnglish();
                language.language = "en";
                language.setEnglish();
                break;
            case "es":
                Language.Instance.setEspanish();
                language.language = "es";
                language.setEspanish();
                break;
            case "fr":
                Language.Instance.setFrench();
                language.language = "fr";
                language.setFrench();
                break;
                default:
                ChangeLanguage("pt");
                language.language = "pt";
                language.setPortuguese();
                break;
        }
        Debug.Log("Idioma mudado para: " + Language.Instance.getLanguage());
    }
}
