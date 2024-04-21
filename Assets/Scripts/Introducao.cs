using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Introducao : MonoBehaviour
{
    string[] textos = new string[3];
    GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(rotine());

        textos[0] = "Baia da Aventura   uma cidade do litoral com belas praias e outras paisagens naturais, onde a principal fonte de renda é o turismo, ";
        textos[1] = "entretanto com o crescimento desordenado da cidade, o descarte irregular de lixo e a poluição da praia e dos rios aumentou drasticamente, pela nova popula  o local.";
        textos[2] = "Pensando nisso a LAL(Liga anti lixo), uma OSC ambiental que ajuda cidades dependentes do turismo a se reerguer de problemas ambientais, entrou em ação nesta cidade.";

        text = GameObject.Find("texto");
	text.GetComponent<Text>().text = textos[0];
    }

    int i = 0;

    public IEnumerator rotine()
    {
        //Habilita imagem
        GameObject img = GameObject.Find("img" + i);
	img.GetComponent<RawImage>().enabled = true;
        text.GetComponent<Text>().text = textos[i];
        i++;

        //Apaga anterior
        
        //Troca o texto

        if(i < 3)
        {
            yield return new WaitForSeconds(5);
            StartCoroutine(rotine());
        }
        else
        {
            //SceneManager.LoadScene("");
        }
    }

}