using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TesteJogadorControlador : MonoBehaviour
{
     public Rigidbody2D playerRigidBody2D;
    public float playerSpeed;
    private Vector2 playerDirection;   


    // Update is called once per frame
    void Update()
    {   
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(moveX, moveY).normalized;
        playerRigidBody2D.MovePosition(playerRigidBody2D.position + playerSpeed * Time.deltaTime * playerDirection);
    }
}
