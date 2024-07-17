using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuOptions : MonoBehaviour
{
    public Slider volumeSlider; // O Slider que controlará o volume
    public Button volumeButton; // O botão para alternar o volume
    public Sprite volumeOnSprite; // O sprite quando o volume está ligado
    public Sprite volumeOffSprite; // O sprite quando o volume está desligado
    private float previousVolume = 1f; // Armazena o volume anterior
    private bool isMuted = false; // Estado de mudo

    void Start()
    {
        gameObject.SetActive(false);
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
            Load();
        }
        else
        {
            Load();
        }

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
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        AudioListener.volume = volumeSlider.value;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void ExitOption()
    {
        gameObject.SetActive(false);
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
