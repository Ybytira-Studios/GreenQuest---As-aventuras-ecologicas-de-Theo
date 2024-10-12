using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; 
    [SerializeField] private Button volumeButton;
    [SerializeField] private Button buttonExit;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private Sprite volumeOnSprite;
    [SerializeField] private Sprite volumeOffSprite;
    
    private float previousVolume = 1f;
    private bool isMuted = false;

    void Awake()
    {
        menuOptions.SetActive(false);

        // Verifica se o volume já está salvo; se não, configura para 70% (0.7)
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.7f);
        }
        volumeSlider.value = 0.7f;

        // Carregar volume salvo ou inicial
        Load();

        volumeButton.onClick.AddListener(ToggleVolume);
        buttonExit.onClick.AddListener(ExitOption);
    }

    public void ChangeVolume()
    {
        if (!isMuted)
        {
            AudioListener.volume = volumeSlider.value;
            Save();
        }
    }

    private void Load()
    {
        // Carrega o volume salvo
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        AudioListener.volume = volumeSlider.value;
    }

    private void Save()
    {
        // Salva o valor do volume atual
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void ExitOption()
    {
        menuOptions.SetActive(false);
    }

    private void ToggleVolume()
    {
        if (isMuted)
        {
            AudioListener.volume = previousVolume;
            volumeSlider.value = previousVolume;
            volumeButton.image.sprite = volumeOnSprite;
            isMuted = false;
        }
        else
        {
            previousVolume = volumeSlider.value;
            AudioListener.volume = 0;
            volumeSlider.value = 0;
            volumeButton.image.sprite = volumeOffSprite;
            isMuted = true;
        }
        Save();
    }
}
