using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    [Header("Movement")]
    [HideInInspector] public bool selected;
    public bool hasMoved;
    private float moveSpeed = 10;
    public CharacterInfoScript characterInfo;

    GameMaster gm;
    PlayerControllerScript pc;
    AttackListScript al;

    //public bool playerDecide;

    [Header("Attack")]
    List<EnemyScript> enemiesInRange = new List<EnemyScript>();
    private GameObject weaponIcon, AimedAttackTile;
    public bool hasAttacked;
    private int currentAttackTileNumber;

    void Start(){
      gm = GameMaster.current;
      pc = PlayerControllerScript.current;
      al = AttackListScript.current;
      InstantiateCharacterInfo();
    }

    void InstantiateCharacterInfo(){
      characterInfo = CharacterDeckScript.current.playerDeck[gm.characterInstantiated];
      gm.characterInstantiated ++;
    }

    void Update(){
      if(!pc.inventoryOpen){
        if(Input.GetButtonDown("Submit") && pc.selectedCharacter == this.gameObject && !pc.chooseHungryChar){
          selectedUnit();
        }

        if(selected && Input.GetButtonDown("Cancel")){
          UnselectUnit();
        }

        if(Input.GetButtonDown("Cancel") && pc.chooseHungryChar){
          pc.instantInv();
          ResetFeedingSes();
        }

        if(Input.GetButtonDown("Submit") && pc.selectedCharacter == this.gameObject && pc.chooseHungryChar){
          if(pc.attackTypeUnit == "Single" && pc.attackTiles != null){
            pc.takeAim = true;
          }
        }

        if(pc.takeAim == true && pc.selectedTile.GetComponent<TileScript>().readyForAttack){
            GetAffectedTiles();

          if(Input.GetButtonDown("Submit")){
            feedCharacter();
          }
        }

      }

    }

    void feedCharacter(){
      Debug.Log("Attack!!"); //call attacklist and do which ever attack the ingredient has attached to it
      al.DecideEffects();
      pc.feedingFood.RemoveFromInventory();
      ResetFeedingSes();
    }

    void UnselectUnit(){
      if(selected == true){
          selected = false;
          gm.selectedUnit = null;
          gm.ResetTiles();
          pc.attackTiles.Clear();
      }
    }

    void selectedUnit(){
      //resetWeaponIcons();

      if(selected == true){
          selected = false;
          gm.ResetTiles();
          gm.selectedUnit = null;
      } else{
          if(gm.selectedUnit != null){
            gm.selectedUnit.selected = false;
          }

          selected = true;
          gm.selectedUnit = this;

          gm.ResetTiles();
          pc.attackTiles.Clear();
          GetWalkableTiles();
          //playerDecide = true;
      }
    }

    void GetWalkableTiles(){
      if(hasMoved == true){
        return;
      }

      foreach(GameObject tile in gm.tiles){
        if(Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= characterInfo.stamina){
          if(tile.GetComponent<TileScript>().isClear() == true){
            tile.GetComponent<TileScript>().Highlight();
          }
        }
      }
    }

    public void GetCombatTiles(){ //preperng for battle; when hovering over unit with food
      pc.attackTiles.Clear();
      pc.planAttack = true;

      foreach(GameObject tile in gm.tiles){
        //attack only horizontal
        if(pc.attackDirectionUnit == "x" &&  tile != pc.selectedTile){
          if((Mathf.Abs(transform.position.x - tile.transform.position.x) <= pc.attackRangeUnit && Mathf.Abs(transform.position.y - tile.transform.position.y) == 0)){
              tile.GetComponent<TileScript>().Highlight();
              pc.attackTiles.Add(tile);
          }
        }
        //attack only Vertical
        if(pc.attackDirectionUnit == "y" &&  tile != pc.selectedTile){
          if((Mathf.Abs(transform.position.y - tile.transform.position.y) <= pc.attackRangeUnit && Mathf.Abs(transform.position.x - tile.transform.position.x) == 0)){
              tile.GetComponent<TileScript>().Highlight();
              pc.attackTiles.Add(tile);
          }
        }
        //attack horizontal & Vertical in a straight line
        if(pc.attackDirectionUnit == "xy" &&  tile != pc.selectedTile){
          if((Mathf.Abs(transform.position.x - tile.transform.position.x) <= pc.attackRangeUnit && Mathf.Abs(transform.position.y - tile.transform.position.y) == 0) ||
                (Mathf.Abs(transform.position.y - tile.transform.position.y) <= pc.attackRangeUnit && Mathf.Abs(transform.position.x - tile.transform.position.x) == 0)){
              tile.GetComponent<TileScript>().Highlight();
              pc.attackTiles.Add(tile);
          }
        }

        //attack in circular radius
        if(pc.attackDirectionUnit == "o" &&  tile != pc.selectedTile){
          if((Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y)) <= pc.attackRangeUnit){
              tile.GetComponent<TileScript>().Highlight();
              pc.attackTiles.Add(tile);
          }
        }

      }
    }

    public void Move(Vector2 tilePos){
      gm.ResetTiles();
      StartCoroutine(StartMovement(tilePos));
    }

    IEnumerator StartMovement(Vector2 tilePos){
      while(transform.position.x != tilePos.x){
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x, transform.position.y), moveSpeed * Time.deltaTime);
        yield return null;
      }
      while(transform.position.y != tilePos.y){
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), moveSpeed * Time.deltaTime);
        yield return null;
      }

      hasMoved = true;
      UnselectUnit();
    }

      void ResetFeedingSes(){
        pc.chooseHungryChar = false;
        pc.feedingFood = null;
        pc.takeAim = false;
        pc.planAttack = false;
        pc.attackTiles.Clear();
        gm.ResetTiles();
      }

      void GetAffectedTiles(){
        pc.AffectedTiles.Clear();
        if(pc.attackTiles == null ){
          return;
        }

          if(pc.attackTypeUnit == "Single"){
            pc.AffectedTiles.Add(pc.selectedTile);
          }
          if(pc.attackTypeUnit == "yLine"){
            foreach (GameObject tile in pc.attackTiles) {
              pc.AffectedTiles.Add(tile);
            }
          }

          foreach (GameObject tile in pc.AffectedTiles) {
            tile.GetComponent<TileScript>().HighlightForAttack();
          }
      }

}
