using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private Rigidbody2D playerRigidBody2D;
   private Animator playerAnimator;
   public  float playerSpeed;
   private Vector2 playerDirection;

    void Start()
    {   
        playerAnimator = GetComponent<Animator>();
        playerRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(playerDirection.sqrMagnitude > 0)
        {
            playerAnimator.SetInteger("Movimento", 1);
        } else{
            playerAnimator.SetInteger("Movimento", 0);
        }

        Flip();

    }

    void FixedUpdate(){
        playerRigidBody2D.MovePosition(playerRigidBody2D.position + playerDirection * playerSpeed * Time.fixedDeltaTime);
    }

 void Flip(){
        if(playerDirection.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        } 
        else if (playerDirection.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }
}

   