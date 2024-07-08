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
    public float playerSpeed;
    public float buoyancyForce = 5f; // Força de empuxo a ser aplicada
    public float KBForce;
    public float KBCount;
    public float KBTime;
    public bool isKnockRight;
    private Vector2 playerDirection;
    private bool isInWaterScene;
    public float invulnerabilityDuration = 2f;
    public bool isInvulnerable = false;

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
            footStepAudioSource.volume = 0.1f;
        }
        // Verifica se o nome da cena é "Cena2" ou qualquer outro nome que você deseja verificar
        isInWaterScene = SceneManager.GetActiveScene().name == "Fase2_water";
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (playerDirection.sqrMagnitude > 0)
        {
            playerAnimator.SetInteger("Movimento", 1);
        } 
        else
        {
            playerAnimator.SetInteger("Movimento", 0);
        }

        Flip();
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
