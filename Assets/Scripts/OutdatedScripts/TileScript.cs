using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

    private SpriteRenderer sr;

    [Header("Tiles")]
    public Sprite[] tileGraphics;
    public float hoverAmount;

    [Header("Highlight")]
    public Color originalColour;
    public Color highlightedColor;
    public Color highlightedAttackColor;
    public bool isWalkable, readyForAttack;

    GameMaster gm;
    PlayerControllerScript pc;
    AttackListScript al;

    private void Start(){
      // randomises tile pattern
      sr = GetComponent<SpriteRenderer>();
      int randTile = Random.Range(0, tileGraphics.Length);
      sr.sprite = tileGraphics[randTile];

      gm = GameMaster.current;
      pc = PlayerControllerScript.current;
      al = AttackListScript.current;
    }

    void Update(){
      if(pc.selectedTile.GetComponent<TileScript>().isWalkable && gm.selectedUnit != null && Input.GetButtonDown("Submit") && pc.selectedCharacter == null){
        gm.selectedUnit.Move(pc.selectedTile.transform.position);
      }
    }

    private void ActiveTile(){
      transform.localScale += Vector3.one * hoverAmount;
    }

    private void ReturnTile(){
      transform.localScale -= Vector3.one * hoverAmount;
    }

    public bool isFreeToSpawn(){
      Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, al.ingredientLayer);
      if(obstacle != null){
          return false;
      } else{
          return true;
      }
    }

    public bool isClear(){ //used when unit needs to move won't check for overlap with ingredients
      Collider2D obstacle1 = Physics2D.OverlapCircle(transform.position, 0.2f, al.obstacleLayer); //location, radius, recognise what objects
      Collider2D obstacle2 = Physics2D.OverlapCircle(transform.position, 0.2f, al.playerLayer);
      Collider2D obstacle3 = Physics2D.OverlapCircle(transform.position, 0.2f, al.enemyLayer);
      if(obstacle1 != null || obstacle2 != null || obstacle3 != null){
          return false;
      } else{
          return true;
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
      sr.color = originalColour;
      isWalkable = false;
      readyForAttack = false;
      pc.attackTiles.Clear();
      pc.AffectedTiles.Clear();
    }

    void OnTriggerExit2D (Collider2D col){
      if(sr.color == highlightedAttackColor){
        sr.color = highlightedColor;
        pc.AffectedTiles.Clear();
      }
    }
/*
    private void OnMouseDown(){
      if(isWalkable && gm.selectedUnit != null){
        gm.selectedUnit.Move(this.transform.position);
      }
    }*/

}
