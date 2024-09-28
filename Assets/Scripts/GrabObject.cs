using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GrabObject : MonoBehaviour
{
    public float interactDistance = 1f;
    public Image imageKeyE;
    public string[] trashTags = { "GlassTrash", "MetalTrash", "PlasticTrash", "PaperTrash", "claw"};
    public PlayerController playerController;
    public PlayerControllerRiver playerControllerRiver;
    public KeyCode interactKey = KeyCode.E;
    public Vector2 carryOffset = new Vector2(1f, 0f); // Offset para posicionar o objeto coletado à frente do jogador
    public Vector2 imageOffset = new Vector2(0f, 2f); // Offset para posicionar a imagem da tecla acima do jogador
    public bool isGrabbing = false;
    private GameObject carriedObject;
    private Rigidbody2D carriedRigidbody;
    private int grabCounter = 0;
    private bool isInRiverScene;
    
    public float volumeGrabbing;

    void Start()
    {
        isInRiverScene = SceneManager.GetActiveScene().name == "Fase4_river";
        if(!isInRiverScene){
        playerController.grabAudioSource.clip = playerController.soundEffects[1];
        playerController.grabAudioSource.volume = volumeGrabbing;
        }
        imageKeyE.gameObject.SetActive(false);
    }

    public GameObject GetCarriedObject()
{
    return carriedObject;
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

        UpdateImagePosition();
    }

    public void InteractWithObject()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, interactDistance);
        foreach (Collider2D objCollider in nearbyObjects)
        {
            if (IsTrashObject(objCollider.tag))
            {
                if(!isInRiverScene){
                    playerController.grabAudioSource.Play();
                } else if(isInRiverScene) {
                    playerControllerRiver.grabGarraAudioSource.Play();
                }
                isGrabbing = true;
                carriedObject = objCollider.gameObject;
                carriedRigidbody = carriedObject.GetComponent<Rigidbody2D>();
                UpdateCarriedObjectPosition();
                grabCounter ++;
                imageKeyE.gameObject.SetActive(false);
                break;
            }
        }
    }

    bool IsTrashObject(string tag)
    {
        foreach (string trashTag in trashTags)
        {
            if (tag == trashTag)
            {
                return true;
            }
        }
        return false;
    }

   public void DropObject()
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
            UpdateCarriedObjectPosition();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsTrashObject(other.tag))
        {
            if(grabCounter < 2)
            imageKeyE.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsTrashObject(other.tag) && imageKeyE != null)
        {
            imageKeyE.gameObject.SetActive(false);
        }
        //Debug.LogWarning("Saiu do trigger.");
    }

    void UpdateCarriedObjectPosition()
    {
        if (carriedObject != null)
        {
            carriedObject.transform.position = (Vector2)transform.position + carryOffset;
        }
    }

    void UpdateImagePosition()
    {
        if (imageKeyE.gameObject.activeSelf)
        {
            Vector3 playerPosition = transform.position;
            Vector3 newPosition = playerPosition + (Vector3)imageOffset;
            imageKeyE.transform.position = newPosition;
        }
    }
}
