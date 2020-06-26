using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

  public Slider sliderP3;

  public void SetMaxHealth (int health){
    sliderP3.maxValue = health;
    sliderP3.value = health;
  }
  public void SetHealth(int health){
    sliderP3.value = health;
  }

}
