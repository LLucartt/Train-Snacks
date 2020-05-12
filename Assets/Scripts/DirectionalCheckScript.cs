using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalCheckScript : MonoBehaviour
{
    [SerializeField] bool main = false;
    [SerializeField] bool left = false;
    [SerializeField] bool right = false;
    [SerializeField] bool up = false;
    [SerializeField] bool down = false;
    PlayerControllerScript pc;
    GameMaster gm;

    void Start(){
      pc = PlayerControllerScript.current;
      gm = GameMaster.current;
    }

    void FixedUpdate(){
    }

    void OnTriggerEnter2D (Collider2D col){

      if(col.gameObject.tag == "Tile"){
        if(main){
          pc.selectedTile = col.gameObject;
        }
        if(right){
          pc.RightCheck = col.gameObject;
        }
        if(left){
          pc.LeftCheck = col.gameObject;
        }
        if(up){
          pc.UpCheck = col.gameObject;
        }
        if(down){
          pc.DownCheck = col.gameObject;
        }
      }

      if(col.gameObject.tag == "Player"){
        if(main){
          pc.selectedCharacter = col.gameObject;
          pc.DisplayCharacterInfo();
        }
      }


      if(col.gameObject.tag == "Ingredient"){
        if(main){
          pc.selectedIngredient = col.gameObject;
          pc.DisplayFoodInfo();
        }
      }

      if(col.gameObject.tag == "Wall"){
          if(right){
            pc.RightCheck = null;
          }
          if(left){
            pc.LeftCheck = null;
          }
          if(up){
            pc.UpCheck = null;
          }
          if(down){
            pc.DownCheck = null;
          }
        }
    }

    void OnTriggerExit2D (Collider2D col){
      if(main && col.gameObject.tag == "Player"){
        if(!col.gameObject.GetComponent<UnitScript>().selected){
          pc.ResetInfo();
        }
        if(pc.chooseHungryChar && !pc.takeAim){
          pc.planAttack = false;
          gm.ResetTiles();
        }

        pc.selectedCharacter = null;
      }

      if(main && col.gameObject.tag == "Ingredient"){
        pc.selectedIngredient = null;
        pc.ResetInfo();
      }
    }


}
