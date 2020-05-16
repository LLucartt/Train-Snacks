using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript current;
    private void Awake(){current = this;}

    public delegate void OnIngredientChanged();
    public OnIngredientChanged OnIngredientChangedCallback;

    public int ingredientSpace = 3;
    public List<IngedientObjectScript> ingredients = new List<IngedientObjectScript>();



    //adding ingredients to ingredient inventory
    public bool AddIngredient (IngedientObjectScript ingredient){
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

    public void RemoveIngredient(IngedientObjectScript ingredient){
      ingredients.Remove(ingredient);

      if(OnIngredientChangedCallback != null){
          OnIngredientChangedCallback.Invoke();
      }
    }


}
