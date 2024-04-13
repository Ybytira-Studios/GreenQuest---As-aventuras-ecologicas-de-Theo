using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private Rigidbody2D playerRigidBody2D;
   public  float playerSpeed;
   private Vector2 playerDirection;

    void Start()
    {
       playerRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate(){
        playerRigidBody2D.MovePosition(playerRigidBody2D.position + playerDirection * playerSpeed * Time.fixedDeltaTime);
    }

}
