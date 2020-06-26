using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIScript : MonoBehaviour
{
    InventoryScript inventory;
    public Transform inventoryIngredientParent;
    public InventorySlotScript[] IngredientSlots;

    public Transform inventoryMealParent;
    public InventorySlotScript[] MealSlots;

    void Start(){
      inventory = InventoryScript.current;

      inventory.OnIngredientChangedCallback += UpdateIngredientUI;
      inventory.OnMealChangedCallback += UpdateMealUI;

      IngredientSlots = inventoryIngredientParent.GetComponentsInChildren<InventorySlotScript>();
      MealSlots = inventoryMealParent.GetComponentsInChildren<InventorySlotScript>();
    }

    void UpdateIngredientUI(){
        for (int i = 0; i < IngredientSlots.Length; i++){
          if(i < inventory.ingredients.Count){
            IngredientSlots[i].AddIngredient(inventory.ingredients[i]);
          } else {
              IngredientSlots[i].ClearIngredientSlot();
          }
        }
    }

    void UpdateMealUI(){
        for (int i = 0; i < MealSlots.Length; i++){
          if(i < inventory.meals.Count){
            MealSlots[i].AddMeal(inventory.meals[i]);
          } else {
              MealSlots[i].ClearMealSlot();
          }
        }
    }


}
