using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Adicionado

public class DialogueControlIntroTheoEscola : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;
    public Button nextButton; // Adicionando a referência ao botão

    [Header("Settings")]
    public float typingSpeed;
    private string[] setences;
    private int index;
    
    // Lista de diálogos
    public List<Dialogue> dialogues;
    private int dialogueIndex;

    // Nome da próxima cena
    public string nextSceneName;

    private void Start()
    {
        // Adiciona o listener para o botão
        nextButton.onClick.AddListener(NextSetence);
        dialogueIndex = 0;
        StartNextDialogue();
    }

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueObj.SetActive(true);
        AdjustImageSize(profile, p); // Ajusta o tamanho da imagem sem distorção
        setences = txt;
        actorNameText.text = actorName;
        index = 0; // Reseta o índice para começar do início
        speechText.text = ""; // Reseta o texto atual
        StartCoroutine(TypeSetence());
    }

    IEnumerator TypeSetence()
    {
        foreach (char letter in setences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSetence()
    {
        if (speechText.text == setences[index])
        {
            //ainda há textos
            if (index < setences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSetence());
            }
            else
            { //acabou os textos
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                StartNextDialogue();
            }
        }
    }

    private void StartNextDialogue()
    {
        if (dialogueIndex < dialogues.Count)
        {
            Dialogue dialogue = dialogues[dialogueIndex];
            Speech(dialogue.profile, dialogue.setences, dialogue.actorName);
            dialogueIndex++;
        }
        else
        {
            // Todos os diálogos foram exibidos
            Debug.Log("Todos os diálogos foram exibidos.");
            LoadNextScene(); // Chama a função para carregar a próxima cena
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Intro-Theo-Quarto");
    }

    private void AdjustImageSize(Image image, Sprite sprite)
    {
        if (sprite == null) return;

        image.sprite = sprite;
        image.preserveAspect = true; // Mantém a proporção da imagem
    }
}

[System.Serializable]
public class Dialogue
{
    public Sprite profile;
    public string actorName;
    public string[] setences;
}
