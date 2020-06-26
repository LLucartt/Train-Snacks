using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSlotMachine : MonoBehaviour
{
    public IngredientObjectScript ingredient;
    public bool ingredientCanBeAdded;

    void Update(){
      if(ingredient == null){
        AssignIngr();
      }
    }

    void AssignIngr(){
      if(Random.value > 0.2){ //80% chance
        if(Random.value >= 0.5){ //50% chance
          if(Random.value > 0){ //70% chance
            ingredient = GM.current.Ve1;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Ve1.IngredientImage;
          }
          if(Random.value > 0.8){ //50% chance
            ingredient = GM.current.Ve2;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Ve2.IngredientImage;
          }
          if(Random.value > 0.95){ //20% chance
            ingredient = GM.current.Ve3;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Ve3.IngredientImage;
          }
        }
        if(Random.value < 0.5){
          if(Random.value > 0){ //70% chance
            ingredient = GM.current.Fr1;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Fr1.IngredientImage;
          }
          if(Random.value > 0.8){ //50% chance
            ingredient = GM.current.Fr2;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Fr2.IngredientImage;
          }
          if(Random.value > 0.95){ //20% chance
            ingredient = GM.current.Fr3;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Fr3.IngredientImage;

          }
        }
      }

      if(Random.value > 0.5){ //50% chance
        if(Random.value >= 0.5){
          if(Random.value >0){ //70% chance
            ingredient = GM.current.Me1;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Me1.IngredientImage;
          }
          if(Random.value > 0.8){ //50% chance
            ingredient = GM.current.Me2;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Me2.IngredientImage;
          }
          if(Random.value > 0.95){ //20% chance
            ingredient = GM.current.Me3;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Me3.IngredientImage;
          }
        }
        if(Random.value < 0.5){
          if(Random.value > 0){ //70% chance
            ingredient = GM.current.Fi1;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Fi1.IngredientImage;
          }
          if(Random.value > 0.8){ //50% chance
            ingredient = GM.current.Fi2;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Fi2.IngredientImage;
          }
          if(Random.value > 0.95){ //20% chance
            ingredient = GM.current.Fi3;
            gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Fi3.IngredientImage;
          }
        }
      }

      if(Random.value > 0.7){//30% chance
        if(Random.value > 0){ //70% chance
          ingredient = GM.current.Ce1;
          gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Ce1.IngredientImage;
        }
        if(Random.value > 0.8){ //50% chance
          ingredient = GM.current.Ce2;
          gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Ce2.IngredientImage;
        }
        if(Random.value > 0.95){ //20% chance
          ingredient = GM.current.Ce3;
          gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Ce3.IngredientImage;
        }
      }

      if(Random.value > 0.9){//10% chance
        if(Random.value > 0){ //70% chance
          ingredient = GM.current.Da1;
          gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Da1.IngredientImage;
        }
        if(Random.value > 0.8){ //50% chance
          ingredient = GM.current.Da2;
          gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Da2.IngredientImage;
        }
        if(Random.value > 0.95){ //20% chance
          ingredient = GM.current.Da3;
          gameObject.GetComponent<SpriteRenderer>().sprite = GM.current.Da3.IngredientImage;
        }
      }
    }

    void OnTriggerEnter2D (Collider2D col){
      if(col.gameObject.tag == "PlayerSelectBox"){
        if(GM.current.IngrToChooseFrom.Contains(this.gameObject)){
        } else {
          ingredientCanBeAdded = true;
          GM.current.IngrToChooseFrom.Add(this.gameObject);
        }
      }
    }

    void OnTriggerStay2D (Collider2D col){
      if(col.gameObject.tag == "MainIngrSlot"){
        GM.current.MainIngredient = this.gameObject;
      }
      if(col.gameObject.tag == "SecondIngrSlot"){
        GM.current.SecondIngredient = this.gameObject;
      }
      if(col.gameObject.tag == "ThirdIngrSlot"){
        GM.current.ThirdIngredient = this.gameObject;
      }
    }

    void OnTriggerExit2D (Collider2D col){
      if(col.gameObject.tag == "PlayerSelectBox"){
        ingredientCanBeAdded = false;
        GM.current.IngrToChooseFrom.Remove(this.gameObject);
      }
      if(col.gameObject.tag == "SecondIngrSlot"){
        GM.current.SecondIngredient = null;
      }
      if(col.gameObject.tag == "ThirdIngrSlot"){
        GM.current.ThirdIngredient = null;
      }
    }
}
