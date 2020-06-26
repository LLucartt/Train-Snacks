using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManagerScript : MonoBehaviour
{

    public static CardManagerScript current;
    private void Awake(){current = this;}

    [SerializeField] GameObject middleCard = null;
    [HideInInspector] public bool chooseCardToAdd = true;

    void Start(){
      chooseCardToAdd = true;
      EventSystem.current.SetSelectedGameObject(middleCard);
    }
}
