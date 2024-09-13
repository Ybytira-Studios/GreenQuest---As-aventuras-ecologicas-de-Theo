using UnityEngine;

public class MoveObjectOnKeyPress : MonoBehaviour
{
    public PlayerControllerRiver playerController;
    public GrabObject grabObject;
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    public float speed = 2.0f;
    private bool movingDown = false;
    private bool isMoving = false;

    void Update()
    {
        // Verifica se o jogador está segurando a garra
        GameObject carriedObject = grabObject.GetCarriedObject();
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving && carriedObject != null && carriedObject.tag == "claw")
        {
            initialPosition = transform.position;
            targetPosition = new Vector3(initialPosition.x, initialPosition.y - 2f, initialPosition.z);

            grabObject.DropObject(); // Dropa o objeto
            playerController.canMove = false; // Impede o movimento do jogador
            isMoving = true; // Ativa o movimento da garra
        }

        if (isMoving)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if (!movingDown)
        {
            // Move a garra para baixo
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                movingDown = true; // Inicia o movimento de volta para cima
            }
        }
        else
        {
            // Move a garra para cima
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
            {
                movingDown = false;
                isMoving = false; // Movimento terminou
                grabObject.isGrabbing = false;
                grabObject.InteractWithObject(); // Recolhe o objeto automaticamente
                playerController.canMove = true; // Permite que o jogador volte a se mover
            }
        }
    }
    
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            Destroy(other.gameObject); // Destrói o objeto coletável
            print("Destroi o lixo.");
        }
    }

}
