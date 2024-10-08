using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainInterface : MonoBehaviour
{
    public GameObject OptionMenu;
    public GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(){
        MainMenu.SetActive(false);
        SceneManager.LoadScene("Intro-City", LoadSceneMode.Single);
    }

    public void Options(){
        OptionMenu.SetActive(true);
    }

    public void Quit(){
        Application.Quit();
    }

}
