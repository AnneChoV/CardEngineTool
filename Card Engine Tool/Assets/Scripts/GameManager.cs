﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour {

    CardOptions cardOptions;
    DeckOptions deckOptions;

    //Deck Type
    public TypeOfDeck DeckType;     //52 Card deck or custom

    //DECK
    public int numberOfDecks;
    public int deckSize;

    //HAND
    public int startingHandSize;   
    public int minimumSizeOfHand;
    public int maximumSizeOfHand;

    //TURNS
    public int turnOrder; //NEEDS AN ENUM TO SPECIFY HOW WE PICK TURNS. EG RANDOMIZE, IN ORDER, ETC
    public int turnTimer;  //End the turn after this amount of time. 0 for no turn timer.


    public enum TypeOfDeck
    {
        Standard52CardDeck,
        Custom
    }

    private void OnValidate()
    {
        cardOptions = GetComponent<CardOptions>();
        deckOptions = GetComponent<DeckOptions>();

        if (DeckType == TypeOfDeck.Custom)
        {
           
        }
        else
        {

        }
    }
}
