using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CharacterCardScript : MonoBehaviour
{

    [Header ("General")]
    public string IAmChar;

    [Header ("Display")]
    public CharacterInfoScript CharacterInfoScript;
    public TextMeshProUGUI NameDisplay;
    public TextMeshProUGUI CurrentHealthDisplay;
    public TextMeshProUGUI MaxHealthDisplay;
    public Image FavFoodDisplay;
    public Image DislikeFoodDisplay;
    public Image IntolDisplay;

    private int attackMod = 0;
    private float attackStat = 1f;
    private int defenseMod = 0;
    private float defenseStat = 1f;
    private int accuracyMod = 0;
    private float accuracyStat = 1f;
    private int evasionMod = 0;
    private float evasionStat = 1f;
    private float currentHealth = 100;

    [SerializeField] GameObject shieldOverlay = null;
    [SerializeField] GameObject evadeOverlay = null;
    [SerializeField] GameObject weakenOverlay = null;
    [SerializeField] GameObject healOverlay = null;
    [SerializeField] GameObject attackOverlay = null;
    [SerializeField] GameObject UnpredictableOverlay = null;

    [HideInInspector] public int turnInCombat = 0;

    [HideInInspector] public CharacterCardScript enemy;

    [Header ("attack list properties")]
    [HideInInspector] public string MainAttack;
    [HideInInspector] public string secondBuff;
    [HideInInspector] public string thirdBuff;
    [HideInInspector] public float modStrength;
    [HideInInspector] public float boostMultiplier;


    void Start(){
      GM.current.AssignCharacters(IAmChar);
      DisplayInfoOnCard();
    }

    void Update(){/*
      if(currentHealth >= 0){
        if(IAmChar == "Play1"){
          GM.current.player1Dead = true;
        }
        if(IAmChar == "Play2"){
          GM.current.player2Dead = true;
        }
        if(IAmChar == "Play3"){
          GM.current.player3Dead = true;
        }
      }*/

      if(GM.current.battleMode && turnInCombat == GM.current.combatTurnNum && !GM.current.charAttacking){
        GM.current.charAttacking = true;
        StartCoroutine(IndividualCombatSequence());
      }
    }

    void statModChecker(float statMod, float stat){
      if(statMod == -6f){ stat = 0.25f; }
      if(statMod == -5f){ stat = 0.28f; }
      if(statMod == -4f){ stat = 0.33f; }
      if(statMod == -3f){ stat = 0.40f; }
      if(statMod == -2f){ stat = 0.50f; }
      if(statMod == -1f){ stat = 0.66f; }
      if(statMod == 0f){ stat = 1f; }
      if(statMod == 1f){ stat = 1.5f; }
      if(statMod == 2f){ stat = 2f; }
      if(statMod == 3f){ stat = 2.5f; }
      if(statMod == 4f){ stat = 3f; }
      if(statMod == 5f){ stat = 2.5f; }
      if(statMod == 6f){ stat = 4f; }
    }

    public void DisplayInfoOnCard(){
      if(IAmChar == "Play1"){
        CharacterInfoScript = GM.current.PlayerChar1;
      }
      if(IAmChar == "Play2"){
        CharacterInfoScript = GM.current.PlayerChar2;
      }
      if(IAmChar == "Play3"){
        CharacterInfoScript = GM.current.PlayerChar3;
      }
      if(IAmChar == "Enem1"){
        CharacterInfoScript = GM.current.EnemyChar1;
      }
      if(IAmChar == "Enem2"){
        CharacterInfoScript = GM.current.EnemyChar2;
      }
      if(IAmChar == "Enem3"){
        CharacterInfoScript = GM.current.EnemyChar3;
      }

      NameDisplay.text = CharacterInfoScript.name;
      MaxHealthDisplay.text = CharacterInfoScript.maxHealth.ToString();
      FavFoodDisplay.sprite = CharacterInfoScript.FavFood.IngredientImage;
      DislikeFoodDisplay.sprite = CharacterInfoScript.DislikeFood.IngredientImage;
      IntolDisplay.sprite = CharacterInfoScript.Intolerance.IngredientImage;
    }

    public void Feed(){
      if(CookBookScript.current.feedCharBool = true){
        if(IAmChar == "Play1"){
          GM.current.Player1Eaten = true;
        }
        if(IAmChar == "Play2"){
          GM.current.Player2Eaten = true;
        }
        if(IAmChar == "Play3"){
          GM.current.Player3Eaten = true;
        }

        CookBookScript.current.FeedChar(this);
        GetComponent<Button>().interactable = false;
        CookBookScript.current.feedCharBool = false;
      }
    }

    IEnumerator IndividualCombatSequence(){
      if(IAmChar == "Enem1" || IAmChar == "Enem2" || IAmChar == "Enem3"){
        MainAttack = "Attack";
      }
      yield return new WaitForSeconds(0.5f);

      if(MainAttack == "Shield"){
        shieldOverlay.SetActive(true);
        defenseMod = defenseMod + 1;
        statModChecker(defenseMod, defenseStat);
        yield return new WaitForSeconds(2f);
        shieldOverlay.SetActive(false);
      }

      if(MainAttack == "Evade"){
        evadeOverlay.SetActive(true);
        evasionMod = evasionMod + 1;
        statModChecker(evasionMod, evasionStat);
        yield return new WaitForSeconds(2f);
        evadeOverlay.SetActive(false);
      }

      if(MainAttack == "Weaken"){
        weakenOverlay.SetActive(true);
        yield return new WaitForSeconds(2f);
        weakenOverlay.SetActive(false);
      }

      if(MainAttack == "Heal"){
        healOverlay.SetActive(true);
        currentHealth = currentHealth + 20;
        yield return new WaitForSeconds(2f);
        healOverlay.SetActive(false);
      }

      if(MainAttack == "Attack"){
        attackOverlay.SetActive(true);
        attackMod = attackMod + 1;
        statModChecker(attackMod, attackStat);
        yield return new WaitForSeconds(2f);
        attackOverlay.SetActive(false);
      }

      if(MainAttack == "Unpredictable"){
        UnpredictableOverlay.SetActive(true);
        //other things
        yield return new WaitForSeconds(2f);
        UnpredictableOverlay.SetActive(false);
      }

      MainAttack = null;
      secondBuff = null;
      thirdBuff = null;
      GM.current.updateBattleTurn();
    }

    public void chooseEnemy(){
      enemy = this;
    }
}
