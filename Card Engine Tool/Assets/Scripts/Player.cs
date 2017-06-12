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

    //By default, this function only initializes the players hand but can be overriden to do player animations on start or have increased functionality. 
    //It has also already been overriden to allow creation without accepting a hand of cards,
    public virtual void InitializeGame(List<Card> _newHand)
    {
        SetHand(_newHand);
    }

    public virtual void InitializeGame()
    {
        //No hand Set Up.
    }


    //SetHand provides functionality for the first hand setup (because this might be different from later draws).
    //Later, AddCardToHand can be used in the same way to remake the hand
    public virtual void SetHand(List<Card> _newHand)
    {
        m_Hand = _newHand;
        for (int i = 0; i < _newHand.Count; i++)
        {
            _newHand[i].transform.SetParent(transform);// = transform;
            RefreshHandDisplay();
        }
    }

    //This function should be overridden to replace the cards in the correct place on the screen.
    public virtual void RefreshHandDisplay()
    {    }


    //This function essentially starts the turn of the player and allows them to play if they aren't already.
    //It could be overridden, for example, to draw a card on start turn.
    public virtual bool StartTurn()
    {
        if (m_InPlay || !m_CanPlay)
        {
            return false;
        }
        m_InPlay = true;
        return true;
    }

    //This function provides the end turn functionality of the player.
    public virtual bool EndTurn()
    {
        if (!m_InPlay || !m_CanPlay)
        {
            return false;
        }
        m_InPlay = false;
        return true;
    }

    //This function causes the flag on the player that represents if it’s playing to be false, meaning that the player is out of the game.
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


    //This function uses the DrawFromDeck function from the manager to draw a card from the deck and then adds it to the current hand. 
    //It also causes the players GameEnd function to be used if they have no cards left.
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

    //PlayCard removes the card from the players hand. 
    //It should be overridden in a game where the cards have functionality to use the cards functionality.
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

    //This function gets the card at the index from the hand, returns it and removes it from the hand.
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

    //This function adds the _card to the hand.
    //There is also an override to add a list of cards to the hand.
    public virtual void AddCardToHand(Card _card)
    {
        m_Hand.Add(_card);
        _card.transform.SetParent(transform);
        RefreshHandDisplay();
    }


    public virtual void AddCardToHand(List<Card> _cards)
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            AddCardToHand(_cards[i]);
        }
    }

    //This function just returns the current hand size.
    public int GetCurrentHandSize()
    {
        return m_Hand.Count;
    }
}
