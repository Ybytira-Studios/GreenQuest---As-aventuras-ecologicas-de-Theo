using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public float interactDistance = 1f;
    public KeyCode interactKey = KeyCode.E;
    public Vector2 carryOffset = new Vector2(1f, 0f); // Offset para posicionar o objeto coletado à frente do jogador

    private GameObject carriedObject;
    private Rigidbody2D carriedRigidbody;

    void Start(){
        
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (carriedObject == null)
            {
                InteractWithObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    void InteractWithObject()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, interactDistance);
        foreach (Collider2D objCollider in nearbyObjects)
        {
            if (objCollider.CompareTag("GlassTrash") || objCollider.CompareTag("MetalTrash") || objCollider.CompareTag("PlasticTrash") || objCollider.CompareTag("PaperTrash"))
            {
                carriedObject = objCollider.gameObject;
                carriedRigidbody = carriedObject.GetComponent<Rigidbody2D>();
                // Define a posição do objeto coletado à frente do jogador, com base no offset
                UpdateCarriedObjectPosition();
                break;
            }
        }
    }

    void DropObject()
    {
        if (carriedObject != null)
        {
            carriedObject = null;
            carriedRigidbody = null;
        }
    }

    void FixedUpdate()
    {
        if (carriedObject != null)
        {
            // Mantém o objeto à frente do jogador o tempo todo enquanto estiver segurando
            UpdateCarriedObjectPosition();
        }
    }

    void UpdateCarriedObjectPosition()
    {
        if (carriedObject != null)
        {
            // Define a posição do objeto à frente do jogador, com base no offset
            carriedObject.transform.position = (Vector2)transform.position + carryOffset;
        }
    }
}