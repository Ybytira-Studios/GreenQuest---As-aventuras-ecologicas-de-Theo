using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueQControlCozinhaTheo : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueQObj;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;
    public Button nextButton;

    [Header("Settings")]
    public float typingSpeed = 0.05f;
    private string[] sentences;
    private int index;

    // Nome da próxima cena
    public string nextSceneName;

    // Referências às imagens dos personagens
    public Sprite theoImage;
    public Sprite maeImage;

    private int dialogueQIndex = 0;

    private void Start()
    {
        nextButton.onClick.AddListener(NextSentence);
        dialogueQObj.SetActive(false); // Garanta que o diálogo comece escondido
        StartNextDialogueQ();
    }

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueQObj.SetActive(true);
        AdjustImageSize(profile, p);
        sentences = txt;
        actorNameText.text = actorName;
        index = 0;
        speechText.text = "";
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        // Verifica se a frase atual foi completamente exibida
        if (speechText.text == sentences[index])
        {
            // Se ainda houver textos para mostrar
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                // Acabaram os textos
                speechText.text = "";
                index = 0;

                if (dialogueQIndex > 3) // Se todos os diálogos forem concluídos
                {
                    SceneManager.LoadScene("Fase4_river");
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
        Language lg = new();
        string language = lg.getLanguage();

        switch (language)
        {
            case "pt":
                switch (dialogueQIndex)
                {
                    case 0:
                        Speech(maeImage, new string[] {
                            "Se a gente não ajudar filho, o bairro vai continuar todo sujo. A água do rio tá cheia de lixo e você sabe que ter que ir comigo, você não ficar sozinho em casa."
                        }, "Mãe de Theo");
                        break;
                    case 1:
                        Speech(theoImage, new string[] {
                            "Eu não gosto de lixo... é tudo tão feio agora."
                        }, "Theo");
                        break;
                    case 2:
                        Speech(theoImage, new string[] {
                            "(mudando de ideia)", 
                            "Tá bom, mãe. Mas eu posso ajudar? Não quero que o São Jerônimo Sujeirinha fique assim pra sempre."
                        }, "Theo");
                        break;
                    case 3:
                        Speech(maeImage, new string[] {
                            "Orgulho da mamãe!!"
                        }, "Mãe de Theo");
                        break;
                }
                break;

            case "en":
                switch (dialogueQIndex)
                {
                    case 0:
                        Speech(maeImage, new string[] {
                            "If we don't help, son, the neighborhood will stay dirty. The river water is full of trash, and you know you have to come with me; you can't stay home alone."
                        }, "Mom");
                        break;
                    case 1:
                        Speech(theoImage, new string[] {
                            "I don't like trash... everything looks so ugly now."
                        }, "Theo");
                        break;
                    case 2:
                        Speech(theoImage, new string[] {
                            "(changing his mind)", 
                            "Okay, mom. Can I help? I don't want São Jerônimo Sujeirinha to stay like this forever."
                        }, "Theo");
                        break;
                    case 3:
                        Speech(maeImage, new string[] {
                            "Mom's pride!!"
                        }, "Mom");
                        break;
                }
                break;

            case "es":
                switch (dialogueQIndex)
                {
                    case 0:
                        Speech(maeImage, new string[] {
                            "Si no ayudamos, hijo, el barrio seguirá todo sucio.", 
                            "El agua del río está llena de basura y sabes que tienes que venir conmigo. No te puedes quedar solo en casa."
                        }, "Mamá de Theo");
                        break;
                    case 1:
                        Speech(theoImage, new string[] {
                            "No me gusta la basura... ahora todo está tan feo."
                        }, "Theo");
                        break;
                    case 2:
                        Speech(theoImage, new string[] {
                            "(cambiando de opinión)", 
                            "Está bien, mamá. ¿Puedo ayudar? No quiero que São Jerônimo Sujeirinha siga así para siempre."
                        }, "Theo");
                        break;
                    case 3:
                        Speech(maeImage, new string[] {
                            "Orgullo de mamá!"
                        }, "Mamá de Theo");
                        break;
                }
                break;

            case "fr":
                switch (dialogueQIndex)
                {
                    case 0:
                        Speech(maeImage, new string[] {
                            "Si nous n’aidons pas, mon fils, le quartier restera tout sale.", 
                            "L’eau de la rivière est pleine de déchets et tu sais que tu dois venir avec moi. Tu ne peux pas rester seul à la maison."
                        }, "Maman de Theo");
                        break;
                    case 1:
                        Speech(theoImage, new string[] {
                            "Je n’aime pas les déchets... tout est si moche maintenant."
                        }, "Theo");
                        break;
                    case 2:
                        Speech(theoImage, new string[] {
                            "(changeant d'avis)", 
                            "D'accord, maman. Mais je peux aider ? Je ne veux pas que São Jerônimo Sujeirinha reste comme ça pour toujours."
                        }, "Theo");
                        break;
                    case 3:
                        Speech(maeImage, new string[] {
                            "Fierté de maman!"
                        }, "Maman de Theo");
                        break;
                }
                break;
        }

        dialogueQIndex++;
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene("Intro-Fase1");
    }

    private void AdjustImageSize(Image image, Sprite sprite)
    {
        if (sprite == null) return;

        image.sprite = sprite;
        image.preserveAspect = true;
    }
}
