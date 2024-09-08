using System.Collections;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    public float stretchSpeed = 10f; // Velocidade da garra ao esticar
    public float maxDistance = 5f;   // Distância máxima que a garra pode se esticar

    private Vector3 initialPosition; // Posição inicial da garra no mundo
    private bool isStretching = false;

    void Start()
    {
        initialPosition = transform.position; // Posição inicial no mundo
        
    }

    public void LaunchGarra()
    {
        if (!isStretching)
        {
            StartCoroutine(StretchAndReturn());
        }
    }

    private IEnumerator StretchAndReturn()
    {
        isStretching = true;
        Vector3 targetPosition = initialPosition - new Vector3(0, maxDistance, 0); // Movimento apenas para baixo

        // Estica a garra para baixo até a distância máxima
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, stretchSpeed * Time.deltaTime);
            yield return null;
        }

        // Espera um curto período de tempo antes de retornar
        yield return new WaitForSeconds(0.5f);

        // Retorna a garra ao jogador
        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, stretchSpeed * Time.deltaTime);
            yield return null;
        }

        isStretching = false;
    }

    // Função para detectar objetos de lixo no rio
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash") && isStretching)
        {
            Destroy(other.gameObject); // Destroi o lixo ao tocar
        }
    }
}
