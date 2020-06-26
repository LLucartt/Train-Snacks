using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIngr", menuName = "Food/ NewIngredient")]
public class IngredientObjectScript : ScriptableObject
{
  [Header("Info")]
  public string IngrCode;
  public Sprite IngredientImage;
  public string IngredientName;
  public string IngredientDesciption;
  public string MainAbility;
  public string SecondAbility;

  [Header("Stats")]
  public float IngredientQuality;
  public string IngredientCatagory;
  public string IngredientTier;
}
