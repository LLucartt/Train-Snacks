using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public bool selected;
    GameMaster gm;
    public int tileSpeed;
    public bool hasMoved;
    public float moveSpeed;

    private void Start(){
      gm = GameMaster.current;
    }

    private void OnMouseDown(){
      if(selected == true){
          selected = false;
          gm.selectedUnit = null;
          gm.ResetTiles();
      } else{
          if(gm.selectedUnit != null){
            gm.selectedUnit.selected = false;
          }

          selected = true;
          gm.selectedUnit = this;

          gm.ResetTiles();
          GetWalkableTiles();
      }
    }

    void GetWalkableTiles(){
      if(hasMoved == true){
        return;
      }

      foreach(GameObject tile in gm.tiles){
        if(Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed){
          if(tile.GetComponent<TileScript>().isClear() == true){
            tile.GetComponent<TileScript>().Highlight();
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
    }

}
