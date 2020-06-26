using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Food/ Ingredient")]
public class FoodObjectScript : ScriptableObject
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
    public string AttackDescription;
    public int healAmount;
    public int damage;
    public int attackRange;
    public enum attackDirections{ xy, x, y, o};
    public attackDirections attackDirection;
    public enum attackTypes{ Single };
    public attackTypes attackType;
    public List<string> attackEffects = new List<string>();

    [Header("Additional stats for meals")]
    public bool isMeal;

    void start(){

    }

    public virtual void Use(){
      PlayerControllerScript.current.SwitchInventoryDelay();
      Debug.Log("choose character to eat " + foodName);
      PlayerControllerScript.current.feedingFood = this;
      PlayerControllerScript.current.chooseHungryChar = true;
      PlayerControllerScript.current.attackRangeUnit = attackRange;

      //transfer attackdirections to PlayerControllerScript
      if(attackDirection == attackDirections.xy){
        PlayerControllerScript.current.attackDirectionUnit = "xy";
      }
      else if(attackDirection == attackDirections.x){
        PlayerControllerScript.current.attackDirectionUnit = "x";
      }
      else if(attackDirection == attackDirections.y){
        PlayerControllerScript.current.attackDirectionUnit = "y";
      }
      else if(attackDirection == attackDirections.o){
        PlayerControllerScript.current.attackDirectionUnit = "o";
      }

      //transfer attackTypes to PlayerControllerScript
      if(attackType == attackTypes.Single){
        PlayerControllerScript.current.attackTypeUnit = "Single";
      }

    }

    public void RemoveFromInventory(){
      if(isMeal){
        InventoryScript.current.RemoveMeal(this);
      } else { InventoryScript.current.RemoveIngredient(this); }
    }

}
