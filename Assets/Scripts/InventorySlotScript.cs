using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour
{
    IngedientObjectScript ingredient;
    public Image icon;

    public void AddIngredient (IngedientObjectScript newIngredient){
      ingredient = newIngredient;
      icon.sprite = ingredient.foodImage;
      icon.enabled = true;
    }

    public void ClearSlot(){
      ingredient = null;
      icon.enabled = false;
    }

    public void UseIngredient(){
      if(ingredient != null){
        ingredient.Use();
      }
    }


}
