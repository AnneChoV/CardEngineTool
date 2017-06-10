using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public DeckOfCards deckOfCards;
    public Transform cardHand;
    public Sprite kittyBack;

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


    public DeckOfCards cardDeck;



    public enum TypeOfDeck
    {
        Standard52CardDeck,
        Custom
    }

    private void OnValidate()
    {
       // cardOptions = GetComponent<CardOptions>();
        deckOptions = GetComponent<DeckOptions>();


        if (DeckType == TypeOfDeck.Custom)
        {
           
        }
        else
        {

        }
    }

    private void Start()
    {
        //Debug.Log("Display first few cards");

        //for (int i = 0; i < 8; i++)
        //{
        //    Sprite sprite = deckOfCards.itemList[i].cardIcon;
        //    GameObject card = Instantiate(Resources.Load("TempCard"), cardHand) as GameObject;
        //    card.GetComponent<Image>().sprite = sprite;
        //}
    }

    public void ChangeFace()
    {
        for (int i = 0; i < cardHand.childCount; i++)
        {
            cardHand.GetChild(i).GetComponent<Image>().sprite = kittyBack;
        }
    }
}
