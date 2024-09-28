using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRiver : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    public SpriteRenderer spriteRenderer;
    private Vector2 playerDirection;
    public float playerSpeed;
    public AudioClip[] soundEffects;
    public float stepVolume = 1.5f;
    public AudioSource footStepAudioSource;
    public AudioSource disparaGarraAudioSource;
    public AudioSource grabGarraAudioSource;
    public bool canMove = true; // Variável que controla se o player pode se mover

    void Start()
    {   
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
            playerDirection = new Vector2(moveX, moveY).normalized;
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