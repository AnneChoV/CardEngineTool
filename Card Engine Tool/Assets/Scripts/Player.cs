using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool m_CanPlay = true;
    public bool m_InPlay = false;

    public GameManager m_GameManager;

    protected List<Card> m_Hand;

    private void Awake()
    {
        m_Hand = new List<Card>();
    }

    public virtual void InitializeGame(List<Card> _newHand)
    {
        SetHand(_newHand);
    }

    public virtual void SetHand(List<Card> _newHand)
    {
        m_Hand = _newHand;
        for (int i = 0; i < _newHand.Count; i++)
        {
            _newHand[i].transform.parent = transform;
            RefreshHandDisplay();
        }
    }

    public virtual void RefreshHandDisplay()
    {    }

    public virtual bool StartTurn()
    {
        if (m_InPlay || !m_CanPlay)
        {
            return false;
        }
        m_InPlay = true;
        return true;
    }

    public virtual bool EndTurn()
    {
        if (!m_InPlay || !m_CanPlay)
        {
            return false;
        }
        m_InPlay = false;
        return true;
    }

    public virtual bool EndGame()
    {
        if (!m_CanPlay)
        {
            return false;
        }
        EndTurn();
        m_CanPlay = false;
        return true;
    }

    public virtual bool DrawCard()
    {
        Card drawnCard = m_GameManager.DrawFromDeck();
        if (drawnCard)
        {
            AddCardToHand(drawnCard);
            return true;
        }
        else
        {
            if (m_Hand.Count == 0)
            {
                EndGame();
            }
            return false;
        }
    }

    public virtual void PlayCard(int _index)
    {
        if (m_Hand.Count <= _index)
        {
            return;
        }

        Card card = m_Hand[_index];
        m_Hand.RemoveAt(_index);
        Destroy(card.gameObject);
    }

    public virtual Card HandOverCard(int _index)
    {
        if (m_Hand.Count <= _index)
        {
            return null;
        }

        Card cardToHandOver = m_Hand[_index];
        m_Hand.RemoveAt(_index);
        return cardToHandOver;   
    }

    public virtual void AddCardToHand(Card _card)
    {
        m_Hand.Add(_card);
        _card.transform.parent = transform;
        RefreshHandDisplay();
    }

    public virtual void AddCardToHand(List<Card> _cards)
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            AddCardToHand(_cards[i]);
        }
    }

    public int GetCurrentHandSize()
    {
        return m_Hand.Count;
    }

    //public List<SimpleCardItem> m_CardHand;

    //public int CheckAmountOfCardsInHandOfRank(int _rankNumber)
    //{
    //    int counter = 0;

    //    for (int i = 0; i < m_CardHand.Count; i++)
    //    {
    //        if (m_CardHand[i].m_CardRank == _rankNumber)
    //        {
    //            counter++;
    //        }
    //    }
    //    return counter;
    //}

    //public void CheckHasEnoughCardsOfSameRank(int _rankNumber, int numberNeeded)
    //{
    //    List<int> foundCardIndexes = new List<int>();

    //    for (int i = m_CardHand.Count; i > 0; i--)  //Go through hand
    //    {
    //        if (m_CardHand[i].m_CardRank == _rankNumber) //Check if the names of the card is the same
    //        {
    //            foundCardIndexes.Add(i);    //if they are add it to the list
    //            if(foundCardIndexes.Count == numberNeeded)  //if we have found enough cards of the same name
    //            {
    //                for (int x = 0; i < numberNeeded; i++)  //go through and remove all of them using the saved indexes
    //                {
    //                    m_CardHand.RemoveAt(foundCardIndexes[x]);
    //                    foundCardIndexes.Clear();
    //                }
    //            }
    //        }
    //    }
    //}

    //public void PickUpCard(SimpleCardItem card)
    //{
    //    m_CardHand.Add(card);
    //}

}
