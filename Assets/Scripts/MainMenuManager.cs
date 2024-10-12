using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager instance;

    void Awake()
    {
        gameObject.SetActive(true);
        // Verifica se já existe uma instância deste objeto
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Não destrói este objeto ao mudar de cena
            // Certifique-se de que o menu esteja ativo
            gameObject.SetActive(true);
        }
        else
        {
            Destroy(gameObject); // Destrói o novo objeto se já existir uma instância
        }
    }

    void Start(){
    gameObject.SetActive(true);
    }

    void OnEnable()
    {
        // Garante que o menu esteja ativo ao ser habilitado
        gameObject.SetActive(true);
    }
}
