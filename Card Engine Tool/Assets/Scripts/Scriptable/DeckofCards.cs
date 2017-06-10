using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckOfCards : ScriptableObject
{
    public List<CardItem> itemList;

    private void OnEnable()
    {
       
    }

    public List<CardItem> ShuffleDeck()
    {
        List<CardItem> shuffledList = new List<CardItem>();
        for (int i = itemList.Count; i > 0; i--)
        {
            int randomNumber = Random.Range(0, itemList.Count);
            shuffledList.Add(itemList[randomNumber]);
            itemList.RemoveRange(randomNumber, 1);
        }
        itemList = shuffledList;
        return shuffledList;
    }

    public CardItem RemoveOneCardFromList(int _index)
    {
        Debug.Log("Reached here1");
        CardItem cardItem = itemList[_index];
        itemList.RemoveRange(_index, 1);
        return cardItem;
    }
    public CardItem RemoveOneCardFromList(CardItem cardItem)
    {        
        itemList.Remove(cardItem);
        return cardItem;
    }

    public List<CardItem> CreateHand(int _numberOfCards)
    {
        List<CardItem> newHand = new List<CardItem>();

        for (int i = _numberOfCards; i > 0; i--)
        {
            Debug.Log("Reachedhere2");
            newHand.Add(itemList[0]);
            Debug.Log(itemList[0]);
            RemoveOneCardFromList(0);
        }
        return newHand;
    }
}
