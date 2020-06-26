using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript current;
    private void Awake(){current = this;}

    public delegate void OnIngredientChanged();
    public OnIngredientChanged OnIngredientChangedCallback;

    public delegate void OnMealChanged();
    public OnMealChanged OnMealChangedCallback;

    public int ingredientSpace = 3;
    public List<FoodObjectScript> ingredients = new List<FoodObjectScript>();

    public int mealSpace = 3;
    public List<FoodObjectScript> meals = new List<FoodObjectScript>();



    //adding ingredients to ingredient inventory
    public bool AddIngredient (FoodObjectScript ingredient){
      if(ingredients.Count >= ingredientSpace){
        Debug.Log("out of room");
        return false;
      }

      ingredients.Add(ingredient);

      if(OnIngredientChangedCallback != null){
          OnIngredientChangedCallback.Invoke();
      }

      return true;
    }

    //adding Meal to ingredient inventory
    public bool AddMeal (FoodObjectScript meal){
      if(meals.Count >= mealSpace){
        Debug.Log("out of room");
        return false;
      }

      meals.Add(meal);

      if(OnMealChangedCallback != null){
          OnMealChangedCallback.Invoke();
      }

      return true;
    }

    public void RemoveIngredient(FoodObjectScript ingredient){
      ingredients.Remove(ingredient);

      if(OnIngredientChangedCallback != null){
          OnIngredientChangedCallback.Invoke();
      }
    }

    public void RemoveMeal(FoodObjectScript meal){
      meals.Remove(meal);

      if(OnMealChangedCallback != null){
          OnMealChangedCallback.Invoke();
      }
    }


}
