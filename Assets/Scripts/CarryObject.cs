using UnityEngine;

public class CarryObject : MonoBehaviour
{
    public GrabObject grabObject; // Referência ao script de grab
    public Vector2 carryOffset = new Vector2(1f, 0f); // Offset para posicionar o objeto coletado
    private GameObject carriedObject; // Objeto sendo segurado
    private bool isShooting = false; // Controla se a garra está sendo disparada

    void Update()
    {
        if (grabObject.isGrabbing && !isShooting)
        {
            if (carriedObject == null)
            {
                carriedObject = grabObject.GetCarriedObject(); // Obter o objeto que está sendo carregado
            }
            UpdateCarriedObjectPosition();
        }
        else
        {
            carriedObject = null; // Para de segurar o objeto quando não está mais grabando ou está disparando a garra
        }
    }

    // Método chamado pelo script de movimentação da garra quando ela está sendo disparada
    public void ShootGarra()
    {
        isShooting = true; // Garra foi disparada
    }

    // Método chamado quando o movimento da garra termina
    public void ResetGarra()
    {
        isShooting = false; // Garra volta para o estado normal de ser carregada
    }

    void UpdateCarriedObjectPosition()
    {
        if (carriedObject != null && !isShooting)
        {
            carriedObject.transform.position = (Vector2)transform.position + carryOffset;
        }
    }
}
