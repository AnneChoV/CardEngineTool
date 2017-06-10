using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckOfCards : ScriptableObject
{
    public List<CardItem> cardList;

    public List<CardItem> ShuffleDeck()
    {
        List<CardItem> shuffledList = new List<CardItem>();
        for (int i = cardList.Count; i > 0;)
        {
            int randomNumber = Random.Range(0, cardList.Count);
            shuffledList.Add(cardList[randomNumber]);
            cardList.RemoveRange(randomNumber, 1);
        }
        cardList = shuffledList;
        return shuffledList;
    }

    public void RemoveOneCardFromList(int index)
    {
        cardList.RemoveRange(index, 1);
    }
}
