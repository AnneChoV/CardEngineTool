using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    int typeOfDeck;     //52 Card deck or custom

    //HAND
    int startingHandSize;
    int minimumSizeOfHand;
    int maximumSizeOfHand;

    //DECK
    int numberOfDecks;
    int deckSize;

    //TURNS
    int turnOrder; //NEEDS AN ENUM TO SPECIFY HOW WE PICK TURNS. EG RANDOMIZE, IN ORDER, ETC

    int turnTimer;  //End the turn after this amount of time. 0 for no turn timer.
}
