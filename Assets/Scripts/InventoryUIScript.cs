using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIScript : MonoBehaviour
{
    InventoryScript inventory;
    public Transform inventoryParent;
    public InventorySlotScript[] slots;

    void Start(){
      inventory = InventoryScript.current;
      inventory.OnIngredientChangedCallback += UpdateUI;

      slots = inventoryParent.GetComponentsInChildren<InventorySlotScript>();
    }

    void UpdateUI(){
        for (int i = 0; i < slots.Length; i++){
          if(i < inventory.ingredients.Count){
            slots[i].AddIngredient(inventory.ingredients[i]);
          } else {
              slots[i].ClearSlot();
          }
        }
    }


}
