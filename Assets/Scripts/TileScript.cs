using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

    private SpriteRenderer sr;

    [Header("Tiles")]
    public Sprite[] tileGraphics;
    public float hoverAmount;
    public LayerMask obstacleLayer;
    //[HideInInspector] public bool isSelectedTile;
    //private bool isSelected;
    //public bool isStartTile;

    [Header("Highlight")]
    public Color highlightedColor;
    public Color highlightedAttackColor;
    public bool isWalkable, readyForAttack;
    public bool isClearBool = true;

    GameMaster gm;
    PlayerControllerScript pc;

    private void Start(){
      // randomises tile pattern
      sr = GetComponent<SpriteRenderer>();
      int randTile = Random.Range(0, tileGraphics.Length);
      sr.sprite = tileGraphics[randTile];

      gm = GameMaster.current;
      pc = PlayerControllerScript.current;
    }

    void Update(){
      if(pc.selectedTile.GetComponent<TileScript>().isWalkable && gm.selectedUnit != null && Input.GetButtonDown("Submit") && pc.selectedCharacter == null){
        gm.selectedUnit.Move(pc.selectedTile.transform.position);
      }
      if(pc.selectedTile.GetComponent<TileScript>().isWalkable && Input.GetButtonDown("Submit") && pc.feedingFood != null){
        //pc.foodAttack(pc.selectedTile.transform.position);
      }
    }
    /*
    private void OnMouseEnter(){
      ActiveTile();
    }

    private void OnMouseExit(){
      ReturnTile();
    }
    */
    private void ActiveTile(){
      transform.localScale += Vector3.one * hoverAmount;
    }

    private void ReturnTile(){
      transform.localScale -= Vector3.one * hoverAmount;
    }

    public bool isClear(){
      Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer); //location, radius, recognise what objects
      if(obstacle != null){
          return false;
          isClearBool = false;
      } else{
          return true;
          isClearBool = true;
      }
    }

    public void Highlight(){
      sr.color = highlightedColor;
      if(!pc.planAttack){
        isWalkable = true;
      }
      else if(pc.planAttack){
        readyForAttack = true;
      }
    }
    public void HighlightForAttack(){
      sr.color = highlightedAttackColor;
    }
    public void Reset(){
      sr.color = Color.white;
      isWalkable = false;
      readyForAttack = false;
    }

    void OnTriggerExit2D (Collider2D col){
      if(sr.color == highlightedAttackColor){
        sr.color = highlightedColor;
      }
    }
/*
    private void OnMouseDown(){
      if(isWalkable && gm.selectedUnit != null){
        gm.selectedUnit.Move(this.transform.position);
      }
    }*/

}
