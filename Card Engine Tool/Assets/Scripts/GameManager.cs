using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[System.Serializable]
public class GameTurn
{
    public List<int> PlayerTurns;
}

public class GameManager : MonoBehaviour
{
    public bool m_InProgress = true;
    public Player[] m_Players;
    public GameObject[] m_Decks;

    public List<GameTurn> m_OrderPattern;
    public int m_CurrentTurn = 0;
    public Text turnOrderText;

    protected Deck[] m_InstancedDecks;

    // GAME FLOW FUNCTIONS
    protected virtual void Awake()
    {
        // Error checking
        if (m_Players.Length <= 0)
        {
            Debug.LogError("At least one Player is required for the Game Manager to operate.");
        }
        if (m_Decks.Length <= 0)
        {
            Debug.LogError("At least one Deck is required for the Game Manager to operate.");
        }
        //if (m_OrderPattern.Count <= 0)
        //{
        //    Debug.LogError("At least one Turn must be defined for the Game Manager to operate.");
        //}
        if (m_OrderPattern.Count <=0)
        {
            Debug.LogError("At least one Turn must be defined for the Game Manager to operate.");
        }

        InitializeGame();
    }

    protected virtual void OnValidate()
    {
        for (int i = 0; i < m_Decks.Length; i++)
        {
            if (m_Decks[i] && !m_Decks[i].GetComponent<Deck>())
            {
                Debug.LogError("All decks added must have a deck/deck-inheriting class.");
                m_Decks[i] = null;
            }
        }
    }

    protected virtual void Update()
    {
        if (m_InProgress)
        {
            if (TurnNaturallyEnded() || ForceTurnEnd())
            {
                FullyEndTurn();
                StartNewTurn();
            }

            if (CheckForSimpleWin() || ForceGameEnd())
            {
                FullyEndGame();
            }
        }
        turnOrderText.text = "Player " + (m_CurrentTurn + 1) + "'s turn";
    }

    //INITIALIZATION FUNCTIONS:

    //The first function of note is the InitializeGame function. 
    //This is used to call the deck initializations and the player initializations and to start the first turn. 
    protected virtual void InitializeGame()
    {
        InitializeDecks();
        InitializePlayers();
        StartNewTurn();
    }

    //The default InitializeDecks is used to instantiate and shuffle all the decks that the game uses. 
    //If the user wishes to Initialize Decks somewhere that isn’t game startup or doesn’t want them shuffled, they can override the function as needed.
    protected virtual void InitializeDecks()
    {
        m_InstancedDecks = new Deck[m_Decks.Length];
        for (int i = 0; i < m_Decks.Length; i++)
        {
            GameObject go = Instantiate(m_Decks[i], transform);
            m_InstancedDecks[i] = go.GetComponent<Deck>();
        }

        for (int i = 0; i < m_InstancedDecks.Length; i++)
        {
            m_InstancedDecks[i].ShuffleCards();
        }
    }

    //The default InitializePlayers calls the PlayerScripts own Initialize Game on each player. 
    //By default, it passes a hand cards to each player via the Create Hand Function (see below) that the Player Script will use to set up its’ hand.
    protected virtual void InitializePlayers()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            m_Players[i].InitializeGame(CreateHand(7));
        }
    }

    //TURN FUNCTIONS:

        //By default, StartNewTurn reads the current turn and compares it to the order pattern in order to envoke the StartTurn() function of the players who are having their current turn.
    public virtual void StartNewTurn()
    {
        int order = m_CurrentTurn % m_OrderPattern.Count;
        for (int i = 0; i < m_OrderPattern[order].PlayerTurns.Count; i++)
        {
            m_Players[m_OrderPattern[order].PlayerTurns[i]].StartTurn();
        }
    }


    //ForceTurnEnd is checked every frame in the Game Manager Update to see whether the current game turn should end (aka when it returns true). 
    //By default, it only returns false, however, the user can override this to end turns (by making it return true) when needed. 
    //If the user, for example, wanted timed turns, they could override the function to return true after a timer reaches the max time state.
    public virtual bool ForceTurnEnd()
    {
        // If we wanted to end turns early via timer would do so here
        return false;
    }

    //This function which doesn’t support overriding functionality automatically returns true if there are no current players in play, aka, when the turn has ended naturally.
    public bool TurnNaturallyEnded()
    {
        int order = m_CurrentTurn % m_OrderPattern.Count;

        for (int i = 0; i < m_OrderPattern[order].PlayerTurns.Count; i++)
        {
            if (m_Players[m_OrderPattern[order].PlayerTurns[i]].m_InPlay)
            {
                return false;
            }
        }
        return true;
    }

    //This function causes all the players to call their EndTurn function, and is recommended to be used purposely every turn end to end all players turns as it serves as a cheap failsafe. 
    //It is the only function which increments the current turn amount, which the turn system relies upon. 
    //The recommended overriding usage case of this would call the base functionality and then add turn ending aesthetics and mechanics on top.
    public virtual void FullyEndTurn()
    {
        int order = m_CurrentTurn % m_OrderPattern.Count;
        for (int i = 0; i < m_OrderPattern[order].PlayerTurns.Count; i++)
        {
            m_Players[m_OrderPattern[order].PlayerTurns[i]].EndTurn();
        }
        m_CurrentTurn++;
    }

    //GAME END/WINSTATE FUNCTIONS:

    //This function returns the winner of the game according to simple win conditions. Currently, it returns true if there is only one player left in the game (used for something like a battlecard game). 
    //But it could be overriden to check, for example, a player has reached 21 points in BlackJack, or if only one player is no longer playing (like in last card).
    public virtual Player CheckForSimpleWin()
    {
        Player winner = null;
        for (int i = 0; i < m_Players.Length; i++)
        {
            if (m_Players[i].m_CanPlay)
            {
                if (winner)
                {
                    return null;
                }
                else
                {
                    winner = m_Players[i];
                }
            }
        }
        return winner;
    }

    //This function can be overriden to return true to end the game early(IE before all the players are knocked out). 
    //It returns false by default.
    public virtual bool ForceGameEnd()
    {
        //E.G:
        //if (CheckIfAllDecksEmpty())
        //{
        //    return true;
        //}

        return false;
    }

    //This function interates through the players and causes them to use their EndGame() function. 
    //It also sets the m_Inprogress variable to false which identifies that the game is over. 
    //This function can be overridden to make animations or scene changes happen when the game ends.
    public virtual void FullyEndGame()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            m_Players[i].EndGame();
        }
        m_InProgress = false;
    }

    //CARD AND DECK FUNCTIONS:

    //CreateHand returns a randomly picked array of the same size as the _handSize from the deck passed in through deck index (it default picks from Deck 0). 
    //It also removes these cards from the deck.
    public virtual List<Card> CreateHand(int _handSize, int _deckIndex = 0)
    {
        List<Card> newHand = new List<Card>();
        for (int i = 0; i < _handSize; i++)
        {
            if (m_InstancedDecks[_deckIndex].cards.Count > 0)
            {
                Card randomCard = (m_InstancedDecks[_deckIndex].DrawRandomCard());
                if (randomCard)
                {
                    newHand.Add(randomCard);
                }
            }
        }
        return newHand;
    }

    //Draw from deck calls the DrawRandomCard function from the deck, which returns a random card and deletes it from the deck.
    public virtual Card DrawFromDeck(int _deckIndex = 0)
    {
        return m_InstancedDecks[_deckIndex].DrawRandomCard();
    }

    //Returns true if any of the decks are empty.
    public bool CheckIfAnyDecksAreEmpty()
    {
        for (int i = 0; i < m_Decks.Length; i++)
        {
            if (m_InstancedDecks[i].CheckIsEmpty())
                return true;
        }
        return false;
    }

    //This function checks and returns the number of empty decks in the decks array
    public int CheckNumberOfDecksEmpty()
    {
        int counter = 0;
        for (int i = 0; i < m_Decks.Length; i++)
        {
            if (m_InstancedDecks[i].CheckIsEmpty())
                counter++;
        }
        return counter;
    }

    //Returns true only if all decks have no cards left
    public bool CheckIfAllDecksEmpty()
    {
        return CheckNumberOfDecksEmpty() == m_Decks.Length;
    }
}
