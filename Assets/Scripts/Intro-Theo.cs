using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Cutscene2 : MonoBehaviour
{
    string[] textos = new string[4];
    public TextMeshProUGUI texto;
    public GameObject[] imagens; // Array de GameObjects para imagens

    int cont = 0; // Contador para o progresso da cutscene

    void Start()
    {
        Language languageScript = FindObjectOfType<Language>();
        // Instancia a classe de linguagem
        Language lg = new Language();

        // Define os textos com base no idioma
        switch (languageScript.getLanguage())
        {
            case "pt":
                textos[0] = "Theodoro Correia Junior, apelidado de Theo, é uma criança alegre e agitada. Ele não gosta muito da escola, mas é um bom aluno. Tem 12 anos e sempre viveu na Baía da Aventura.";
                textos[1] = "A mãe de Theo, Daniela Regina, é uma mulher ativista, empoderada e muito simpática. Aos 35 anos, acredita no bem da humanidade e na organização coletiva.";
                textos[2] = " Por isso, ela fundou a LAL (Liga Anti-Lixo), acreditando que, com a organização da cidade, é possível que ela volte a ser como antes.";
                textos[3] = "Nestas férias, Theo não terá descanso. Ele vai ajudar a Liga, pois fica muito triste ao ver como a cidade ficou. Mal sabe ele o que o aguarda nessas férias nada convencionais…";
                break;

            case "en":
                textos[0] = "Theodoro Correia Junior, nicknamed Theo, is a cheerful and energetic child. He doesn't like school much, but he's a good student. He's 12 years old and has always lived in Baía da Aventura.";
                textos[1] = "Theo's mother, Daniela Regina, is an empowered and friendly activist. At 35, she believes in the goodness of humanity and collective organization.";
                textos[2] = "That's why she founded the LAL (Anti-Litter League), believing that with the city's organization, it can return to what it once was.";
                textos[3] = "This vacation, Theo will have no rest. He's going to help the League, as it makes him sad to see how the city has become. Little does he know what awaits him in these unconventional holidays...";
                break;

            case "es":
                textos[0] = "Theodoro Correia Junior, apodado Theo, es un niño alegre y enérgico. No le gusta mucho la escuela, pero es un buen estudiante. Tiene 12 años y siempre ha vivido en Baía da Aventura.";
                textos[1] = "La madre de Theo, Daniela Regina, es una activista empoderada y muy simpática. A los 35 años, cree en la bondad de la humanidad y en la organización colectiva.";
                textos[2] = "Por eso, fundó la LAL (Liga Anti Basura), creyendo que, con la organización de la ciudad, es posible que vuelva a ser como antes.";
                textos[3] = "En estas vacaciones, Theo no tendrá descanso. Va a ayudar a la Liga, ya que se pone triste al ver cómo ha quedado la ciudad. Y no tiene ni idea de lo que le espera en estas vacaciones nada convencionales...";
                break;

            case "fr":
                textos[0] = "Theodoro Correia Junior, surnommé Theo, est un enfant joyeux et énergique. Il n'aime pas beaucoup l'école, mais c'est un bon élève. Il a 12 ans et a toujours vécu à Baía da Aventura.";
                textos[1] = "La mère de Theo, Daniela Regina, est une militante sympathique et pleine d'assurance. À 35 ans, elle croit en la bonté de l'humanité et à l'organisation collective.";
                textos[2] = " C'est pourquoi elle a fondé la LAL (Ligue Anti-Déchets), croyant qu'avec l'organisation de la ville, elle pourrait redevenir comme avant.";
                textos[3] = "Pendant ces vacances, Theo n'aura pas de repos. Il va aider la Ligue, car cela le rend triste de voir l'état de la ville. Il ne sait pas encore ce qui l'attend pendant ces vacances pas comme les autres...";
                break;

            default:
                 textos[0] = "Theodoro Correia Junior, apelidado de Theo, é uma criança alegre e agitada. Ele não gosta muito da escola, mas é um bom aluno. Tem 12 anos e sempre viveu na Baía da Aventura.";
                textos[1] = "A mãe de Theo, Daniela Regina, é uma mulher ativista, empoderada e muito simpática. Aos 35 anos, acredita no bem da humanidade e na organização coletiva.";
                textos[2] = " Por isso, ela fundou a LAL (Liga Anti-Lixo), acreditando que, com a organização da cidade, é possível que ela volte a ser como antes.";
                textos[3] = "Nestas férias, Theo não terá descanso. Ele vai ajudar a Liga, pois fica muito triste ao ver como a cidade ficou. Mal sabe ele o que o aguarda nessas férias nada convencionais…";
                break;
        }

        // Exibe o primeiro texto
        texto.text = textos[0];

        // Inicia a cutscene
        StartCoroutine(RotinaCutscene());
    }

    public IEnumerator RotinaCutscene()
    {
        while (cont < textos.Length)
        {
            // Atualiza o texto e as imagens com base no contador
            texto.text = textos[cont];
            AtualizaImagens(cont);

            Debug.Log("Texto exibido: " + texto.text);

            // Espera por 10 segundos antes de continuar
            yield return new WaitForSeconds(13);

            // Incrementa o contador para a próxima parte da cutscene
            cont++;
        }

        // Quando todos os textos forem exibidos, carrega a próxima cena
        SceneManager.LoadScene("Intro-Fase1");
    }

    void AtualizaImagens(int index)
    {
        // Desativa todas as imagens primeiro
        foreach (var imagem in imagens)
        {
            if (imagem != null)
            {
                imagem.GetComponent<Image>().enabled = false;
            }
        }

        // Ativa a imagem correspondente ao índice atual
        if (imagens[index] != null)
        {
            imagens[index].GetComponent<Image>().enabled = true;
        }
        else
        {
            Debug.LogWarning("Tentou acessar uma imagem nula no índice " + index);
        }
    }
}
