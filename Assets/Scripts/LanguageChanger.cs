using TMPro;
using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown languageDropdown;
    private Language language;

    void Awake()
    {
        language = FindObjectOfType<Language>();
        languageDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(languageDropdown);
        });
    }

    // Método chamado quando o usuário muda o valor
    void DropdownValueChanged(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                ChangeLanguage("pt");
                break;
            case 1:
                ChangeLanguage("en");
                break;
            case 2:
                ChangeLanguage("es");
                break;
            case 3:
                ChangeLanguage("fr");
                break;
            default:
                ChangeLanguage("pt");
                break;
        }
    }

    void ChangeLanguage(string languageCode)
    {
        switch (languageCode)
        {
            case "pt":
                language.setPortuguese();
                break;
            case "en":
                language.setEnglish();
                break;
            case "es":
                language.setEspanish();
                break;
            case "fr":
                language.setFrench();
                break;
        }
        Debug.Log("Idioma mudado para: " + language.getLanguage());
    }
}
