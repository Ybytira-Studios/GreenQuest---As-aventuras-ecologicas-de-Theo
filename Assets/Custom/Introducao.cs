using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Introducao : MonoBehaviour
{
    string[] textos = new string[3];
    TextMeshPro textMeshPro; // Usamos TextMeshPro em vez de TextMesh

    // Start is called before the first frame update
    void Start()
    {
        textos[0] = "Baia da Aventura é uma cidade do litoral com belas praias e outras paisagens naturais, onde a principal fonte de renda é o turismo, ";
        textos[1] = "entretanto com o crescimento desordenado da cidade, o descarte irregular de lixo e a poluição da praia e dos rios aumentou drasticamente, pela nova população local.";
        textos[2] = "Pensando nisso a LAL(Liga anti lixo), uma OSC ambiental que ajuda cidades dependentes do turismo a se reerguer de problemas ambientais, entrou em ação nesta cidade.";

        textMeshPro = GameObject.Find("texto").GetComponent<TextMeshPro>(); // Encontramos o TextMeshPro diretamente e não mais o GameObject
        StartCoroutine(rotine());
    }

    int i = 0;

    public IEnumerator rotine()
    {
        while (i < 3)
        {
            textMeshPro.text = textos[i]; // Definimos o texto diretamente no TextMeshPro

            // Habilita imagem
            GameObject img = GameObject.Find("img" + i);
            img.GetComponent<MeshRenderer>().enabled = true; // Ajustamos a renderização da imagem
            
            // Espera 5 segundos antes de continuar
            yield return new WaitForSeconds(5);

            i++;
        }

        // Após exibir todos os textos, carrega a próxima cena
        SceneManager.LoadScene("NomeDaProximaCena"); // Substitua "NomeDaProximaCena" pelo nome da próxima cena que deseja carregar
    }
}
