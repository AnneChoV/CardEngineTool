using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStandardDeck : MonoBehaviour {

    private DeckOfCards deckOfCards;
    private int numberOfCards = 52;

    int rank;
    int suit;

    // Use this for initialization
    void Start () {
        CreateNewItemList();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateNewItemList()
    {
        // Start at 1 because it's easier to read
        rank = 1;
        suit = 1;

        deckOfCards = CreateDeckOfCards.Create();

        if (deckOfCards)
        {
            deckOfCards.itemList = new List<CardItem>();
        }

        for (int i = 0; i < numberOfCards; i++)
        {
            CardItem cardItem = new CardItem();

            if (rank > 13)
            {
                Debug.Log("Rank" + rank);
                rank = 1;
            }

            cardItem.m_cardRank = rank;
            rank++;


            if (suit > 4)
            {
                suit = 1;
            }

            cardItem.m_cardSuit = suit;
            suit++;

            deckOfCards.itemList.Add(cardItem);
        }
    }
}
