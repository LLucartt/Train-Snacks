using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster current;
    private void Awake(){current = this;}

    [Header("Tile Settings")]
    public GameObject[] tiles;
    [HideInInspector] public UnitScript selectedUnit;

    [Header("Game Generator")]
    public float numberEnemies;
    public float Ingredients;
    public GameObject Enemy;

    public void ResetTiles(){
      foreach(GameObject tile in tiles){
        tile.GetComponent<TileScript>().Reset();
      }
    }

    void Start(){
      //GenerateMap();
    }

    public void ClearMap(){
      EnemyScript[] enemies = (EnemyScript[])FindObjectsOfType<EnemyScript>();
      foreach (EnemyScript enemy in enemies){
        Destroy(enemy.gameObject);
      }
    }

    public void GenerateMap(){/*
      numberEnemies = Random.Range(44, 45);
      Ingredients = Random.Range(3, 8);

      for (float enemyNumber = 1; enemyNumber <= numberEnemies;) {
        GameObject randomTile = tiles[Random.Range(0, tiles.Length)];
        if(randomTile.GetComponent<TileScript>().isClearBool == true){
          Instantiate(Enemy);
          Enemy.transform.position = randomTile.transform.position;
          Debug.Log(Enemy.name + randomTile.name);
          enemyNumber ++;
        }
      }*/
    }
}
