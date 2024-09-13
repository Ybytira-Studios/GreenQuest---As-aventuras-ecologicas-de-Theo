using UnityEngine;
using UnityEngine.UI;

public class ApertaMuito : MonoBehaviour
{
    public float variableValue = 0.2f;  // Valor inicial para uma opacidade baixa
    public float increaseAmount = 0.1f; // Quantidade que aumenta a variável
    public float decreaseRate = 0.05f; // Taxa de diminuição da variável
    public float limit = 1f; // Limite para a variável

    public float timeBetweenPresses = 0.5f; // Tempo necessário entre toques da tecla
    private float timeSinceLastPress = 0f; // Tempo desde o último toque

    public Image image; // Referência ao componente Image
    public Slider slider; // Referência ao componente Slider
    private Color imageColor; // Cor da imagem

    public SpriteRenderer targetSpriteRenderer; // Referência ao SpriteRenderer do objeto cujo sprite será trocado
    public Sprite newSprite; // Novo sprite a ser atribuído quando o valor atingir 1

    public GameObject panel; // Referência ao painel
    private bool isPanelActive; // Estado do painel

    void Start()
    {
        if (image != null)
        {
            imageColor = image.color; // Obtém a cor atual da imagem
            imageColor.a = variableValue; // Define a opacidade inicial com base no valor da variável
            image.color = imageColor; // Aplica a cor inicial com a opacidade definida
        }

        if (slider != null)
        {
            slider.minValue = 0f; // Define o valor mínimo do slider
            slider.maxValue = 1f; // Define o valor máximo do slider
            slider.value = variableValue; // Define o valor inicial do slider
            slider.onValueChanged.AddListener(OnSliderValueChanged); // Adiciona um ouvinte para mudanças no slider
        }

        // Inicializa o estado do painel
        if (panel != null)
        {
            panel.SetActive(false); // Certifica-se de que o painel está desativado no início
        }
    }

    void Update()
    {
        timeSinceLastPress += Time.deltaTime;

        if (variableValue < limit)
        {
            if (Input.GetKeyDown(KeyCode.Space) && timeSinceLastPress >= timeBetweenPresses)
            {
                variableValue += increaseAmount;
                timeSinceLastPress = 0f; // Reseta o tempo desde o último toque
                Debug.Log("Increased: " + variableValue); // Log para depuração
            }

            variableValue -= decreaseRate * Time.deltaTime;

            if (variableValue < 0f)
            {
                variableValue = 0f;
            }
        }

        if (variableValue > limit)
        {
            variableValue = limit;
        }

        // Atualiza a opacidade da imagem com base no valor da variável
        if (image != null)
        {
            float alpha = Mathf.Clamp(variableValue, 0f, 1f); // Calcula o valor de alpha
            imageColor.a = alpha; // Define a opacidade
            image.color = imageColor; // Aplica a nova cor com a opacidade alterada
        }

        // Atualiza o valor do slider com base na variável
        if (slider != null)
        {
            slider.value = variableValue; // Define o valor do slider
        }

        // Se a variável atingir o limite, feche o painel e troque o sprite
        if (variableValue >= limit)
        {
            if (panel != null)
            {
                panel.SetActive(false); // Fecha o painel
            }

            if (targetSpriteRenderer != null && newSprite != null)
            {
                targetSpriteRenderer.sprite = newSprite; // Troca o sprite
            }
        }
    }

    private void OnSliderValueChanged(float newValue)
    {
        variableValue = newValue; // Atualiza a variável com o novo valor do slider
        if (image != null)
        {
            imageColor.a = variableValue; // Define a opacidade da imagem com o novo valor
            image.color = imageColor; // Aplica a nova cor com a opacidade alterada
        }
    }
}
