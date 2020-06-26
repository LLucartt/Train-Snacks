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
    /*
    public EnemyScript[] enemies;
    public float numberEnemies;
    public float Ingredients;
    public GameObject Enemy;*/
    public GameObject PlayerTokenPrefab;
    public GameObject EnemyTokenPrefab;
    public GameObject IngredientTokenPrefab;
    public GameObject ObstacleTokenPrefab;
    public GameObject[] PlayerShortCutDisplays;
    [HideInInspector] public int characterInstantiated = 0;
    public FoodObjectScript[] IngredientVariety;
    [SerializeField] int minNumIngr;
    [SerializeField] int maxNumIngr;
    [SerializeField] int minNumobstc;
    [SerializeField] int maxNumobstc;
    private int randomAmountOfIngr;
    private int randomAmountOfObstc;

    [HideInInspector] public bool submitButInUse;

    public void ResetTiles(){
      foreach(GameObject tile in tiles){
        tile.GetComponent<TileScript>().Reset();
      }
    }

    void Update(){
      if(Input.GetButtonDown("Submit")){
        submitButInUse = true;
      }
      if(Input.GetButtonUp("Submit")){
        submitButInUse = false;
      }
    }

    void Start(){
      for(int i = 0; i < PlayerShortCutDisplays.Length; i++){
        PlayerShortCutDisplays[i].SetActive(false);
      }
      StartCoroutine(GenerateLevel());
    }

    IEnumerator GenerateLevel(){
      //spawn random amount of ingredients
      yield return new WaitForSeconds(0.1f);
      for(int i = 0; i < 9; i++){
        GameObject randomTile = tiles[Random.Range(0, tiles.Length)];
        if(randomTile.GetComponent<TileScript>().isFreeToSpawn() == true){
          Instantiate(IngredientTokenPrefab);
          IngredientTokenPrefab.transform.position = randomTile.transform.position;
        } else {i--;}
      }

      /*
      yield return new WaitForSeconds(0.1f);
      //spawn Players from player Deck
      for(int i = 0; i < CharacterDeckScript.current.playerDeck.Count; i++){
        GameObject randomTile = tiles[Random.Range(0, tiles.Length)];
        if(randomTile.GetComponent<TileScript>().isFreeToSpawn() == true){
          Instantiate(PlayerTokenPrefab);
          PlayerTokenPrefab.transform.position = randomTile.transform.position;
          PlayerShortCutDisplays[i].SetActive(true);
        } else {i--;}
      }
      //spawn the same amount of enemies as there are PlayerSendFrameComplete
      for(int i = 0; i < CharacterDeckScript.current.playerDeck.Count; i++){
        GameObject randomTile = tiles[Random.Range(0, tiles.Length)];
        if(randomTile.GetComponent<TileScript>().isFreeToSpawn() == true){
          Instantiate(EnemyTokenPrefab);
          EnemyTokenPrefab.transform.position = randomTile.transform.position;
        } else {i--;}
      }
      //spawn random amount of ingredients
      randomAmountOfIngr = Random.Range(minNumIngr, maxNumIngr);
      for(int i = 0; i < randomAmountOfIngr; i++){
        GameObject randomTile = tiles[Random.Range(0, tiles.Length)];
        if(randomTile.GetComponent<TileScript>().isFreeToSpawn() == true){
          Instantiate(IngredientTokenPrefab);
          IngredientTokenPrefab.transform.position = randomTile.transform.position;
        } else {i--;}
      }
      //spawn random amount of obstacles
      randomAmountOfObstc = Random.Range(minNumobstc, maxNumobstc);
      for(int i = 0; i < randomAmountOfIngr; i++){
        GameObject randomTile = tiles[Random.Range(0, tiles.Length)];
        if(randomTile.GetComponent<TileScript>().isFreeToSpawn() == true){
          Instantiate(ObstacleTokenPrefab);
          ObstacleTokenPrefab.transform.position = randomTile.transform.position;
        } else {i--;}
      }*/
    }
}
