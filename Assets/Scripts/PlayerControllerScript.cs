using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerControllerScript : MonoBehaviour
{
    public static PlayerControllerScript current;
    private void Awake(){current = this;}

    [Header("Controller Settings")]
    [HideInInspector] public bool mouseInput;
    private bool AxisInUse;
    private float controlDelay = 0.15f;
    GameMaster gm;

    [Header("Tile Checker")]
    [HideInInspector] public GameObject selectedTile;
    public GameObject selectedCharacter;
    public GameObject selectedIngredient;
    [HideInInspector] public GameObject LeftCheck = null, RightCheck  = null, UpCheck = null, DownCheck = null;

    [Header("UI")]
    public TextMeshProUGUI objectNameDisplay;
    [SerializeField] GameObject firstSlotMealInventory = null;
    [SerializeField] GameObject firstSlotIngrInventory = null;
    [HideInInspector] public bool inventoryOpen = false;
    private bool mealsInvOpen = true;
    [SerializeField] GameObject mealInvGo = null;
    [SerializeField] GameObject ingrInvGo = null;

    [Header("Attack/ feeding")]
    public int attackRangeUnit;
    public string attackDirectionUnit;
    public string attackTypeUnit;
    [HideInInspector] public bool chooseHungryChar, planAttack, takeAim;
    [HideInInspector] public IngedientObjectScript feedingFood;



    void Start(){
      mealInvGo.SetActive(true);
      ingrInvGo.SetActive(false);
      gm = GameMaster.current;
    }

    void Update(){/*
      if(selectedCharacter != null && objectNameDisplay.text == null){
        DisplayCharacterInfo();
        Debug.Log("character select");
        if(chooseHungryChar && !planAttack){
          selectedCharacter.GetComponent<UnitScript>().GetCombatTiles();
        }
      }*/

      if(chooseHungryChar && !planAttack && selectedCharacter != null){
        selectedCharacter.GetComponent<UnitScript>().GetCombatTiles();
      }

      if(Input.GetButtonDown("Inventory") && gm.selectedUnit == null){
        instantInv();
      }
      if(Input.GetButtonDown("Cancel") && inventoryOpen){
        instantInv();
      }
      if(Input.GetButtonDown("Tab") && !takeAim){
        SwitchInventory();
      }
    }
    void FixedUpdate(){
      //decides if the navigation is mouse or keyboard/controller controlled
      if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0){
        mouseInput = true;
      }
      if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
        mouseInput = false;
      }

      //if player is playing with controller/keyboard
      if(!mouseInput && !inventoryOpen){
        if((Input.GetAxisRaw("Horizontal")) > 0f && !AxisInUse && RightCheck != null){
          StartCoroutine(MoveRight());
        }
        else if((Input.GetAxisRaw("Horizontal")) < 0f && !AxisInUse && LeftCheck != null){
          StartCoroutine(MoveLeft());
        }
        if((Input.GetAxisRaw("Vertical")) > 0f && !AxisInUse && UpCheck != null){
          StartCoroutine(MoveUp());
        }
        else if((Input.GetAxisRaw("Vertical")) < 0f && !AxisInUse && DownCheck != null){
          StartCoroutine(MoveDown());
        }
      }
    }

    IEnumerator MoveRight(){
      AxisInUse = true;
      transform.position += new Vector3(1, 0, 0);
      yield return new WaitForSeconds(controlDelay);
      AxisInUse = false;
    }
    IEnumerator MoveLeft(){
      AxisInUse = true;
      transform.position -= new Vector3(1, 0, 0);
      yield return new WaitForSeconds(controlDelay);
      AxisInUse = false;
    }
    IEnumerator MoveUp(){
      AxisInUse = true;
      transform.position += new Vector3(0, 1, 0);
      yield return new WaitForSeconds(controlDelay);
      AxisInUse = false;
    }
    IEnumerator MoveDown(){
      AxisInUse = true;
      transform.position -= new Vector3(0, 1, 0);
      yield return new WaitForSeconds(controlDelay);
      AxisInUse = false;
    }

    public void DisplayCharacterInfo(){
      objectNameDisplay.text = selectedCharacter.GetComponent<UnitScript>().characterInfo.name;
    }

    public void DisplayFoodInfo(){
      objectNameDisplay.text = selectedIngredient.GetComponent<PickUpIngredientScript>().ingredient.foodName;
    }

    public void ResetInfo(){
      objectNameDisplay.text = "";
    }

    public void instantInv(){
    if(!inventoryOpen && mealsInvOpen){
      EventSystem.current.SetSelectedGameObject(firstSlotMealInventory);
    }
    else if (!inventoryOpen && !mealsInvOpen){
      EventSystem.current.SetSelectedGameObject(firstSlotIngrInventory);
    }
    else if(inventoryOpen){
      EventSystem.current.SetSelectedGameObject(null);
    }
    inventoryOpen = !inventoryOpen;
  }

  void SwitchInventory(){
    mealsInvOpen = !mealsInvOpen;
    mealInvGo.SetActive(mealsInvOpen);
    ingrInvGo.SetActive(!mealsInvOpen);
    inventoryOpen = !inventoryOpen;
    instantInv();
  }

  public void SwitchInventoryDelay(){
    StartCoroutine(inventoryDelay());
  }

  IEnumerator inventoryDelay(){
    yield return new WaitForSeconds(0.2f);
    instantInv();
  }

//  public void foodAttack(Vector2 tilePos){
    //if attack type is single tile do damage to any enemies that are on this tile\
    //if(attackTypeUnit == "Single"){
    //  Instantiate(attackBox, tilePos);
  //  }

    // if attack is a beam effect
  //  GameMaster.current.ResetTiles();
//  }
}
