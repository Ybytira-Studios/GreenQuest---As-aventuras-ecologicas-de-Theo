using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLevelFase_1 : MonoBehaviour
{
    public Timer timer;
    public GameObject finishLevelPanel;
    public FinalDialogueRiver finalDialogueRiver;
    //public GameObject startPanel2;
    public StarController starController;
    public Button retryButton;
    public Button nextButton;
    public TextMeshProUGUI timerFinish;
    public Image starFinish;
    public string cenaAtual;
    public StartLevelRiver startLevelRiver; // Referência ao script de diálogo

    void Start()
    {
        retryButton.onClick.AddListener(Retry);
        nextButton.onClick.AddListener(Next);
        cenaAtual = SceneManager.GetActiveScene().name;
        //startPanel2.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        print("clicou");
    }

    public void Next()
    {
        
        if (cenaAtual == "Fase1_beach")
        {
            print("pintio de negao");
            //SceneManager.LoadScene("Fase1_beach");
        }
        else if (cenaAtual == "Fase4_river")    
        {
            finishLevelPanel.SetActive(false);
            finalDialogueRiver.enabled = true;
            //startPanel2.SetActive(true);
            print("clicou next fase 4 river");
            
        }
        
    }
}
