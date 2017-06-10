using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStandardDeck : MonoBehaviour {

    private int numberOfCards = 52;

    public Sprite[] CardFrontImages;

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
        //rank = 1;
        //suit = 1;

        DeckOfCards deckOfCards = new DeckOfCards();

        deckOfCards = CreateDeckOfCards.Create();

        if (deckOfCards)
        {
            deckOfCards.itemList = new List<CardItem>();
        }

        //for (int i = 0; i < numberOfCards; i++)
        //{
        //    CardItem cardItem = new CardItem();

        //    if (rank > 13)
        //    {
        //        Debug.Log("Rank" + rank);
        //        rank = 1;
        //        suit++;

        //        if (suit > 4) // should never reach
        //        {
        //            Debug.Log("asdfdsf");
        //            suit = 1;
        //        }
        //    }

        //    cardItem.m_cardRank = rank;
        //    rank++;

        //    cardItem.m_cardSuit = suit;


        //    deckOfCards.itemList.Add(cardItem);
        //}

        CardFrontImages = Resources.LoadAll<Sprite>("");
        deckOfCards.itemList.Clear();
        for (int i = 0; i < CardFrontImages.Length; i++)
        {
            deckOfCards.itemList.Add(new CardItem());
            deckOfCards.itemList[i].cardIcon = CardFrontImages[i];
            deckOfCards.itemList[i].cardName = CardFrontImages[i].name;

            string cardNameString = deckOfCards.itemList[i].cardName;
            if (cardNameString.StartsWith("A"))
            {
                deckOfCards.itemList[i].m_cardRank = 1;
            }
            if (cardNameString.StartsWith("2"))
            {
                deckOfCards.itemList[i].m_cardRank = 2;
            }
            else if (cardNameString.StartsWith("3"))
            {
                deckOfCards.itemList[i].m_cardRank = 3;
            }
            else if (cardNameString.StartsWith("4"))
            {
                deckOfCards.itemList[i].m_cardRank = 4;
            }
            else if (cardNameString.StartsWith("5"))
            {
                deckOfCards.itemList[i].m_cardRank = 5;
            }
            else if (cardNameString.StartsWith("6"))
            {
                deckOfCards.itemList[i].m_cardRank = 6;
            }
            else if (cardNameString.StartsWith("7"))
            {
                deckOfCards.itemList[i].m_cardRank = 7;
            }
            else if (cardNameString.StartsWith("8"))
            {
                deckOfCards.itemList[i].m_cardRank = 8;
            }
            else if (cardNameString.StartsWith("9"))
            {
                deckOfCards.itemList[i].m_cardRank = 9;
            }
            else if (cardNameString.StartsWith("10"))
            {
                deckOfCards.itemList[i].m_cardRank = 10;
            }
            else if (cardNameString.StartsWith("J"))
            {
                deckOfCards.itemList[i].m_cardRank = 11;
            }
            else if (cardNameString.StartsWith("Q"))
            {
                deckOfCards.itemList[i].m_cardRank = 12;
            }
            else if (cardNameString.StartsWith("K"))
            {
                deckOfCards.itemList[i].m_cardRank = 13;
            }

            // SUITS
            if (cardNameString.EndsWith("hearts"))
            {
                deckOfCards.itemList[i].m_cardSuit = 1;
            }
            else if (cardNameString.EndsWith("spades"))
            {
                deckOfCards.itemList[i].m_cardSuit = 2;
            }
            else if (cardNameString.EndsWith("diamonds"))
            {
                deckOfCards.itemList[i].m_cardSuit = 3;
            }
            else if (cardNameString.EndsWith("clubs"))
            {
                deckOfCards.itemList[i].m_cardSuit = 4;
            }


        }
    }
}
