using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpIngredientScript : MonoBehaviour
{

    public FoodObjectScript ingredient;

    void Awake(){
      ingredient = GameMaster.current.IngredientVariety[(Random.Range(0, GameMaster.current.IngredientVariety.Length))];
    }

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
