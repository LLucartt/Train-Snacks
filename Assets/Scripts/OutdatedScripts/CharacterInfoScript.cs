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
    public int currentHealth;
    [HideInInspector] public int stamina;
    public IngredientObjectScript FavFood;
    public IngredientObjectScript DislikeFood;
    public IngredientObjectScript Intolerance;

    /*
    public int initiativeStat;
    private int attackMod = 0;
    private float attackStat = 1f;
    private int defenseMod = 0;
    private float defenseStat = 1f;
    private int accuracyMod = 0;
    private float accuracyStat = 1f;
    private int evasionMod = 0;
    private float evasionStat = 1f;
    */
}
