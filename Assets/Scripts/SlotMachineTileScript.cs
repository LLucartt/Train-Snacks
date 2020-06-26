using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineTileScript : MonoBehaviour
{
    public LayerMask ingredientLayer;

    public bool isFreeToSpawn(){
      Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, ingredientLayer);
      if(obstacle != null){
          return false;
      } else{
          return true;
      }
    }
}
