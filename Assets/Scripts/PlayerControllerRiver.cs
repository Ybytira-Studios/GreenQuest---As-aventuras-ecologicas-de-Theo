using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRiver : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    public Animator playerAnimator;
    public SpriteRenderer spriteRenderer;
    private Vector2 playerDirection;
    private Vector2 lastPlayerDirection;
    public float playerSpeed;
    public AudioClip[] soundEffects;
    public float stepVolume = 1.5f;
    public AudioSource footStepAudioSource;
    public AudioSource disparaGarraAudioSource;
    public AudioSource grabGarraAudioSource;
    public bool canMove = true; // Variável que controla se o player pode se mover

    void Start()
    {   
        playerAnimator = GetComponent<Animator>();
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        footStepAudioSource.volume = stepVolume;
    }

    void Update()
    {
        if (canMove) // O jogador só se move se puder
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
        else
        {
            playerDirection = Vector2.zero; // Impede movimento
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
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
        }
    }
}