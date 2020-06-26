using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeckScript : MonoBehaviour
{
    public static CharacterDeckScript current;
    private void Awake(){current = this;}

    public List<CharacterInfoScript> allCharacters = new List<CharacterInfoScript>();
    public List<CharacterInfoScript> playerDeck = new List<CharacterInfoScript>();

}
