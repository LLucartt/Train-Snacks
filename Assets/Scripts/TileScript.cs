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

    [Header("Highlight")]
    public Color highlightedColor;
    public bool isWalkable;
    public bool isClearBool = true;

    GameMaster gm;

    private void Start(){
      sr = GetComponent<SpriteRenderer>();
      int randTile = Random.Range(0, tileGraphics.Length);
      sr.sprite = tileGraphics[randTile];

      gm = GameMaster.current;
    }

    void Update(){/*
      Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
      if(obstacle != null){
          isClearBool = false;
      } else{
          isClearBool = true;
      }*/
    }

    private void OnMouseEnter(){
      IncreaseTileSize();
    }

    private void OnMouseExit(){
      ReturnTileSize();
    }

    private void IncreaseTileSize(){
      transform.localScale += Vector3.one * hoverAmount;
    }

    private void ReturnTileSize(){
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
      isWalkable = true;
    }
    public void Reset(){
      sr.color = Color.white;
      isWalkable = false;
    }

    private void OnMouseDown(){
      if(isWalkable && gm.selectedUnit != null){
        gm.selectedUnit.Move(this.transform.position);
      }
    }

}
