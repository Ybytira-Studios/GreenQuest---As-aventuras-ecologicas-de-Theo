using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    public Slider volumeSlider; // O Slider que controlará o volume
    public Button volumeButton; // O botão para alternar o volume
    public Sprite volumeOnSprite; // O sprite quando o volume está ligado
    public Sprite volumeOffSprite; // O sprite quando o volume está desligado
    private float previousVolume = 1f; // Armazena o volume anterior
    private bool isMuted = false; // Estado de mudo
    public GameObject menuOptions;

    void Start()
    {
        menuOptions.SetActive(false);

        // Verificar se o volume já está salvo; se não, configurar para 70% (0.7)
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.7f); // Definir volume inicial em 70%
        }
        volumeSlider.value = 0.7f;

        // Carregar volume salvo ou inicial
        Load();

        volumeButton.onClick.AddListener(ToggleVolume);
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
        // Carregar volume salvo nos PlayerPrefs
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        AudioListener.volume = volumeSlider.value;
    }

    private void Save()
    {
        // Salvar o valor do volume atual
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
            // Restaurar o volume anterior
            AudioListener.volume = previousVolume;
            volumeSlider.value = previousVolume;
            volumeButton.image.sprite = volumeOnSprite;
            isMuted = false;
        }
        else
        {
            // Armazenar o volume atual e zerar o volume
            previousVolume = volumeSlider.value;
            AudioListener.volume = 0;
            volumeSlider.value = 0;
            volumeButton.image.sprite = volumeOffSprite;
            isMuted = true;
        }
        Save();
    }
}
