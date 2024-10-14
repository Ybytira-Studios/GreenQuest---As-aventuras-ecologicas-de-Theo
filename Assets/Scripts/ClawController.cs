using UnityEngine;

public class ClawController : MonoBehaviour
{
    public PlayerControllerRiver playerControllerRiver;
    public GrabObject grabObject;
    public GameObject pressQ;
    public AudioSource audioSourceClaw;
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    public string penis = "pt";
    public float speed = 4.0f;
    private bool movingDown = false;
    private bool isMoving = false;

    void Start()
    {
        playerControllerRiver.disparaGarraAudioSource.clip = playerControllerRiver.soundEffects[2];
        pressQ.SetActive(false);
    }

    void Update()
{
    // Verifica se o jogador está segurando a garra
    GameObject carriedObject = grabObject.GetCarriedObject();
    if (carriedObject != null && carriedObject.CompareTag("claw"))
    {
        pressQ.SetActive(true); // Mostra a indicação para pressionar Q
    }
    else
    {
        pressQ.SetActive(false); // Esconde a indicação se não estiver segurando a garra
    }

    if (Input.GetKeyDown(KeyCode.Q) && !isMoving && carriedObject != null && carriedObject.CompareTag("claw"))
    {
        playerControllerRiver.disparaGarraAudioSource.Play();
        initialPosition = transform.position;
        targetPosition = new Vector3(initialPosition.x, initialPosition.y - 3f, initialPosition.z);
        grabObject.DropObject(); // Dropa o objeto

        // Transição para o estado Idle do jogador
        playerControllerRiver.playerAnimator.SetFloat("Speed", 0f); // Força o Idle
        playerControllerRiver.canMove = false; // Impede o movimento do jogador
        
        playerControllerRiver.playerAnimator.enabled = true;
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
                playerControllerRiver.canMove = true;
                playerControllerRiver.playerAnimator.enabled = true; // Permite que o jogador volte a se mover
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            audioSourceClaw.Play();
            Destroy(other.gameObject); // Destrói o objeto coletável
        }
    }
}
