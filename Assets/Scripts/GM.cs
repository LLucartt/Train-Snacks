using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GM : MonoBehaviour
{
    public static GM current;
    private void Awake(){current = this;}

    [Header ("Spawn Ingredients")]
    public GameObject[] tiles;
    public GameObject IngredientTokenPrefab;
    public Transform spawnPointRight;
    public Transform spawnPointCenter;
    public Transform spawnPointLeft;
    private bool checkingTime;

    [Header ("Spawn Player Characters")]
    public GameObject[] playerCharLocations;
    public GameObject PlayerTokenPrefab;
    public GameObject[] PlayerShortCutDisplays;

    [Header ("Ingredients")]
    public IngredientObjectScript Me1;
    public IngredientObjectScript Me2;
    public IngredientObjectScript Me3;
    public IngredientObjectScript Fi1;
    public IngredientObjectScript Fi2;
    public IngredientObjectScript Fi3;
    public IngredientObjectScript Ce1;
    public IngredientObjectScript Ce2;
    public IngredientObjectScript Ce3;
    public IngredientObjectScript Fr1;
    public IngredientObjectScript Fr2;
    public IngredientObjectScript Fr3;
    public IngredientObjectScript Ve1;
    public IngredientObjectScript Ve2;
    public IngredientObjectScript Ve3;
    public IngredientObjectScript Da1;
    public IngredientObjectScript Da2;
    public IngredientObjectScript Da3;

    [Header ("playerMeals")]
    public TextMeshProUGUI activeMealDisplay;
    public int ActiveMeal = 1;
    public GameObject MainIngredient;
    public GameObject SecondIngredient;
    public GameObject ThirdIngredient;
    public List<GameObject> meal1 = new List<GameObject>();
    public List<GameObject> meal2 = new List<GameObject>();
    public List<GameObject> meal3 = new List<GameObject>();
    public List<GameObject> potSlots = new List<GameObject>();
    private bool canBeStirred = true;
    public GameObject CookedDisplay;
    public bool meal1Cooked;
    public bool meal2Cooked;
    public bool meal3Cooked;
    public GameObject cookAnimation;

    [Header ("player controls")]
    public GameObject playerContr;
    public GameObject StartLoctionInSlots;
    private bool playerInPot = false;
    private float timer;

    [Header ("Turn Manager")]
    public int turnMan;
    public bool canSpawnIngr;
    public GameObject playerTurnGo;
    public GameObject enemyTurnGo;

    [Header ("Enemy Ai")]
    public List<GameObject> IngrToChooseFrom = new List<GameObject>();
    public List<GameObject> AiMeal1 = new List<GameObject>();
    public List<GameObject> AiMeal2 = new List<GameObject>();
    public List<GameObject> AiMeal3 = new List<GameObject>();
    public GameObject EnemyPot;
    private bool EnemyCanPlay = true;

    [Header ("Charaters")]
    [HideInInspector] public CharacterInfoScript PlayerChar1;
    [HideInInspector] public CharacterInfoScript PlayerChar2;
    [HideInInspector] public CharacterInfoScript PlayerChar3;
    [HideInInspector] public CharacterInfoScript EnemyChar1;
    [HideInInspector] public CharacterInfoScript EnemyChar2;
    [HideInInspector] public CharacterInfoScript EnemyChar3;
    [HideInInspector] public bool Player1Eaten;
    [HideInInspector] public bool Player2Eaten;
    [HideInInspector] public bool Player3Eaten;

    [Header ("Combat")]
    public GameObject CombatScene;
    public GameObject SlotMachineUi;
    public GameObject SlotMachineGo;
    [HideInInspector] public bool battleMode;
    public GameObject play1BattleCard;
    public GameObject play2BattleCard;
    public GameObject play3BattleCard;
    public GameObject enem1BattleCard;
    public GameObject enem2BattleCard;
    public GameObject enem3BattleCard;
    public List<CharacterCardScript> characterTurns = new List<CharacterCardScript>();
    public List<CharacterCardScript> characterTurnsReshuffel = new List<CharacterCardScript>();
    public GameObject playIconPrefab;
    public GameObject enemIconPrefab;
    public Transform TurnParent;
    [HideInInspector] public int combatTurnNum;
    [SerializeField] List<TextMeshProUGUI> charTurnDisplays = new List<TextMeshProUGUI>();
    [SerializeField] List<GameObject> ActiveTurnDisplays = new List<GameObject>();
    [SerializeField] Transform EnemyLocationInBattle;
    [SerializeField] Transform prevEnemyLoc;
    [HideInInspector] public bool charAttacking;
    public List<CharacterCardScript> Enemies = new List<CharacterCardScript>();
    [HideInInspector] public CharacterCardScript characterAttacking;

    [Header ("Controls")]
    [HideInInspector] public bool allowInput = true;

    void Start(){
      //AssignCharacters();
      StartCoroutine(spawn());
      UpdateCookingPot();
      playerContr.transform.position = StartLoctionInSlots.transform.position;
      PC.current.ResetInfo();
      //StartCoroutine(spawnPlayerCharacters());
    }

    void Update(){
      if(allowInput){
        if(Input.GetButtonDown("CycleBack") && ActiveMeal > 1 && turnMan == 1){
          ActiveMeal --;
          UpdateCookingPot();
        }
        if(Input.GetButtonDown("CycleForward") && ActiveMeal < 3 && turnMan == 1){
          ActiveMeal ++;
          UpdateCookingPot();
        }
        if(Input.GetButtonDown("Tab") && turnMan == 1){
          SwitchInventory();
        }
        if(IngrToChooseFrom.Count == 0 && canSpawnIngr && !checkingTime){
          checkingTime = true;
          EnemyCanPlay = false;
          StartCoroutine(forSomeTime());
        }
        if(playerInPot && Input.GetButtonDown("Submit") && canBeStirred && MainIngredient != null){
          if((ActiveMeal == 1 && meal1.Count >= 2 && !meal1Cooked) || (ActiveMeal == 2 && meal2.Count >= 2 && !meal2Cooked) || (ActiveMeal == 3 && meal3.Count >= 2 && !meal3Cooked)){
            canBeStirred = false;
            StartCoroutine(StirPot());
          }
        }

        if(Input.GetButton("Cook") && ((ActiveMeal == 1 && meal1.Count != 0 && !meal1Cooked) || (ActiveMeal == 2 && meal2.Count != 0 && !meal2Cooked) || (ActiveMeal == 3 && meal3.Count != 0 && !meal3Cooked))){
          timer += Time.deltaTime;
          cookAnimation.SetActive(true);

          if(timer >= 3){
            Debug.Log("Cooking: " + ActiveMeal);
            CookBookScript.current.checkIngredients();
            allowInput = false;
            timer = 0;
            cookAnimation.SetActive(false);
          }
        }
        if(Input.GetButtonUp("Cook") && timer < 3){
          timer = 0;
          cookAnimation.SetActive(false);
        }
      }

      if(turnMan == 1){
        playerTurnGo.SetActive(true);
        enemyTurnGo.SetActive(false);
      }
      if(turnMan == 2){
        playerTurnGo.SetActive(false);
        enemyTurnGo.SetActive(true);
      }

      if((ActiveMeal == 1 && meal1Cooked) || (ActiveMeal == 2 && meal2Cooked) || (ActiveMeal == 3 && meal3Cooked)){
        CookedDisplay.SetActive(true);
      } else { CookedDisplay.SetActive(false); }
    }

    public void AssignCharacters(string IAmChar){
      //player characters
      if(IAmChar == "Play1"){
        int whichChar = Random.Range(0, CharacterDeckScript.current.allCharacters.Count);
        PlayerChar1 = CharacterDeckScript.current.allCharacters[whichChar];
        CharacterDeckScript.current.allCharacters.Remove(CharacterDeckScript.current.allCharacters[whichChar]);
      }

      else if(IAmChar == "Play2"){
        int whichChar = Random.Range(0, CharacterDeckScript.current.allCharacters.Count);
        PlayerChar2 = CharacterDeckScript.current.allCharacters[whichChar];
        CharacterDeckScript.current.allCharacters.Remove(CharacterDeckScript.current.allCharacters[whichChar]);
      }

      else if(IAmChar == "Play3"){
        int whichChar = Random.Range(0, CharacterDeckScript.current.allCharacters.Count);
        PlayerChar3 = CharacterDeckScript.current.allCharacters[whichChar];
        CharacterDeckScript.current.allCharacters.Remove(CharacterDeckScript.current.allCharacters[whichChar]);
      }

      //enemy characters
      else if(IAmChar == "Enem1"){
        int whichChar = Random.Range(0, CharacterDeckScript.current.allCharacters.Count);
        EnemyChar1 = CharacterDeckScript.current.allCharacters[whichChar];
        CharacterDeckScript.current.allCharacters.Remove(CharacterDeckScript.current.allCharacters[whichChar]);
      }

      else if(IAmChar == "Enem2"){
        int whichChar = Random.Range(0, CharacterDeckScript.current.allCharacters.Count);
        EnemyChar2 = CharacterDeckScript.current.allCharacters[whichChar];
        CharacterDeckScript.current.allCharacters.Remove(CharacterDeckScript.current.allCharacters[whichChar]);
      }

      else if(IAmChar == "Enem3"){
        int whichChar = Random.Range(0, CharacterDeckScript.current.allCharacters.Count);
        EnemyChar3 = CharacterDeckScript.current.allCharacters[whichChar];
        CharacterDeckScript.current.allCharacters.Remove(CharacterDeckScript.current.allCharacters[whichChar]);
      }
    }

    IEnumerator forSomeTime(){
      yield return new WaitForSeconds(0.2f);
      if(IngrToChooseFrom.Count == 0){
        canSpawnIngr = false;
        StartCoroutine(spawn());
      } else { EnemyCanPlay = true; }

      checkingTime = false;
    }

    IEnumerator StirPot(){
      MainIngredient.transform.position = GM.current.potSlots[3].transform.position;
      yield return new WaitForSeconds(0.5f);
      canBeStirred = true;
    }

    void SwitchInventory(){
      playerInPot = !playerInPot;
      if(playerInPot){
        playerContr.transform.position = potSlots[0].transform.position;
      }
      if(!playerInPot){
        playerContr.transform.position = StartLoctionInSlots.transform.position;
      }
    }

    void UpdateCookingPot(){
      //turn off all lists
      for(int i = 0; i < meal1.Count; i++){
          meal1[i].SetActive(false);
      }
      for(int i = 0; i < meal2.Count; i++){
          meal2[i].SetActive(false);
      }
      for(int i = 0; i < meal3.Count; i++){
          meal3[i].SetActive(false);
      }
      //turn on specific one
      if(ActiveMeal == 1){
        activeMealDisplay.text = "I";
        for(int i = 0; i < meal1.Count; i++){
            meal1[i].SetActive(true);
        }
      }
      if(ActiveMeal == 2){
        activeMealDisplay.text = "II";
        for(int i = 0; i < meal2.Count; i++){
            meal2[i].SetActive(true);
        }
      }
      if(ActiveMeal == 3){
        activeMealDisplay.text = "III";
        for(int i = 0; i < meal3.Count; i++){
            meal3[i].SetActive(true);
        }
      }
    }


    IEnumerator SpawnIngredientSlotMAchine(){
      foreach(GameObject tile in tiles){
        yield return new WaitForSeconds(0.1f);
        Instantiate(IngredientTokenPrefab);
        IngredientTokenPrefab.transform.position = tile.transform.position;
      }
    }

    IEnumerator spawn(){
      PC.current.AxisInUse = true;
      playerContr.SetActive(false);
      yield return new WaitForSeconds(0.2f);
      spawnLeft();
      yield return new WaitForSeconds(0.2f);
      spawnCenter();
      yield return new WaitForSeconds(0.2f);
      spawnRight();
      yield return new WaitForSeconds(0.2f);
      spawnLeft();
      yield return new WaitForSeconds(0.2f);
      spawnCenter();
      yield return new WaitForSeconds(0.2f);
      spawnRight();
      yield return new WaitForSeconds(0.2f);
      spawnLeft();
      yield return new WaitForSeconds(0.2f);
      spawnCenter();
      yield return new WaitForSeconds(0.2f);
      spawnRight();
      canSpawnIngr = true;
      PC.current.AxisInUse = false;
      playerContr.SetActive(true);

      if(turnMan == 2){
        StartCoroutine(EnemyTurnAi());
        EnemyCanPlay = true;
      }
    }

    void spawnRight(){
      IngredientTokenPrefab.transform.position = spawnPointRight.transform.position;
      Instantiate(IngredientTokenPrefab);
    }
    void spawnCenter(){
      IngredientTokenPrefab.transform.position = spawnPointCenter.transform.position;
      Instantiate(IngredientTokenPrefab);
    }
    void spawnLeft(){
      IngredientTokenPrefab.transform.position = spawnPointLeft.transform.position;
      Instantiate(IngredientTokenPrefab);
    }

    public void SwitchTurn(){
      if(turnMan == 1){
        turnMan = 2;
        playerContr.GetComponent<SpriteRenderer>().enabled = false;
        if(EnemyCanPlay = true){
          StartCoroutine(EnemyTurnAi());
        }
      }
      else if(turnMan == 2){
        turnMan = 1;
        playerContr.GetComponent<SpriteRenderer>().enabled = true;
      }
    }

    IEnumerator spawnPlayerCharacters(){
      yield return new WaitForSeconds(0.1f);
      //spawn Players from player Deck
      for(int i = 0; i < CharacterDeckScript.current.playerDeck.Count; i++){
          Instantiate(PlayerTokenPrefab);
          PlayerTokenPrefab.transform.position = playerCharLocations[i].transform.position;
          PlayerShortCutDisplays[i].SetActive(true);
        }
      }

    IEnumerator EnemyTurnAi(){
          yield return new WaitForSeconds(1.2f);

          if(IngrToChooseFrom.Count != 0){
            // which ingredient does Ai choose
            int whichIngrAi = Random.Range(0, IngrToChooseFrom.Count);

            //add ingredient to pot
            if(AiMeal1.Count < 3){
              AiMeal1.Add(IngrToChooseFrom[whichIngrAi]);
              IngrToChooseFrom[whichIngrAi].transform.position = EnemyPot.transform.position;
            }

            else if(AiMeal2.Count < 3){
              AiMeal2.Add(IngrToChooseFrom[whichIngrAi]);
              IngrToChooseFrom[whichIngrAi].transform.position = EnemyPot.transform.position;
            }

            else if(AiMeal3.Count < 3){
              AiMeal3.Add(IngrToChooseFrom[whichIngrAi]);
              IngrToChooseFrom[whichIngrAi].transform.position = EnemyPot.transform.position;
            }

            //end turn
            yield return new WaitForSeconds(0.5f);
            GM.current.SwitchTurn();
          }
    }

    public void battleModeActivate(){
      CombatScene.transform.position = EnemyLocationInBattle.position;
      SlotMachineUi.SetActive(false);
      SlotMachineGo.SetActive(false);

      canSpawnIngr = false;

      combatTurnNum = 0;
      characterTurns.Add(play1BattleCard.GetComponent<CharacterCardScript>()); characterTurns.Add(play2BattleCard.GetComponent<CharacterCardScript>()); characterTurns.Add(play3BattleCard.GetComponent<CharacterCardScript>()); characterTurns.Add(enem1BattleCard.GetComponent<CharacterCardScript>()); characterTurns.Add(enem2BattleCard.GetComponent<CharacterCardScript>()); characterTurns.Add(enem3BattleCard.GetComponent<CharacterCardScript>());
      for (int i = 0; i < 6; i++) {
        int randCharGen = Random.Range(0, characterTurns.Count);
        characterTurnsReshuffel.Add(characterTurns[randCharGen]);

        if(characterTurns[randCharGen].IAmChar == "Play1" || characterTurns[randCharGen].IAmChar == "Play2" || characterTurns[randCharGen].IAmChar == "Play3"){
          charTurnDisplays[i].color = new Color32(223, 254, 179, 255);;
          charTurnDisplays[i].text = characterTurns[randCharGen].CharacterInfoScript.name;
        }

        else if(characterTurns[randCharGen].IAmChar == "Enem1" || characterTurns[randCharGen].IAmChar == "Enem2" || characterTurns[randCharGen].IAmChar == "Enem3"){
          charTurnDisplays[i].color = new Color32(224, 99, 75, 255);;
          charTurnDisplays[i].text = characterTurns[randCharGen].CharacterInfoScript.name;
        }

        characterTurns[randCharGen].turnInCombat = i+1;
        characterTurns.Remove(characterTurns[randCharGen]);
      }

      battleMode = true;
      updateBattleTurn();
    }

    public void updateBattleTurn(){
      for (int i = 0; i < ActiveTurnDisplays.Count; i++) {
        ActiveTurnDisplays[i].GetComponent<Image>().enabled = false;
      }
      combatTurnNum ++;
      if(combatTurnNum < 7){
        ActiveTurnDisplays[combatTurnNum-1].GetComponent<Image>().enabled = true;
      } else { battleModeDeactivate(); }
      charAttacking = false;
    }

    void battleModeDeactivate(){
      battleMode = false;
      combatTurnNum = 0;
      characterTurnsReshuffel.Clear();

      for (int i = 0; i < charTurnDisplays.Count; i++) {
        charTurnDisplays[i].text = null;
      }

      CombatScene.transform.position = prevEnemyLoc.position;
      SlotMachineUi.SetActive(true);
      SlotMachineGo.SetActive(true);

      meal1.Clear();
      meal2.Clear();
      meal3.Clear();
      meal1Cooked = false;
      meal2Cooked = false;
      meal3Cooked = false;
      Player1Eaten = false;
      Player2Eaten = false;
      Player3Eaten = false;

      AiMeal1.Clear();
      AiMeal2.Clear();
      AiMeal3.Clear();

      play1BattleCard.GetComponent<Button>().interactable = true;
      play2BattleCard.GetComponent<Button>().interactable = true;
      play3BattleCard.GetComponent<Button>().interactable = true;

      StartCoroutine(spawn());
      UpdateCookingPot();
      SwitchInventory();
      if(playerInPot){
        SwitchInventory();
      }
      PC.current.ResetInfo();
      CookBookScript.current.StartCoroutine(CookBookScript.current.allowMovement());
    }
}
