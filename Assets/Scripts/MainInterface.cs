using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainInterface : MonoBehaviour
{
    public GameObject OptionMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(){
        SceneManager.LoadScene("Intro-City");
    }

    public void Options(){
        OptionMenu.SetActive(true);
    }

    public void Quit(){
        Application.Quit();
    }

}
