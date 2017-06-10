using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public List<CardItem> m_CardHand;



    public int CheckAmountOfCardsInHandOfRank(int _rankNumber)
    {
        int counter = 0;

        for (int i = 0; i < m_CardHand.Count; i++)
        {
            if (m_CardHand[i].m_cardRank == _rankNumber)
            {
                counter++;
            }
        }
        return counter;
    }

    public void CheckHasEnoughCardsOfSameRank(int _rankNumber, int numberNeeded)
    {
        List<int> foundCardIndexes = new List<int>();

        for (int i = m_CardHand.Count; i > 0; i--)  //Go through hand
        {
            if (m_CardHand[i].m_cardRank == _rankNumber) //Check if the names of the card is the same
            {
                foundCardIndexes.Add(i);    //if they are add it to the list
                if(foundCardIndexes.Count == numberNeeded)  //if we have found enough cards of the same name
                {
                    for (int x = 0; i < numberNeeded; i++)  //go through and remove all of them using the saved indexes
                    {
                        m_CardHand.RemoveAt(foundCardIndexes[x]);
                        foundCardIndexes.Clear();
                    }
                }
            }
        }
    }

    public void PickUpCard(CardItem card)
    {
        m_CardHand.Add(card);
    }
}
