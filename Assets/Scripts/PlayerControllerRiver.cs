using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRiver : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    public SpriteRenderer spriteRenderer;
    private Vector2 playerDirection;
    public float playerSpeed;
    // Start is called before the first frame update
     void Start()
    {   
        
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate(){
        playerRigidBody2D.MovePosition(playerRigidBody2D.position + playerDirection * playerSpeed * Time.fixedDeltaTime);
    }
}
