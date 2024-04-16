using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    private bool pickUpAllowed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Default_Theodoro")){
            pickUpAllowed = true;
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Default_Theodoro")){
            pickUpAllowed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (pickUpAllowed && Input.GetKeyDown(KeyCode.E)) {
        PickUp();
       }
    }

    void PickUp(){
        Debug.Log("Picking up");
        Destroy(gameObject);
    }
}
