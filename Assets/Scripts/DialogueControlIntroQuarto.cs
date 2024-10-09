using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System; // Adicionado

public class DialogueQControlIntroTheoQuarto : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueQObj;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;
    public Button nextButton; // Adicionando a referência ao botão

    [Header("Settings")]
    public float typingSpeed = 0.05f; // Ajuste da velocidade da escrita
    private string[] sentences;
    private int index;

    // Nome da próxima cena
    public string nextSceneName;


    // Referências às imagens dos personagens
    public Sprite theoImage;
    public Sprite maeImage;

    private void Start()
    {
        // Adiciona o listener para o botão
        nextButton.onClick.AddListener(NextSentence);
        dialogueQIndex = 0;
        StartNextDialogueQ();
    }

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueQObj.SetActive(true);
        AdjustImageSize(profile, p); // Ajusta o tamanho da imagem sem distorção
        sentences = txt;
        actorNameText.text = actorName;
        index = 0; // Reseta o índice para começar do início
        speechText.text = ""; // Reseta o texto atual
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Aumente ou diminua typingSpeed
        }
    }

    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            // ainda há textos
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                // acabou os textos
                speechText.text = "";
                index = 0;

                if (dialogueQIndex >= 3) // Se todos os diálogos forem concluídos
                {
                    LoadNextScene(); // Chama a função para carregar a próxima cena
                }
                else
                {
                    StartNextDialogueQ(); // Começa o próximo conjunto de diálogos
                }
            }
        }
    }


    private void StartNextDialogueQ()
    {
        // Insere os diálogos diretamente no código, com switch-case para os idiomas
        Language languageScript = FindObjectOfType<Language>();
        Language lg = new();
        string language = lg.getLanguage();
        switch (languageScript.getLanguage())
        {
            case "pt":
                if (dialogueQIndex == 0)
                {
                    string[] dialoguesMae = { 
                        "Theo, acorda, filho. Hoje é dia de ajudar no São Jerônimo."
                    };
                    Speech(maeImage, dialoguesMae, "Mãe de Theo");
                }
                else if (dialogueQIndex == 1)
                {
                    string[] dialoguesTheo = { 
                        "(se virando na cama)", 
                        "No São Jerônimo Sujeirinha? Ah, mãe… vô não… tá tudo tão sujo lá... É nojento!" 
                    };
                    Speech(theoImage, dialoguesTheo, "Theo");
                }
                else if (dialogueQIndex == 2)
                {
                    string[] dialoguesMae = { 
                        "Eu sei que tá sujo, mas o São Jerônimo já foi um lugar tão bonito, lembra?", 
                        "Você adorava lá quando menor, agora tá cheio de lixo e precisamos limpar. Vamos tomar café para melhorar o ânimo." 
                    };
                    Speech(maeImage, dialoguesMae, "Mãe de Theo");
                }
                break;

            case "en":
                if (dialogueQIndex == 0)
                {
                    string[] dialoguesMae = { 
                        "Theo, wake up, son. Today is the day to help in São Jerônimo."
                    };
                    Speech(maeImage, dialoguesMae, "Mom");
                }
                else if (dialogueQIndex == 1)
                {
                    string[] dialoguesTheo = { 
                        "(turning in bed)", 
                        "In São Jerônimo Sujeirinha? Oh, mom… I don't wanna go… it's so dirty there... It's gross!" 
                    };
                    Speech(theoImage, dialoguesTheo, "Theo");
                }
                else if (dialogueQIndex == 2)
                {
                    string[] dialoguesMae = { 
                        "I know it's dirty, but São Jerônimo used to be such a beautiful place, remember?", 
                        "You loved it when you were younger, now it's full of trash and we need to clean it up. Let's have breakfast to get in the mood." 
                    };
                    Speech(maeImage, dialoguesMae, "Mom");
                }
                break;

            case "es": 
                if (dialogueQIndex == 0)
                {
                    string[] dialoguesMae = { 
                        "Theo, despierta, hijo. Hoy es día de ayudar en São Jerônimo."
                    };
                    Speech(maeImage, dialoguesMae, "Mamá de Theo");
                }
                else if (dialogueQIndex == 1)
                {
                    string[] dialoguesTheo = { 
                        "(dándose vuelta en la cama)", 
                        "¿En São Jerônimo Sujeirinha? Ah, mamá... no quiero ir... está todo tan sucio allá... ¡Qué asco!" 
                    };
                    Speech(theoImage, dialoguesTheo, "Theo");
                }
                else if (dialogueQIndex == 2)
                {
                    string[] dialoguesMae = { 
                        "Sé que está sucio, pero São Jerônimo solía ser un lugar tan bonito, ¿recuerdas?", 
                        "Te encantaba cuando eras pequeño, ahora está lleno de basura y necesitamos limpiarlo. Vamos a desayunar para animarnos." 
                    };
                    Speech(maeImage, dialoguesMae, "Mamá de Theo");
                }
                break;

            case "fr": 
                if (dialogueQIndex == 0)
                {
                    string[] dialoguesMae = { 
                        "Theo, réveille-toi, mon fils. Aujourd'hui, c'est le jour d'aider à São Jerônimo."
                    };
                    Speech(maeImage, dialoguesMae, "Maman de Theo");
                }
                else if (dialogueQIndex == 1)
                {
                    string[] dialoguesTheo = { 
                        "(se tournant dans le lit)", 
                        "À São Jerônimo Sujeirinha? Ah, maman... je ne veux pas y aller... c'est tellement sale là-bas... C'est dégoûtant!" 
                    };
                    Speech(theoImage, dialoguesTheo, "Theo");
                }
                else if (dialogueQIndex == 2)
                {
                    string[] dialoguesMae = { 
                        "Je sais que c'est sale, mais São Jerônimo était un endroit si beau autrefois, tu te souviens?", 
                        "Tu l'adorais quand tu étais plus jeune, maintenant c'est plein de déchets et nous devons le nettoyer. Allons prendre le petit-déjeuner pour se mettre de bonne humeur." 
                    };
                    Speech(maeImage, dialoguesMae, "Maman de Theo");
                }
                break;
        }

        dialogueQIndex++;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Intro-Theo-Cozinha");
    }

    private void AdjustImageSize(Image image, Sprite sprite)
    {
        if (sprite == null) return;

        image.sprite = sprite;
        image.preserveAspect = true; // Mantém a proporção da imagem
    }

    private int dialogueQIndex = 0;
}
