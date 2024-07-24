using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLevelFase_1 : MonoBehaviour
{
    public Timer timer;
    public StarController starController;
    public Button retryButton;
    public Button nextButton;
    public TextMeshProUGUI timerFinish;
    public Image starFinish;

    void Start()
    {
        retryButton.onClick.AddListener(Retry);
        nextButton.onClick.AddListener(Next);
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
        SceneManager.LoadScene("Fase2_water");
        print("clicou");
    }
}
