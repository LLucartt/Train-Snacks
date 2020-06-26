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
    PC pc;

    void Start(){
      pc = PC.current;
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
/*
      if(col.gameObject.tag == "Ingredient"){
        if(main){
          pc.selectedIngredient = col.gameObject;
          pc.DisplayFoodInfo();
        }
      }*/

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

    void OnTriggerStay2D (Collider2D col){
      if(col.gameObject.tag == "Ingredient"){
        if(main){
          pc.selectedIngredient = col.gameObject;
          pc.DisplayFoodInfo();
        }
      }
    }

    void OnTriggerExit2D (Collider2D col){
      if(col.gameObject.tag == "Ingredient"){
        if(main){
          pc.ResetInfo();
          pc.selectedIngredient = null;
        }
      }
    }


}
