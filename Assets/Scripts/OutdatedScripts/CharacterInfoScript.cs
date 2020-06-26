using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/ InfoSheet")]
public class CharacterInfoScript : ScriptableObject
{
    public Sprite characterPort;
    public Sprite inGameChar;
    public string name;
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int stamina;
    public IngredientObjectScript FavFood;
    public IngredientObjectScript DislikeFood;
    public IngredientObjectScript Intolerance;
}
