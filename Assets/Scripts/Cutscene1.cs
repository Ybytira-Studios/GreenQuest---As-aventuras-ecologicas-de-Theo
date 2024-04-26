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
    public RawImage[] imagens; // Array de RawImage
    private Image[] imageComponents; // Array de Image

    void Start()
    {
        textos[0] = "Baia da Aventura é uma cidade do litoral com belas praias e outras paisagens naturais, onde a principal fonte de renda é o turismo, ";
        textos[1] = "entretanto com o crescimento desordenado da cidade, o descarte irregular de lixo e a poluição da praia e dos rios aumentou drasticamente, pela nova população local.";
        textos[2] = "Pensando nisso a LAL(Liga anti lixo), uma OSC ambiental que ajuda cidades dependentes do turismo a se reerguer de problemas ambientais, entrou em ação nesta cidade.";

        texto.text = textos[0];

        // Obtém os componentes Image associados às RawImages
        imageComponents = new Image[imagens.Length];
        for (int i = 0; i < imagens.Length; i++)
        {
            imageComponents[i] = imagens[i].GetComponent<Image>();
        }

        StartCoroutine(Rotina());
    }

    int cont = 0;

    public IEnumerator Rotina()
    {
        // Torna a imagem visível alterando sua cor alpha
        Color cor = imageComponents[cont].color;
        cor.a = 1f; // Define o valor alpha como 1 para tornar a imagem totalmente visível
        imageComponents[cont].color = cor;

        texto.text = textos[cont];
        cont++;

        if (cont < 3)
        {
            yield return new WaitForSeconds(5);
            StartCoroutine(Rotina());
        }
        else
        {
            SceneManager.LoadScene("Fase1_beach");
        }
    }
}