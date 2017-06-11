using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    public void ShuffleCards()
    {
        List<Card> newCardList = new List<Card>();
        for (int i = cards.Count; i > 0; i--)
        {
            int randomNumber = Random.Range(0, cards.Count); //Gets a random index from cards
            newCardList.Add(cards[randomNumber]);            //Adds it to the new lst
            cards.RemoveAt(randomNumber);              //Removes from old list
        }

        cards = newCardList;
    }

    public Card DrawCardFromTop()
    {
        Card topCard = cards[0];
        cards.RemoveAt(0);
        return topCard;
    }

    public Card DrawCardFromBottom()
    {
        Card bottomCard = cards[cards.Count];
        cards.RemoveAt(cards.Count);
        return bottomCard;
    }

    public Card DrawCardFromPosition(int _i)
    {
        if (_i >= cards.Count || _i < 0)
        {
            Debug.LogError("Cannot draw from that position!");
            return null;
        }
        Card card = cards[_i];
        cards.RemoveAt(_i);
        return card;
    }

    public Card DrawRandomCard()
    {
        if (cards.Count == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, cards.Count);
        Card randomCard = cards[randomIndex];
        cards.RemoveAt(randomIndex);
        return randomCard;
    }

    public Card Peek(int _index)
    {
        return cards[_index];
    }

    public void AddCardToTop(Card _card)
    {
        if (cards.Count <= 0)
        {
            cards.Add(_card);
        }
        else
        {
            cards.Insert(0, _card);
        }
    }

    public void AddCardToBottom(Card _card)
    {
        cards.Add(_card);
    }

    public bool CheckIsEmpty()
    {
        if (cards.Count == 0)
        {
            return true;
        }
        return false;
    }



    //public List<Card> cards = new List<Card>();
	

    //public void PrintCardName(int i)    //untested 4/06/2017
    //{
    //    Debug.Log(cards[i].cardName);
    //}

    //public void PrintAllCards() //untested 4/06/2017
    //{
    //    string totalCardString = "Printing all cards: ";
    //    for (int i = 0; i < cards.Count; i++)
    //    {
    //        totalCardString += "Card [" + i + "]'s cardName is " + cards[i].cardName + " \n";
    //    }
    //    Debug.Log(totalCardString);
    //}


    //public List<Card> ShuffleCards()    //untested 4/06/2017
    //{
    //    List<Card> newCardList = new List<Card>();
    //    for (int i = cards.Count; i > 0; i--)
    //    {
    //        int randomNumber = Random.Range(0, cards.Count); //Gets a random index from cards
    //        newCardList.Add(cards[randomNumber]);            //Adds it to the new lst
    //        cards.RemoveRange(randomNumber, 1);              //Removes from old list
    //    }

    //    cards = newCardList;
    //    UpdateUI();

    //    return newCardList;
    //}

    //public void RemoveCard(int i) //untested 4/06/2017
    //{
    //    cards.RemoveRange(i, 1);
    //}

    //public void RemoveCard(Card removedCard) //untested 4/06/2017
    //{
    //    cards.Remove(removedCard);
    //}

    //public void UpdateUI()  //Any UI updates that need to happen aftershuffling cards etc can go here.
    //{
    //}
}
