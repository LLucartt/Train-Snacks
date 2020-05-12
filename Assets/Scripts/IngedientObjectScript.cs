using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Food/ Ingredient")]
public class IngedientObjectScript : ScriptableObject
{

    private PlayerControllerScript pc;

    [Header("Info")]
    public Sprite foodImage;
    public string foodName;
    public string foodDesciption;

    [Header("Stats")]
    public int Freshness;
    public int edibilityStat;
    public int flavourStat;
    public int Nutitiousness;

    [Header("Combat")]
    public int healAmount;
    public int attackRange;
    public enum attackDirections{ xy, x, y, radius };
    public attackDirections attackDirection;
    public enum attackTypes{ Single };
    public attackTypes attackType;

    void start(){

    }

    public virtual void Use(){
      PlayerControllerScript.current.SwitchInventoryDelay();
      Debug.Log("choose character to eat " + foodName);
      PlayerControllerScript.current.feedingFood = this;
      PlayerControllerScript.current.chooseHungryChar = true;
      PlayerControllerScript.current.attackRangeUnit = attackRange;

      if(attackDirection == attackDirections.xy){
        PlayerControllerScript.current.attackDirectionUnit = "xy";
      }

      if(attackType == attackTypes.Single){
        PlayerControllerScript.current.attackTypeUnit = "Single";
      }
    }

    public void RemoveFromInventory(){
      InventoryScript.current.RemoveIngredient(this);
    }

}
