using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


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

    public int attackMod = 0;
    public float attackStat = 1f;
    public int defenseMod = 0;
    public float defenseStat = 1f;
    public int accuracyMod = 0;
    public float accuracyStat = 1f;
    public int evasionMod = 0;
    public float evasionStat = 1f;
    public float currentHealth = 100;
    private float stat;

    [SerializeField] GameObject shieldOverlay = null;
    [SerializeField] GameObject evadeOverlay = null;
    [SerializeField] GameObject weakenOverlay = null;
    [SerializeField] GameObject healOverlay = null;
    [SerializeField] GameObject attackOverlay = null;
    [SerializeField] GameObject UnpredictableOverlay = null;

    private float baseAttack = 10;

    [HideInInspector] public int turnInCombat = 0;

    [Header ("Attack list properties")]
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

    void statModChecker(int statMod){
      stat = statMod == -6 ? 0.25f : stat;
      stat = statMod == -5 ? 0.28f : stat;
      stat = statMod == -4 ? 0.33f : stat;
      stat = statMod == -3 ? 0.40f : stat;
      stat = statMod == -2 ? 0.50f : stat;
      stat = statMod == -1 ? 0.66f : stat;
      stat = statMod == 0  ? 1f : stat;
      stat = statMod == 1  ? 1.5f : stat;
      stat = statMod == 2  ? 2f : stat;
      stat = statMod == 3  ? 2.5f : stat;
      stat = statMod == 4  ? 3f : stat;
      stat = statMod == 5  ? 3.5f : stat;
      stat = statMod == 6  ? 4f : stat;
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
      if(CookBookScript.current.feedCharBool = true && (IAmChar == "Play1" || IAmChar == "Play2" || IAmChar == "Play3")){
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
        int whichAction = Random.Range(1, 7);
        if(whichAction == 1){ MainAttack = "Attack"; }
        if(whichAction == 2){ MainAttack = "Heal"; }
        if(whichAction == 3){ MainAttack = "Shield"; }
        if(whichAction == 4){ MainAttack = "Evade"; }
        if(whichAction == 5){ MainAttack = "Weaken"; }
        if(whichAction == 6){ MainAttack = "Unpredictable"; }
      }
      yield return new WaitForSeconds(0.5f);

      if(MainAttack == "Shield"){
        shieldOverlay.SetActive(true);
        defenseMod = defenseMod + 1;
        statModChecker(defenseMod);
        defenseStat = stat;
        yield return new WaitForSeconds(2f);
        StartCoroutine(EndTurn());
        GM.current.updateBattleTurn();
      }

      if(MainAttack == "Evade"){
        evadeOverlay.SetActive(true);
        evasionMod = evasionMod + 1;
        statModChecker(evasionMod);
        evasionStat = stat;
        yield return new WaitForSeconds(2f);
        StartCoroutine(EndTurn());
        GM.current.updateBattleTurn();
      }

      if(MainAttack == "Weaken"){
        weakenOverlay.SetActive(true);
        yield return new WaitForSeconds(2f);

        if(IAmChar == "Play1" || IAmChar == "Play2" || IAmChar == "Play3"){
          //StartCoroutine(EndTurn());
          GM.current.characterAttacking = this.GetComponent<CharacterCardScript>();
          GM.current.enem1BattleCard.GetComponent<Button>().interactable = true;
          GM.current.enem2BattleCard.GetComponent<Button>().interactable = true;
          GM.current.enem3BattleCard.GetComponent<Button>().interactable = true;
          EventSystem.current.SetSelectedGameObject(GM.current.enem1BattleCard);
        } else {
          GM.current.characterAttacking = this.GetComponent<CharacterCardScript>();

          int whichTarget = Random.Range(1, 4);
          if(whichTarget == 1){ GM.current.Enemies.Add(GM.current.play1BattleCard.GetComponent<CharacterCardScript>()); }
          if(whichTarget == 2){ GM.current.Enemies.Add(GM.current.play2BattleCard.GetComponent<CharacterCardScript>()); }
          if(whichTarget == 3){ GM.current.Enemies.Add(GM.current.play3BattleCard.GetComponent<CharacterCardScript>()); }

          StartCoroutine(WeakenSequence());
        }
        //StartCoroutine(EndTurn());
        //GM.current.updateBattleTurn();
      }

      if(MainAttack == "Heal"){
        healOverlay.SetActive(true);
        currentHealth += 10;
        CurrentHealthDisplay.text = currentHealth.ToString();
        yield return new WaitForSeconds(2f);
        StartCoroutine(EndTurn());
        GM.current.updateBattleTurn();
      }

      if(MainAttack == "Attack"){
        attackOverlay.SetActive(true);
        attackMod += 1;
        statModChecker(attackMod);
        attackStat = stat;
        yield return new WaitForSeconds(2f);

        if(IAmChar == "Play1" || IAmChar == "Play2" || IAmChar == "Play3"){
          //StartCoroutine(EndTurn());
          GM.current.characterAttacking = this.GetComponent<CharacterCardScript>();
          GM.current.enem1BattleCard.GetComponent<Button>().interactable = true;
          GM.current.enem2BattleCard.GetComponent<Button>().interactable = true;
          GM.current.enem3BattleCard.GetComponent<Button>().interactable = true;
          EventSystem.current.SetSelectedGameObject(GM.current.enem1BattleCard);
        } else {
          GM.current.characterAttacking = this.GetComponent<CharacterCardScript>();

          int whichTarget = Random.Range(1, 4);
          if(whichTarget == 1){ GM.current.Enemies.Add(GM.current.play1BattleCard.GetComponent<CharacterCardScript>()); }
          if(whichTarget == 2){ GM.current.Enemies.Add(GM.current.play2BattleCard.GetComponent<CharacterCardScript>()); }
          if(whichTarget == 3){ GM.current.Enemies.Add(GM.current.play3BattleCard.GetComponent<CharacterCardScript>()); }

          StartCoroutine(AttackSequence());
        }
      }

      if(MainAttack == "Unpredictable"){
        UnpredictableOverlay.SetActive(true);
        //other things
        yield return new WaitForSeconds(2f);
        StartCoroutine(EndTurn());
        GM.current.updateBattleTurn();
      }
    }

    public IEnumerator EndTurn(){
      shieldOverlay.SetActive(false);
      evadeOverlay.SetActive(false);
      weakenOverlay.SetActive(false);
      healOverlay.SetActive(false);
      attackOverlay.SetActive(false);
      UnpredictableOverlay.SetActive(false);
      MainAttack = null;
      secondBuff = null;
      thirdBuff = null;
      yield return new WaitForSeconds(0.1f);
    }

    public void chooseEnemy(){
      if(IAmChar == "Enem1" || IAmChar == "Enem2" || IAmChar == "Enem3"){
        GM.current.Enemies.Add(this.GetComponent<CharacterCardScript>());
        if(GM.current.characterAttacking.MainAttack == "Attack"){
          StartCoroutine(GM.current.characterAttacking.EndTurn());
          StartCoroutine(AttackSequence());
        }
        else if(GM.current.characterAttacking.MainAttack == "Weaken"){
          StartCoroutine(GM.current.characterAttacking.EndTurn());
          StartCoroutine(WeakenSequence());
        }
        GM.current.enem1BattleCard.GetComponent<Button>().interactable = false;
        GM.current.enem2BattleCard.GetComponent<Button>().interactable = false;
        GM.current.enem3BattleCard.GetComponent<Button>().interactable = false;
      }
    }

    IEnumerator AttackSequence(){
      foreach (CharacterCardScript Enemy in GM.current.Enemies) {
        float EvasionChance = (Enemy.accuracyStat / 1) * 15;
        if(Random.value > EvasionChance){
          Enemy.evadeOverlay.SetActive(true);
          yield return new WaitForSeconds(0.2f);
          Enemy.evadeOverlay.SetActive(false);
        } else {
            float AmountOfDamage = (GM.current.characterAttacking.attackStat / Enemy.defenseStat) * baseAttack;
            Enemy.currentHealth = Enemy.currentHealth - AmountOfDamage;
            Enemy.CurrentHealthDisplay.text = Enemy.currentHealth.ToString();
          }
      }

      GM.current.Enemies.Clear();
      StartCoroutine(EndTurn());
      GM.current.updateBattleTurn();
      yield return new WaitForSeconds(0.1f);
    }

    IEnumerator WeakenSequence(){
      foreach (CharacterCardScript Enemy in GM.current.Enemies) {
            Enemy.attackMod += -1;
            statModChecker(attackMod);
            Enemy.attackStat = stat;
            Enemy.weakenOverlay.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Enemy.weakenOverlay.SetActive(false);
      }


      GM.current.Enemies.Clear();
      StartCoroutine(EndTurn());
      GM.current.updateBattleTurn();
      yield return new WaitForSeconds(0.1f);
    }
}
