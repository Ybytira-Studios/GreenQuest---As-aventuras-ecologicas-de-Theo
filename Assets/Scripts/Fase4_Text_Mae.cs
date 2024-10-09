using UnityEngine;
using TMPro;

public class Fase4_TextMae : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    
    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        Language lg = new();
        string language = lg.getLanguage();
        switch (language)
        {
            case "pt":
                textMeshPro.text = "Nós podemos ajudar a população local retirando os dejetos do rio. Você não poderá entrar dentro do córrego como os profissionais, filho, pois é perigoso! Ao invés disso, use a garra limpa-limpa para pegar os lixos estando na margem!";
                break;
            case "en":
                textMeshPro.text = "We can help the local population by removing the waste from the river. You cannot enter the stream like the professionals, son, because it is dangerous! Instead, use the clean-clean claw to pick up the trash while staying on the bank!";
                break;
            case "es":
                textMeshPro.text = "Podemos ayudar a la población local retirando los desechos del río. No podrás entrar en el arroyo como los profesionales, hijo, ¡porque es peligroso! En su lugar, usa la garra limpia-limpia para recoger la basura desde la orilla!";
                break;
            case "fr":
                textMeshPro.text = "Nous pouvons aider la population locale en enlevant les déchets de la rivière. Tu ne peux pas entrer dans le ruisseau comme les professionnels, fils, car c'est dangereux ! Au lieu de cela, utilise la griffe nettoie-nettoie pour ramasser les déchets tout en restant sur la berge !";
                break;
            default:
                textMeshPro.text = "We can help the local population by removing the waste from the river. You cannot enter the stream like the professionals, son, because it is dangerous! Instead, use the clean-clean claw to pick up the trash while staying on the bank!";
                break;
        }
    }
}
