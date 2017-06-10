using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckOfCards : ScriptableObject
{
    public List<CardItem> itemList;

    public Sprite[] CardFrontImages;
    private void OnEnable()
    {
        Debug.Log("Here");
        CardFrontImages = Resources.LoadAll<Sprite>("");
        itemList.Clear();
        for (int i = 0; i < CardFrontImages.Length; i++)
        {
            itemList.Add(new CardItem());
            itemList[i].cardIcon = CardFrontImages[i];
            itemList[i].cardName = CardFrontImages[i].name;

            string cardNameString = itemList[i].cardName;
            if (cardNameString.StartsWith("A"))
            {
                itemList[i].m_cardRank = 1;
            }
            if (cardNameString.StartsWith("2"))
            {
                itemList[i].m_cardRank = 2;
            }
            else if (cardNameString.StartsWith("3"))
            {
                itemList[i].m_cardRank = 3;
            }
            else if (cardNameString.StartsWith("4"))
            {
                itemList[i].m_cardRank = 4;
            }
            else if (cardNameString.StartsWith("5"))
            {
                itemList[i].m_cardRank = 5;
            }
            else if (cardNameString.StartsWith("6"))
            {
                itemList[i].m_cardRank = 6;
            }
            else if (cardNameString.StartsWith("7"))
            {
                itemList[i].m_cardRank = 7;
            }
            else if (cardNameString.StartsWith("8"))
            {
                itemList[i].m_cardRank = 8;
            }
            else if (cardNameString.StartsWith("9"))
            {
                itemList[i].m_cardRank = 9;
            }
            else if (cardNameString.StartsWith("10"))
            {
                itemList[i].m_cardRank = 10;
            }
            else if (cardNameString.StartsWith("J"))
            {
                itemList[i].m_cardRank = 11;
            }
            else if (cardNameString.StartsWith("Q"))
            {
                itemList[i].m_cardRank = 12;
            }
            else if (cardNameString.StartsWith("K"))
            {
                itemList[i].m_cardRank = 13;
            }

            if (cardNameString.EndsWith("hearts"))
                {
                itemList[i].m_cardSuit = 1;
            }
            else if (cardNameString.EndsWith("spades"))
            {
                itemList[i].m_cardSuit = 2;
            }
            else if (cardNameString.EndsWith("diamonds"))
            {
                itemList[i].m_cardSuit = 3;
            }
            else if (cardNameString.EndsWith("clubs"))
            {
                itemList[i].m_cardSuit = 4;
            }


        }


        Debug.Log(CardFrontImages.Length);
    }

    public List<CardItem> ShuffleDeck()
    {
        List<CardItem> shuffledList = new List<CardItem>();
        for (int i = itemList.Count; i > 0;)
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

        for (int i = 0; i < _numberOfCards; i++)
        {
            newHand.Add(itemList[0]);
            RemoveOneCardFromList(0);
        }
        return newHand;
    }
}
