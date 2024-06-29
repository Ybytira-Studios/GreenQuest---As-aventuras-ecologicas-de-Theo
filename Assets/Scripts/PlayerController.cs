using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    private Animator playerAnimator;
    public float playerSpeed;
    public float buoyancyForce = 5f; // Força de empuxo a ser aplicada
    public float KBForce;
    public float KBCount;
    public float KBTime;
    public bool isKnockRight;
    private Vector2 playerDirection;
    private bool isInWaterScene;

    void Start()
    {   
        playerAnimator = GetComponent<Animator>();
        playerRigidBody2D = GetComponent<Rigidbody2D>();

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

        if (isInWaterScene)
       {
            AplicaEmpuxo();
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
}
