using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackListScript : MonoBehaviour
{
  public static AttackListScript current;
  private void Awake(){current = this;}

  GameMaster gm;
  PlayerControllerScript pc;

  [Header("Layers")]
  public LayerMask enemyLayer;
  public LayerMask playerLayer;
  public LayerMask ingredientLayer;
  public LayerMask obstacleLayer;

  void Start(){
    gm = GameMaster.current;
    pc = PlayerControllerScript.current;
  }

  public void DecideEffects(){
    for (int i = 0; i < pc.feedingFood.attackEffects.Count; i++) {
      if(pc.feedingFood.attackEffects[i] == "Damage"){
        AttackDamage();
      }
    }
  }

  public void AttackDamage(){
    foreach (GameObject tile in pc.AffectedTiles) {
      Collider2D obstacle = Physics2D.OverlapCircle(tile.transform.position, 0.2f, enemyLayer); //location, radius, recognise what objects
      if(obstacle != null){
          //do damage to this enemy
          Debug.Log(obstacle);
      }
    }
  }

}
