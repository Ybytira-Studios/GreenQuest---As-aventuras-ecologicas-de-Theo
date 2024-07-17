using System.Collections;
using System.Collections.Generic;
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
        textos[0] = "Baia da Aventura é uma cidade do litoral com belas praias e outras paisagens naturais, onde a principal fonte de renda é o turismo, ";
        textos[1] = "entretanto com o crescimento desordenado da cidade, o descarte irregular de lixo e a poluição da praia e dos rios aumentou drasticamente, pela nova população local.";
        textos[2] = "Pensando nisso a LAL(Liga anti lixo), uma OSC ambiental que ajuda cidades dependentes do turismo a se reerguer de problemas ambientais, entrou em ação nesta cidade.";

        texto.text = textos[0];

        StartCoroutine(Rotina());
    }

    int cont = 0;

    public IEnumerator Rotina()
    {
        Debug.Log("Entrou na rotina");

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
                
                yield return new WaitForSeconds(8);
                StartCoroutine(Rotina());
            }
            else
            {
                Debug.LogWarning("Tentou acessar uma imagem nula no índice " + cont);
            }
        }
        else
        {
            Debug.Log("Todas as imagens foram exibidas. Carregando próxima cena.");
            SceneManager.LoadScene("Fase1_beach");
        }
    }
}
