using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpIngredientScript : MonoBehaviour
{

    public IngedientObjectScript ingredient;

    void OnTriggerEnter2D (Collider2D col){
      if(col.gameObject.tag == "Player"){
        Debug.Log("Pickup");
        bool wasPickedUp = InventoryScript.current.AddIngredient(ingredient); // Add to player inventory
        if(wasPickedUp){
          Destroy(gameObject);
        }
      }
    }



}
