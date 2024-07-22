using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    public SpriteRenderer spriteRenderer;
    public AudioSource footStepAudioSource;
    public AudioSource grabAudioSource;
    public AudioClip[] soundEffects;
    public ParticleSystem playerParticleSystem;
    private Animator playerAnimator;
    private Vector2 playerDirection;

    private Vector2 lastPlayerDirection;
    public float playerSpeed;
    public float buoyancyForce = 5f; // Força de empuxo a ser aplicada
    public float KBForce;
    public float KBCount;
    public float KBTime;
    public bool isKnockRight;
    private bool isInWaterScene;
    public float invulnerabilityDuration = 2f;
    public bool isInvulnerable = false;
    public float stepVolume = 0.05f;

    void Start()
    {   
        playerAnimator = GetComponent<Animator>();
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (footStepAudioSource == null)
        {
            Debug.LogWarning("footStepAudioSource não está atribuído. Sons de passos não serão reproduzidos.");
        }
        else
        {
            footStepAudioSource.volume = stepVolume;
        }
        // Verifica se o nome da cena é "Cena2" ou qualquer outro nome que você deseja verificar
        isInWaterScene = SceneManager.GetActiveScene().name == "Fase2_water";
    }

    // Update is called once per frame
    void Update()
    {   
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        if ( moveX == 0 && moveY == 0 && (playerDirection.x != 0 || playerDirection.y != 0)) {

            lastPlayerDirection = playerDirection;
        }

        playerDirection = new Vector2(moveX, moveY).normalized;

        playerAnimator.SetFloat("Horizontal", playerDirection.x);
        playerAnimator.SetFloat("Vertical", playerDirection.y);
        playerAnimator.SetFloat("Speed", playerDirection.sqrMagnitude);
        playerAnimator.SetFloat("LastHorizontalMove", lastPlayerDirection.x);
        playerAnimator.SetFloat("LastVerticalMove", lastPlayerDirection.y);
    }

    void FixedUpdate()
    {
        KnockLogic();
    }

    void KnockLogic(){
        if (KBCount < 0)
        {
            Move();
        } else
        {
            if (isKnockRight)
            {
                playerRigidBody2D.velocity = new Vector2(-KBCount, KBForce);
            } else
            {
                playerRigidBody2D.velocity = new Vector2(+KBCount, KBForce);
            }
        }
        KBCount -= Time.deltaTime;
    }


    void Move(){

        playerRigidBody2D.MovePosition(playerRigidBody2D.position + playerDirection * playerSpeed * Time.fixedDeltaTime);
        
        if(footStepAudioSource != null){
            if (playerDirection.sqrMagnitude > 0 && !footStepAudioSource.isPlaying)
            {
                footStepAudioSource.clip = soundEffects[0];
                footStepAudioSource.loop = true;
                footStepAudioSource.Play();
            } 

            if(playerDirection.sqrMagnitude <= 00 && footStepAudioSource.isPlaying)
            {
                footStepAudioSource.Stop();
            }
        }

        if (isInWaterScene)
        {
            AplicaEmpuxo();
            if(playerDirection.sqrMagnitude > 0){
                playerParticleSystem.Play();
            }
        }
    }

     

    void Flip()
    {
        if (playerDirection.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        } 
        else if (playerDirection.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }

    void AplicaEmpuxo()
    {
        playerRigidBody2D.AddForce(Vector2.up * buoyancyForce, ForceMode2D.Force);
    }

    public IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red; // Troca a cor para vermelho ou qualquer outra cor de sua escolha

        yield return new WaitForSeconds(invulnerabilityDuration);

        spriteRenderer.color = originalColor; // Retorna à cor original
        isInvulnerable = false;
    }
}
