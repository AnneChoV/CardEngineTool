using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour{
    public Player[] players;
    public DeckOfCards cardDeck;

    public int startingHandSize = 7;
    int m_playersTurn = 0;

    int numberOfCardsNeededForPoints;

    private void Start()
    {
      //  cardDeck.ShuffleDeck();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].m_CardHand = cardDeck.CreateHand(startingHandSize);
        }
    }

    public int FindOtherPlayersIndexNumber()
    {
        if (m_playersTurn == 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }


    public void OnHandCardClick(BaseEventData _data)
    {
        PointerEventData pointerData = _data as PointerEventData;
        Debug.Log(pointerData.pointerPressRaycast.gameObject);

        GameObject clickedObject = pointerData.pointerPressRaycast.gameObject;
        CardItem clickedCard = clickedObject.GetComponent<CardItem>();


        CardItem enemiesCard = CheckIfPlayerHasCardRank(clickedCard.m_cardRank, FindOtherPlayersIndexNumber());

        if (enemiesCard != null)    //then enemy does have card
        {
            players[m_playersTurn].m_CardHand.Add(enemiesCard);

            players[m_playersTurn].CheckHasEnoughCardsOfSameRank(clickedCard.m_cardRank, numberOfCardsNeededForPoints);
        }
        else
        {
            GoFish();
        }
     }


    public CardItem CheckIfPlayerHasCardRank(int rank, int playerIndex)
    {
    
        for (int i = 0; i < players[playerIndex].m_CardHand.Count; i++)
        {
            if (players[playerIndex].m_CardHand[i].m_cardRank == rank)
            {
                return players[playerIndex].m_CardHand[i];
            }
        }
        return null;
    }

    public void GoFish()
    {
        players[m_playersTurn].m_CardHand.Add(cardDeck.RemoveOneCardFromList(0));
    }
}

