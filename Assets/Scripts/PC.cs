using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PC : MonoBehaviour
{

  public static PC current;
  private void Awake(){current = this;}

  [Header("Controller Settings")]
  [HideInInspector] public bool mouseInput;
  public bool AxisInUse;
  private float controlDelay = 0.15f;
  //GameMaster gm;

  [Header("Tile Checker")]
  [HideInInspector] public GameObject selectedTile;
  [HideInInspector] public GameObject selectedIngredient;
  [HideInInspector] public GameObject LeftCheck = null, RightCheck  = null, UpCheck = null, DownCheck = null;

  [Header("Food Display UI")]
  [SerializeField] TextMeshProUGUI FoodName;
  [SerializeField] TextMeshProUGUI FoodDescription;
  [SerializeField] TextMeshProUGUI FoodRarity;
  [SerializeField] TextMeshProUGUI FoodGrade;
  [SerializeField] TextMeshProUGUI FoodCatagory;
  [SerializeField] GameObject FoodInfoCard;
  [SerializeField] GameObject FoodPic;
  [SerializeField] GameObject FreshnessIcon;
  [SerializeField] TextMeshProUGUI MainAbilityDiscription;
  [SerializeField] TextMeshProUGUI SecondAbilityDiscription;
  private IngredientObjectScript selectIngrInfo;
  private int availableSlot;

  void Start(){

  }
  void Update(){/*
    if(Input.GetButtonDown("Submit") && selectedIngredient != null && selectedIngredient.GetComponent<IngredientSlotMachine>().ingredientCanBeAdded && GM.current.turnMan == 1){
        if(GM.current.ActiveMeal == 1 && GM.current.meal1.Count < 3){
          for (int i = 0; i <= GM.current.meal1.Count; i++){
            availableSlot = i;
          }
          GM.current.meal1.Add(selectedIngredient);
          if(availableSlot == 0){ selectedIngredient.transform.position = GM.current.potSlots[0].transform.position;}
          if(availableSlot == 1){ selectedIngredient.transform.position = GM.current.potSlots[1].transform.position;}
          if(availableSlot == 2){ selectedIngredient.transform.position = GM.current.potSlots[2].transform.position;}
          GM.current.SwitchTurn();
        }

        if(GM.current.ActiveMeal == 2 && GM.current.meal2.Count < 3){
          for (int i = 0; i <= GM.current.meal2.Count; i++){
            availableSlot = i;
          }
          GM.current.meal2.Add(selectedIngredient);
          if(availableSlot == 0){ selectedIngredient.transform.position = GM.current.potSlots[0].transform.position;}
          if(availableSlot == 1){ selectedIngredient.transform.position = GM.current.potSlots[1].transform.position;}
          if(availableSlot == 2){ selectedIngredient.transform.position = GM.current.potSlots[2].transform.position;}
          GM.current.SwitchTurn();
        }

        if(GM.current.ActiveMeal == 3 && GM.current.meal3.Count < 3){
          for (int i = 0; i <= GM.current.meal3.Count; i++){
            availableSlot = i;
          }
          GM.current.meal3.Add(selectedIngredient);
          if(availableSlot == 0){ selectedIngredient.transform.position = GM.current.potSlots[0].transform.position;}
          if(availableSlot == 1){ selectedIngredient.transform.position = GM.current.potSlots[1].transform.position;}
          if(availableSlot == 2){ selectedIngredient.transform.position = GM.current.potSlots[2].transform.position;}
          GM.current.SwitchTurn();
        }
    }*/

    if(Input.GetButtonDown("Submit") && selectedIngredient != null && selectedIngredient.GetComponent<IngredientSlotMachine>().ingredientCanBeAdded && GM.current.turnMan == 1){
        if(GM.current.ActiveMeal == 1 && GM.current.meal1.Count < 3 && !GM.current.meal1Cooked){
          GM.current.meal1.Add(selectedIngredient);
          selectedIngredient.transform.position = GM.current.potSlots[2].transform.position;
          GM.current.SwitchTurn();
        }
        if(GM.current.ActiveMeal == 2 && GM.current.meal2.Count < 3 && !GM.current.meal2Cooked){
          GM.current.meal2.Add(selectedIngredient);
          selectedIngredient.transform.position = GM.current.potSlots[2].transform.position;
          GM.current.SwitchTurn();
        }
        if(GM.current.ActiveMeal == 3 && GM.current.meal3.Count < 3 && !GM.current.meal3Cooked){
          GM.current.meal3.Add(selectedIngredient);
          selectedIngredient.transform.position = GM.current.potSlots[2].transform.position;
          GM.current.SwitchTurn();
        }
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
    if(GM.current.turnMan == 1 && GM.current.allowInput){
      //if player is playing with controller/keyboard
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

  public void DisplayFoodInfo(){
    //FoodInfoCard.SetActive(false);
    selectIngrInfo = selectedIngredient.GetComponent<IngredientSlotMachine>().ingredient;
    FoodName.text = selectIngrInfo.IngredientName;
    FoodDescription.text = selectIngrInfo.IngredientDesciption;
    FoodRarity.text = selectIngrInfo.IngredientTier;
    FoodGrade.text = selectIngrInfo.IngredientQuality.ToString();
    FoodCatagory.text = selectIngrInfo.IngredientCatagory;
    FoodPic.GetComponent<Image>().sprite = selectIngrInfo.IngredientImage;
    FreshnessIcon.SetActive(true);
    FoodPic.SetActive(true);
    MainAbilityDiscription.text = selectIngrInfo.MainAbility;
    SecondAbilityDiscription.text = selectIngrInfo.SecondAbility;
    //FoodInfoCard.SetActive(true);
  }

  public void ResetInfo(){
    selectIngrInfo = null;
    FoodName.text = null;
    FoodDescription.text = null;
    FoodRarity.text = null;
    FoodGrade.text = null;
    FoodCatagory.text = null;
    FoodPic.SetActive(false);
    FreshnessIcon.SetActive(false);
    MainAbilityDiscription.text = null;
    SecondAbilityDiscription.text = null;
    //FoodInfoCard.SetActive(false);
  }

}
