using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public HeartSystem heartSystem;
    public PlayerController playerController;
    public int damage;
    public Animator animator;
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player") && !playerController.isInvulnerable)
        {
            playerController.KBCount = playerController.KBTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerController.isKnockRight = true;
            } else
            {
                playerController.isKnockRight = false;
            }


            heartSystem.vida -= damage;

            animator.SetTrigger("attack");

            StartCoroutine(playerController.InvulnerabilityCoroutine());
            // todo Fazer animação de player recebendo dano quando tiver uma
        }
    }
}
