using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour
{
    FoodObjectScript ingredient;
    FoodObjectScript meal;
    public Image icon;

    public void AddIngredient (FoodObjectScript newIngredient){
      ingredient = newIngredient;
      icon.sprite = ingredient.foodImage;
      icon.enabled = true;
    }

    public void AddMeal (FoodObjectScript newMeal){
      meal = newMeal;
      icon.sprite = meal.foodImage;
      icon.enabled = true;
    }

    public void ClearIngredientSlot(){
      ingredient = null;
      icon.enabled = false;
    }

    public void ClearMealSlot(){
      meal = null;
      icon.enabled = false;
    }

    public void UseIngredient(){
      if(ingredient != null){
        ingredient.Use();
      }
    }

    public void UseMeal(){
      if(meal != null){
        meal.Use();
      }
    }


}
