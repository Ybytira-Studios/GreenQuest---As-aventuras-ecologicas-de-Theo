using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainInterface : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    void Awake()
    {
        mainMenu.SetActive(true);
        // Adiciona eventos aos botões
        playButton.onClick.AddListener(Play);
        optionsButton.onClick.AddListener(Options);
        quitButton.onClick.AddListener(Quit);
    }

    public void Play()
    {
        SceneManager.LoadScene("Intro-City", LoadSceneMode.Single);
    }

    public void Options()
    {
        optionMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
     void Start(){
    mainMenu.SetActive(true);
    }

    void OnEnable()
    {
        // Garante que o menu esteja ativo ao ser habilitado
        mainMenu.SetActive(true);
    }
}
