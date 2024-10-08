using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Adicionado

public class DialogueCControlIntroTheoCozinha : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueCObj;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;
    public Button nextButton; // Adicionando a referência ao botão

    [Header("Settings")]
    public float typingSpeed;
    private string[] setences;
    private int index;
    
    // Lista de diálogos
    public List<DialogueC> dialogueCs;
    private int dialogueCIndex;

    // Nome da próxima cena
    public string nextSceneName;

    private void Start()
    {
        // Adiciona o listener para o botão
        nextButton.onClick.AddListener(NextSetence);
        dialogueCIndex = 0;
        StartNextDialogueC();
    }

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueCObj.SetActive(true);
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
                dialogueCObj.SetActive(false);
                StartNextDialogueC();
            }
        }
    }

    private void StartNextDialogueC()
    {
        if (dialogueCIndex < dialogueCs.Count)
        {
            DialogueC dialogueC = dialogueCs[dialogueCIndex];
            Speech(dialogueC.profile, dialogueC.setences, dialogueC.actorName);
            dialogueCIndex++;
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
        SceneManager.LoadScene("Intro-Fase1");
    }

    private void AdjustImageSize(Image image, Sprite sprite)
    {
        if (sprite == null) return;

        image.sprite = sprite;
        image.preserveAspect = true; // Mantém a proporção da imagem
    }
}

[System.Serializable]
public class DialogueC
{
    public Sprite profile;
    public string actorName;
    public string[] setences;
}
