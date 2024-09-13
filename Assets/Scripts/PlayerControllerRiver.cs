using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRiver : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    public SpriteRenderer spriteRenderer;
    private Vector2 playerDirection;
    public float playerSpeed;
    public bool canMove = true; // Variável que controla se o player pode se mover

    void Start()
    {   
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        }
    }
}