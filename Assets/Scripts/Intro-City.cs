using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Cutscene1 : MonoBehaviour
{
    string[] textos = new string[3];
    public TextMeshProUGUI texto;
    public GameObject[] imagens; // Array de GameObjects para imagens

    void Start()
    {
        Language languageScript = FindObjectOfType<Language>();
        print("languageScript.getLanguage(): "+languageScript.getLanguage());
        Language lg = new Language();
        print("lingua: "+lg.getLanguage());

        switch(languageScript.getLanguage())
        {
            case "pt":
                textos[0] = "Baía da Aventura é uma cidade do litoral com belas praias e outras paisagens naturais,";
                textos[1] = "entretanto com o crescimento desordenado da cidade, o descarte irregular de lixo e a poluição das praias e dos rios aumentou drasticamente.";
                textos[2] = "Pensando nisso, as pessoas da cidade criaram a LAL(Liga anti lixo), uma OSC ambiental que vai ajudar a cidade a ficar limpa e bonita de novo.";
                break;

            case "en":
                textos[0] = "Baía da Aventura is a coastal city with beautiful beaches and other natural landscapes,";
                textos[1] = "however, with the uncontrolled growth of the city, improper waste disposal and pollution of beaches and rivers have drastically increased.";
                textos[2] = "With this in mind, the people of the city created LAL (Anti-Litter League), an environmental NGO that will help the city stay clean and beautiful again.";
                break;

            case "es":
                textos[0] = "Baía da Aventura es una ciudad costera con hermosas playas y otros paisajes naturales,";
                textos[1] = "sin embargo, con el crecimiento descontrolado de la ciudad, la eliminación inadecuada de residuos y la contaminación de las playas y ríos ha aumentado drásticamente.";
                textos[2] = "Pensando en eso, la gente de la ciudad creó la LAL (Liga Anti Basura), una ONG ambiental que ayudará a la ciudad a mantenerse limpia y hermosa de nuevo.";
                break;

            case "fr":
                textos[0] = "Baía da Aventura est une ville côtière avec de belles plages et d'autres paysages naturels,";
                textos[1] = "cependant, avec la croissance incontrôlée de la ville, le dépôt irrégulier de déchets et la pollution des plages et des rivières ont considérablement augmenté.";
                textos[2] = "En pensant à cela, les habitants de la ville ont créé la LAL (Ligue anti-déchets), une ONG environnementale qui aidera la ville à rester propre et belle à nouveau.";
                break;

            default:
                // Caso não encontre o idioma, exibe uma mensagem padrão (opcional)
                textos[0] = "Language not supported.";
                break;
        }

        texto.text = textos[0];

        StartCoroutine(Rotina());
    }

    int cont = 0;

    public IEnumerator Rotina()
    {

        if (cont < textos.Length)
        {
            // Ativa a imagem correspondente
            if (imagens[cont] != null)
            {
                if(cont == 1){
                    imagens[0].GetComponent<RawImage>().enabled = false;
                }else if(cont >=2){
                    imagens[1].GetComponent<RawImage>().enabled = false;
                }

                texto.text = textos[cont];
                Debug.Log("Texto exibido: " + texto.text);
                cont++;
                
                if(cont== 0){
                    yield return new WaitForSeconds(5);
                    StartCoroutine(Rotina());    
                }
                yield return new WaitForSeconds(10);
                StartCoroutine(Rotina());
            }
            else
            {
                Debug.LogWarning("Tentou acessar uma imagem nula no índice " + cont);
            }
        }
        else
        {
            SceneManager.LoadScene("Intro-Theo");
        }
    }
}
