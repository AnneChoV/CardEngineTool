using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


[CreateAssetMenu(fileName = "Test", menuName = "Inventory/List", order = 1)]

public class DeckOfCards : ScriptableObject
{
    [HideInInspector]
    public List<Card> itemList;
    public List<CardItem> cardList;


    //public virtual List<Card> ShuffleDeck()
    //{
    //    List<Card> shuffledList = new List<Card>();
    //    for (int i = itemList.Count; i > 0; i--)
    //    {
    //        int randomNumber = Random.Range(0, itemList.Count);
    //        shuffledList.Add(itemList[randomNumber]);
    //        itemList.RemoveRange(randomNumber, 1);
    //    }
    //    itemList = shuffledList;
    //    return shuffledList;
    //}

    //public Card RemoveOneCardFromList(int _index)
    //{
    //    Debug.Log("Reached here1");
    //    Card cardItem = itemList[_index];
    //    itemList.RemoveRange(_index, 1);
    //    return cardItem;
    //}
    //public Card RemoveOneCardFromList(Card cardItem)
    //{        
    //    itemList.Remove(cardItem);
    //    return cardItem;
    //}

    //public List<Card> CreateHand(int _numberOfCards)
    //{
    //    List<Card> newHand = new List<Card>();

    //    for (int i = _numberOfCards; i > 0; i--)
    //    {
    //        Debug.Log("Reachedhere2");
    //        newHand.Add(itemList[0]);
    //        Debug.Log(itemList[0]);
    //        RemoveOneCardFromList(0);
    //    }
    //    return newHand;
    //}
}
