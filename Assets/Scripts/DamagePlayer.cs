using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public HeartSystem heartSystem;
    public PlayerController playerController;
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.KBCount = playerController.KBTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerController.isKnockRight = true;
            } else
            {
                playerController.isKnockRight = false;
            }
            heartSystem.vida--;
            // todo Fazer animação de player recebendo dano quando tiver uma
        }
    }
}
