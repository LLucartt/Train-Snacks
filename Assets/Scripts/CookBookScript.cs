using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.EventSystems;

public class CookBookScript : MonoBehaviour
{

    public static CookBookScript current;
    private void Awake(){current = this;}


    private string FirstIngrAdd;
    private string SecondIngrAdd;
    private string ThirdIngrAdd;
    private string FinalMeal;
    private string SecondFinalMeal;
    private string ThirdFinalMeal;
    private string MealMainAbility;
    private string MealSecondAbility;
    private string MealThirdAbility;
    private bool FinalMealPreped;

    [HideInInspector] public bool feedCharBool = false;

    [Header("Final Meal UI")]
    public GameObject mealUiGo;
    public TextMeshProUGUI mealUiFinalMealDisp;
    public TextMeshProUGUI mealUiSecondMealDisp;
    public TextMeshProUGUI mealUiThirdMealDisp;
    public TextMeshProUGUI MainAbilityDisplay;
    public TextMeshProUGUI SecondAbilityDisplay;
    public TextMeshProUGUI ThirdAbilityDisplay;
    public GameObject MainIngrImage;
    public GameObject SecondIngrImage;
    public GameObject ThirdIngrImage;
    public GameObject firstChar;
    public GameObject secChar;
    public GameObject thirdChar;
    public GameObject withText;
    public GameObject andText;

    private GM gm;

    public class tmpModel {
      public string Key {get;set;}
      public int Count {get;set;}
    }

    void Start(){
      gm = GM.current;
    }

    void Update(){
      /*
      if(Input.GetButtonDown("Submit") && FinalMealPreped){
        mealUiGo.SetActive(false);
        FinalMealPreped = false;
        EventSystem.current.SetSelectedGameObject(firstCharGo);
        gm.playerContr.SetActive(false);
      }*/
    }

    public void checkIngredients(){
      //grab the meal codes from scriptable objects
      if(gm.ActiveMeal == 1){
        FirstIngrAdd                           = gm.meal1[0].GetComponent<IngredientSlotMachine>().ingredient.IngrCode;
        if(gm.meal1.Count == 2){ SecondIngrAdd = gm.meal1[1].GetComponent<IngredientSlotMachine>().ingredient.IngrCode; }
        if(gm.meal1.Count == 3){ ThirdIngrAdd  = gm.meal1[2].GetComponent<IngredientSlotMachine>().ingredient.IngrCode; }
      }
      else if(gm.ActiveMeal == 2){
        FirstIngrAdd                           = gm.meal2[0].GetComponent<IngredientSlotMachine>().ingredient.IngrCode;
        if(gm.meal2.Count == 2){ SecondIngrAdd = gm.meal2[1].GetComponent<IngredientSlotMachine>().ingredient.IngrCode; }
        if(gm.meal2.Count == 3){ ThirdIngrAdd  = gm.meal2[2].GetComponent<IngredientSlotMachine>().ingredient.IngrCode; }
      }
      else if(gm.ActiveMeal == 3){
        FirstIngrAdd                           = gm.meal3[0].GetComponent<IngredientSlotMachine>().ingredient.IngrCode;
        if(gm.meal3.Count == 2){ SecondIngrAdd = gm.meal3[1].GetComponent<IngredientSlotMachine>().ingredient.IngrCode; }
        if(gm.meal3.Count == 3){ ThirdIngrAdd  = gm.meal3[2].GetComponent<IngredientSlotMachine>().ingredient.IngrCode; }
      }

      var tmp = new List<string>() {FirstIngrAdd, SecondIngrAdd, ThirdIngrAdd};
      var uniqueItems = tmp.GroupBy(x => x).Select(x => new tmpModel { Key = x.Key, Count = x.Count()});
      var uniqueItemList = uniqueItems.ToList();

      /*
      foreach (var g in uniqueItemList) {
        Debug.Log(string.Format("Key: {0}, Count = {1}", g.Key, g.Count));
      }*/
      /*
      FinalMeal = "Dubious Goop";

      // meals with red meat
      if(uniqueItemList.Any(x => x.Key == "Me1")){ FinalMeal = "Fried Horned Grasslander";
        // + any other meat
            if(uniqueItemList.Any(x => x.Key == "Me2") || uniqueItemList.Any(x => x.Key == "Me3")){FinalMeal = "Meat Skewers";
                      // + fish
                           if(uniqueItemList.Any(x => x.Key == "Fi1" )){ FinalMeal = "Rainbow Meat Skewers";}
                      else if(uniqueItemList.Any(x => x.Key == "Fi2" )){ FinalMeal = "Cocooned Meat Skewers";}
                      else if(uniqueItemList.Any(x => x.Key == "Fi3" )){ FinalMeal = "Magma Meat Skewers";}
                      // + cereals and nuts
                      else if(uniqueItemList.Any(x => x.Key == "Ce1" )){ FinalMeal = "Prickly Meat Cubes on Rice";}
                      else if(uniqueItemList.Any(x => x.Key == "Ce2" )){ FinalMeal = "Crumbed Heart-Shaped Meat Skewers";}
                      else if(uniqueItemList.Any(x => x.Key == "Ce3" )){ FinalMeal = "Frosty Skewers coated with Nuts";}
                      // + Fruit
                      else if(uniqueItemList.Any(x => x.Key == "Fr1" )){ FinalMeal = "Shimmery Meat Skewers";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr2" )){ FinalMeal = "Soulless Meat Skewers";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr3" )){ FinalMeal = "Winged Meat Skewers";}
                      // + veg
                      else if(uniqueItemList.Any(x => x.Key == "Ve1" )){ FinalMeal = "Punchy Meat Skewers";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve2" )){ FinalMeal = "Flicky Meat Skewers";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve3" )){ FinalMeal = "Plonked Meat Skewers";}
                      // dairy
                      else if(uniqueItemList.Any(x => x.Key == "Da1" )){ FinalMeal = "Starry Meat Skewers";}
                      else if(uniqueItemList.Any(x => x.Key == "Da2" )){ FinalMeal = "Snowy Meat Stew";}
                      else if(uniqueItemList.Any(x => x.Key == "Da3" )){ FinalMeal = "Cheesy Reflective Meat Skewers";}
            }

            // red meat + rainbow fish fish
            else if(uniqueItemList.Any(x => x.Key == "Fi1")){ FinalMeal = "Rainbow Horns";
                      // + cereals and nuts
                           if(uniqueItemList.Any(x => x.Key == "Ce1" )){ FinalMeal = "Stuffed Rainbow Horns with Prickly Rice";}
                      else if(uniqueItemList.Any(x => x.Key == "Ce2" )){ FinalMeal = "Rainbow Horns with Heart-Shaped Croutons";}
                      else if(uniqueItemList.Any(x => x.Key == "Ce3" )){ FinalMeal = "Frosty Rainbow Horns coated with Nuts";}
                      // + Fruit
                      else if(uniqueItemList.Any(x => x.Key == "Fr1" )){ FinalMeal = "Pegasus Horns";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr2" )){ FinalMeal = "Ghostly Pegasus Horns";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr3" )){ FinalMeal = "Nocturnal Pegasus Horns";}
                      // + veg
                      else if(uniqueItemList.Any(x => x.Key == "Ve1" )){ FinalMeal = "Punchy Rainbow Horns";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve2" )){ FinalMeal = "Flicky Rainbow Horns";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve3" )){ FinalMeal = "Plonked Rainbow Horns";}
                      // dairy
                      else if(uniqueItemList.Any(x => x.Key == "Da1" )){ FinalMeal = "Dispersion Horns";}
                      else if(uniqueItemList.Any(x => x.Key == "Da2" )){ FinalMeal = "Aerial Horns";}
                      else if(uniqueItemList.Any(x => x.Key == "Da3" )){ FinalMeal = "Holographic Horns";}
            }

            //red meat + eel
            else if(uniqueItemList.Any(x => x.Key == "Fi2")){ FinalMeal = "Cocooned Grasslander Pocket";
                      // + cereals and nuts
                           if(uniqueItemList.Any(x => x.Key == "Ce1" )){ FinalMeal = "Spikey Grasslander and Rice Pocket";}
                      else if(uniqueItemList.Any(x => x.Key == "Ce2" )){ FinalMeal = "Heart-Shaped Grasslander Bun";}
                      else if(uniqueItemList.Any(x => x.Key == "Ce3" )){ FinalMeal = "Grasslander and Packed Icenuts Pocket";}
                      // + Fruit
                      else if(uniqueItemList.Any(x => x.Key == "Fr1" )){ FinalMeal = "Shimmery Grasslander Pocket";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr2" )){ FinalMeal = "Deflated Grasslander Pocket";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr3" )){ FinalMeal = "Metamorphosed Grasslander";}
                      // + veg
                      else if(uniqueItemList.Any(x => x.Key == "Ve1" )){ FinalMeal = "Punchy Grasslander Pocket";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve2" )){ FinalMeal = "Airy Grasslander Pocket";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve3" )){ FinalMeal = "Bursting Grasslander Pocket";}
                      // dairy
                      else if(uniqueItemList.Any(x => x.Key == "Da1" )){ FinalMeal = "Universal Grasslander Pocket";}
                      else if(uniqueItemList.Any(x => x.Key == "Da2" )){ FinalMeal = "Grasslander Snowglobe";}
                      else if(uniqueItemList.Any(x => x.Key == "Da3" )){ FinalMeal = "Grasslander Pocket Mirror";}
            }

            //red meat + crab + ...
            else if(uniqueItemList.Any(x => x.Key == "Fi3")){ FinalMeal = "Pinched Lava Grasslander Steak";
                      // + cereals and nuts
                           if(uniqueItemList.Any(x => x.Key == "Ce1" )){ FinalMeal = "Stalagmite Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Ce2" )){ FinalMeal = "Heat of the Moment kissed Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Ce3" )){ FinalMeal = "Lukewarm Steak";}
                      // + Fruit
                      else if(uniqueItemList.Any(x => x.Key == "Fr1" )){ FinalMeal = "Shimmery Magma Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr2" )){ FinalMeal = "Underworldly Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr3" )){ FinalMeal = "Winged Eruption Steak";}
                      // + veg
                      else if(uniqueItemList.Any(x => x.Key == "Ve1" )){ FinalMeal = "Magma Sangria Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve2" )){ FinalMeal = "Firestarter Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve3" )){ FinalMeal = "Contained Eruption Steak ";}
                      // dairy
                      else if(uniqueItemList.Any(x => x.Key == "Da1" )){ FinalMeal = "Well-lit Night Sky Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Da2" )){ FinalMeal = "Rock-Hard Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Da3" )){ FinalMeal = "Glass Glazed Lava Steak";}
            }

            // red meat + prickly rice
            else if(uniqueItemList.Any(x => x.Key == "Ce1")){ FinalMeal = "Grasslander on Prickly Rice";
                      // + cereals and nuts
                           if(uniqueItemList.Any(x => x.Key == "Ce2" )){ FinalMeal = "Grasslander     on Prickly Rice";}
                      else if(uniqueItemList.Any(x => x.Key == "Ce3" )){ FinalMeal = "Grasslander on Prickly Rice";}
                      // + Fruit
                      else if(uniqueItemList.Any(x => x.Key == "Fr1" )){ FinalMeal = "Shimmery Magma Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr2" )){ FinalMeal = "Underworldly Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Fr3" )){ FinalMeal = "Winged Eruption Steak";}
                      // + veg
                      else if(uniqueItemList.Any(x => x.Key == "Ve1" )){ FinalMeal = "Magma Sangria Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve2" )){ FinalMeal = "Firestarter Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Ve3" )){ FinalMeal = "Contained Eruption Steak ";}
                      // dairy
                      else if(uniqueItemList.Any(x => x.Key == "Da1" )){ FinalMeal = "Well-lit Night Sky Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Da2" )){ FinalMeal = "Rock-Hard Steak";}
                      else if(uniqueItemList.Any(x => x.Key == "Da3" )){ FinalMeal = "Glass Glazed Lava Steak";}
            }


      }

      //meals with bird meat
      else if(uniqueItemList.Any(x => x.Key == "Me2" && x.Count == 1)){ FinalMeal = "Fried Cloud Surfer";
        if(uniqueItemList.Any(x => x.Key == "Me1" && x.Count == 1) || uniqueItemList.Any(x => x.Key == "Me3" && x.Count == 1 )){FinalMeal = "Meat Skewers"; }
      }

      //meals with rodent meat
      else if(uniqueItemList.Any(x => x.Key == "Me3" && x.Count == 1)){ FinalMeal = "Fried Sewer Dweller";
        if(uniqueItemList.Any(x => x.Key == "Me2" && x.Count == 1) || uniqueItemList.Any(x => x.Key == "Me1" && x.Count == 1 )){FinalMeal = "Meat Skewers"; }
      }

      /*
      if(uniqueItemList.Any(x => x.Key == "1" && x.Count == 2) && uniqueItemList.Any(x => x.Key == "2" && x.Count == 1)) {
        //TypeofScent.text = "Mostly Bacon with a hint of squicky toy";
        //FinalScentNumber = 112;
      }*/

      string tempIngrCode = gm.MainIngredient.GetComponent<IngredientSlotMachine>().ingredient.IngrCode;
      //meat as main ingr
      if(tempIngrCode == "Me1"){
        FinalMeal = "Horned Grasslander Steak";
        MealMainAbility = "Shield";
      }
      if(tempIngrCode == "Me2"){
        FinalMeal = "Grilled Cloud Surfer Drumsticks";
        MealMainAbility = "Shield";
      }
      if(tempIngrCode == "Me3"){
        FinalMeal = "Goopy Sewer Dweller Stew";
        MealMainAbility = "Shield";
      }

      //fish as main IngrCode
      if(tempIngrCode == "Fi1"){
        FinalMeal = "Seared Rainbow Bass Filet";
        MealMainAbility = "Evade";
      }
      if(tempIngrCode == "Fi2"){
        FinalMeal = "Cacooning Eel Chowder";
        MealMainAbility = "Evade";
      }
      if(tempIngrCode == "Fi3"){
        FinalMeal = "Erupting Crab Cakes";
        MealMainAbility = "Evade";
      }

      //cereal as main ingr
      if(tempIngrCode == "Ce1"){
        FinalMeal = "Prickly Fried Rice";
        MealMainAbility = "Weaken";
      }
      if(tempIngrCode == "Ce2"){
        FinalMeal = "Heart-Shaped focaccia";
        MealMainAbility = "Weaken";
      }
      if(tempIngrCode == "Ce3"){
        FinalMeal = "Hailzelnut Crumbed Bites";
        MealMainAbility = "Weaken";
      }

      //fruit as main ingredient
      if(tempIngrCode == "Fr1"){
        FinalMeal = "Glistening Opal Fruit Stew";
        MealMainAbility = "Heal";
      }
      if(tempIngrCode == "Fr2"){
        FinalMeal = "Soulberry Jam";
        MealMainAbility = "Heal";
      }
      if(tempIngrCode == "Fr3"){
        FinalMeal = "Mothmelon Cups";
        MealMainAbility = "Heal";
      }

      //veg as main ingr
      if(tempIngrCode == "Ve1"){
        FinalMeal = "Roasted Punchroot";
        MealMainAbility = "Attack";
      }
      if(tempIngrCode == "Ve2"){
        FinalMeal = "Sauteed Flickleaf";
        MealMainAbility = "Attack";
      }
      if(tempIngrCode == "Ve3"){
        FinalMeal = "Slamkin Soup";
        MealMainAbility = "Attack";
      }

      //dairy as main ingr
      if(tempIngrCode == "Da1"){
        FinalMeal = "Celestial Butter Parfait";
        MealMainAbility = "Unpredictable";
      }
      if(tempIngrCode == "Da2"){
        FinalMeal = "Snowy Milk Jellies";
        MealMainAbility = "Unpredictable";
      }
      if(tempIngrCode == "Da3"){
        FinalMeal = "Mozzamirror Melt";
        MealMainAbility = "Unpredictable";
      }

      if(gm.SecondIngredient != null){
        tempIngrCode = gm.SecondIngredient.GetComponent<IngredientSlotMachine>().ingredient.IngrCode;
        //meat as main ingr
        if(tempIngrCode == "Me1"){
          SecondFinalMeal = "Crispy Grasslander Horns";
          MealSecondAbility = "Fortify";
        }
        if(tempIngrCode == "Me2"){
          SecondFinalMeal = "Golden Cloud Surfer Wings";
          MealSecondAbility = "Fortify";
        }
        if(tempIngrCode == "Me3"){
          SecondFinalMeal = "Furry Sewer Dweller Tails";
          MealSecondAbility = "Fortify";
        }

        //fish as main IngrCode
        if(tempIngrCode == "Fi1"){
          SecondFinalMeal = "Candied Rainbow Bass Scales";
          MealSecondAbility = "Evade";
        }
        if(tempIngrCode == "Fi2"){
          SecondFinalMeal = "A Cacooning Eel Basket";
          MealSecondAbility = "Evade";
        }
        if(tempIngrCode == "Fi3"){
          SecondFinalMeal = "Magma Crab Legs";
          MealSecondAbility = "Evade";
        }

        //cereal as main ingr
        if(tempIngrCode == "Ce1"){
          SecondFinalMeal = "Prickly Risotto";
          MealSecondAbility = "Weaken";
        }
        if(tempIngrCode == "Ce2"){
          SecondFinalMeal = "Heart-Shaped Biscuits";
          MealSecondAbility = "Weaken";
        }
        if(tempIngrCode == "Ce3"){
          SecondFinalMeal = "Frosty Hailzelnuts";
          MealSecondAbility = "Weaken";
        }

        //fruit as main ingredient
        if(tempIngrCode == "Fr1"){
          SecondFinalMeal = "Opal Fruit Shimmers";
          MealSecondAbility = "Heal";
        }
        if(tempIngrCode == "Fr2"){
          SecondFinalMeal = "Soulberry Coulis";
          MealSecondAbility = "Heal";
        }
        if(tempIngrCode == "Fr3"){
          SecondFinalMeal = "Mothmelon Cubes";
          MealSecondAbility = "Heal";
        }

        //veg as main ingr
        if(tempIngrCode == "Ve1"){
          SecondFinalMeal = "Punchroot puree";
          MealSecondAbility = "Damage";
        }
        if(tempIngrCode == "Ve2"){
          SecondFinalMeal = "Flickleaf Crisps";
          MealSecondAbility = "Damage";
        }
        if(tempIngrCode == "Ve3"){
          SecondFinalMeal = "Slamkin Seeds";
          MealSecondAbility = "Damage";
        }

        //dairy as main ingr
        if(tempIngrCode == "Da1"){
          SecondFinalMeal = "Whipped Celestial Butter";
          MealSecondAbility = "Unpredictable";
        }
        if(tempIngrCode == "Da2"){
          SecondFinalMeal = "Condensed Snowy Milk";
          MealSecondAbility = "Unpredictable";
        }
        if(tempIngrCode == "Da3"){
          SecondFinalMeal = "Mozzamirror Shavings";
          MealSecondAbility = "Unpredictable";
        }
      }

      if(gm.ThirdIngredient != null){
        tempIngrCode = gm.ThirdIngredient.GetComponent<IngredientSlotMachine>().ingredient.IngrCode;
        //meat as main ingr
        if(tempIngrCode == "Me1"){
          ThirdFinalMeal = "Horned Grasslander Gravy";
          MealThirdAbility = "Fortify";
        }
        if(tempIngrCode == "Me2"){
          ThirdFinalMeal = "Crispy Cloud Surfer Skin";
          MealThirdAbility = "Fortify";
        }
        if(tempIngrCode == "Me3"){
          ThirdFinalMeal = "Suspiciously Glowing Goop";
          MealThirdAbility = "Fortify";
        }

        //fish as main IngrCode
        if(tempIngrCode == "Fi1"){
          ThirdFinalMeal = "Marinated Rainbow Bass Rolls";
          MealThirdAbility = "Evade";
        }
        if(tempIngrCode == "Fi2"){
          ThirdFinalMeal = "Glazed Cacooning Eel Pockets";
          MealThirdAbility = "Evade";
        }
        if(tempIngrCode == "Fi3"){
          ThirdFinalMeal = "Lava Crab Bisque";
          MealThirdAbility = "Evade";
        }

        //cereal as main ingr
        if(tempIngrCode == "Ce1"){
          ThirdFinalMeal = "Prickly Paella";
          MealThirdAbility = "Weaken";
        }
        if(tempIngrCode == "Ce2"){
          ThirdFinalMeal = "Heart-Shaped Croutons";
          MealThirdAbility = "Weaken";
        }
        if(tempIngrCode == "Ce3"){
          ThirdFinalMeal = "Hailzelnut Peaks";
          MealThirdAbility = "Weaken";
        }

        //fruit as main ingredient
        if(tempIngrCode == "Fr1"){
          ThirdFinalMeal = "Caramel Coated Opal Fruit";
          MealThirdAbility = "Heal";
        }
        if(tempIngrCode == "Fr2"){
          ThirdFinalMeal = "a Soulberry Smoothie";
          MealThirdAbility = "Heal";
        }
        if(tempIngrCode == "Fr3"){
          ThirdFinalMeal = "Mothmelon Salad";
          MealThirdAbility = "Heal";
        }

        //veg as main ingr
        if(tempIngrCode == "Ve1"){
          ThirdFinalMeal = "Spiced Punchroot Sticks";
          MealThirdAbility = "Damage";
        }
        if(tempIngrCode == "Ve2"){
          ThirdFinalMeal = "Creamed Flickleaf";
          MealThirdAbility = "Damage";
        }
        if(tempIngrCode == "Ve3"){
          ThirdFinalMeal = "Slamkin Pie";
          MealThirdAbility = "Damage";
        }

        //dairy as main ingr
        if(tempIngrCode == "Da1"){
          ThirdFinalMeal = "A puddle Of Celestial Butter";
          MealThirdAbility = "Unpredictable";
        }
        if(tempIngrCode == "Da2"){
          ThirdFinalMeal = "A Glass Of Snowy Milk";
          MealThirdAbility = "Unpredictable";
        }
        if(tempIngrCode == "Da3"){
          ThirdFinalMeal = "Pan-fried Mozzamirror Parcels";
          MealThirdAbility = "Unpredictable";
        }
      }

      DisplayMeals();
    }

    void DisplayMeals(){
      mealUiFinalMealDisp.text = FinalMeal;
      mealUiSecondMealDisp.text = SecondFinalMeal;
      mealUiThirdMealDisp.text = ThirdFinalMeal;

      MainAbilityDisplay.text = MealMainAbility;
      SecondAbilityDisplay.text = MealSecondAbility;
      ThirdAbilityDisplay.text = MealThirdAbility;

      MainIngrImage.GetComponent<Image>().sprite = gm.MainIngredient.GetComponent<IngredientSlotMachine>().ingredient.IngredientImage;
      if(gm.SecondIngredient != null){
        SecondIngrImage.GetComponent<Image>().sprite = gm.SecondIngredient.GetComponent<IngredientSlotMachine>().ingredient.IngredientImage;
        SecondIngrImage.SetActive(true);
        withText.SetActive(true);
      }
      if(gm.ThirdIngredient != null){
        ThirdIngrImage.GetComponent<Image>().sprite = gm.ThirdIngredient.GetComponent<IngredientSlotMachine>().ingredient.IngredientImage;
        ThirdIngrImage.SetActive(true);
        andText.SetActive(true);
      }

      if(!GM.current.Player1Eaten){
        EventSystem.current.SetSelectedGameObject(firstChar);
      }
      else if(!GM.current.Player2Eaten){
        EventSystem.current.SetSelectedGameObject(secChar);
      }
      else if(!GM.current.Player3Eaten){
        EventSystem.current.SetSelectedGameObject(thirdChar);
      }

      if(GM.current.ActiveMeal == 1){
        GM.current.meal1Cooked = true;
      }
      else if(GM.current.ActiveMeal == 2){
        GM.current.meal2Cooked = true;
      }
      else if(GM.current.ActiveMeal == 3){
        GM.current.meal3Cooked = true;
      }

      FinalMealPreped = true;
      mealUiGo.SetActive(true);
      feedCharBool = true;
    }

    public void FeedChar(CharacterCardScript Char){
      Char.MainAttack = MealMainAbility;
      Char.secondBuff = MealSecondAbility;
      Char.thirdBuff = MealThirdAbility;
      EventSystem.current.SetSelectedGameObject(null);
      ResetUI();
    }

    void ResetUI(){
      withText.SetActive(false);
      andText.SetActive(false);
      SecondIngrImage.SetActive(false);
      ThirdIngrImage.SetActive(false);
      mealUiSecondMealDisp.text = "";
      mealUiThirdMealDisp.text = "";
      SecondAbilityDisplay.text = "";
      ThirdAbilityDisplay.text = "";
      SecondFinalMeal = null;
      ThirdFinalMeal = null;
      MealSecondAbility = null;
      MealThirdAbility = null;
      mealUiGo.SetActive(false);
      if(GM.current.meal1Cooked && GM.current.meal2Cooked && GM.current.meal3Cooked){
        GameObject[] ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
        foreach(GameObject ingredient in ingredients){
          GameObject.Destroy(ingredient);
        }
        GM.current.battleModeActivate();
      } else {
      StartCoroutine(allowMovement());
      }
    }

    public IEnumerator allowMovement(){
      yield return new WaitForSeconds(0.2f);
      gm.allowInput = true;
    }
}
