using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TestCharScript : MonoBehaviour
{

  public int charHealth = 100;
  public TextMeshProUGUI charHealthDisplay;
  public

  void Start(){
    UpdateCharHealth();
  }

  public void UpdateCharHealth(){
    charHealthDisplay.text = charHealth.ToString();
  }

  public void AssignMeal(){
    GetComponent<Button>().interactable = false;
    EventSystem.current.SetSelectedGameObject(null);
    GM.current.playerContr.SetActive(true);
  }

}
