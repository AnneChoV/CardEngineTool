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
    }

    //INITIALIZATION FUNCTIONS:
    protected virtual void InitializeGame()
    {
        InitializeDecks();
        InitializePlayers();
        StartNewTurn();
    }

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

    protected virtual void InitializePlayers()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            m_Players[i].InitializeGame(CreateHand(7));
        }
    }

    //TURN FUNCTIONS:
    public virtual bool ForceTurnEnd()
    {
        // If we wanted to end turns early via timer would do so here
        return false;
    }

    public bool TurnNaturallyEnded()
    {
        //int order = m_CurrentTurn % m_OrderPattern.Count;
        //for (int i = 0; i < m_OrderPattern[order].Count; i++)
        //{
        //    if (m_Players[m_OrderPattern[order][i]].m_InPlay)
        //    {
        //        return false;
        //    }
        //}

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

    public virtual void StartNewTurn()
    {
        //int order = m_CurrentTurn % m_OrderPattern.Count;
        //for (int i = 0; i < m_OrderPattern[order].Count; i++)
        //{
        //    m_Players[m_OrderPattern[order][i]].StartTurn();
        //}

        int order = m_CurrentTurn % m_OrderPattern.Count;
        for (int i = 0; i < m_OrderPattern[order].PlayerTurns.Count; i++)
        {
            m_Players[m_OrderPattern[order].PlayerTurns[i]].StartTurn();
        }
    }

    public virtual void FullyEndTurn()
    {
        //int order = m_CurrentTurn % m_OrderPattern.Count;
        //for (int i = 0; i < m_OrderPattern[order].Count; i++)
        //{
        //    m_Players[m_OrderPattern[order][i]].EndTurn();
        //}
        //m_CurrentTurn++;

        int order = m_CurrentTurn % m_OrderPattern.Count;
        for (int i = 0; i < m_OrderPattern[order].PlayerTurns.Count; i++)
        {
            m_Players[m_OrderPattern[order].PlayerTurns[i]].EndTurn();
        }
        m_CurrentTurn++;
    }

    //GAME END/WINSTATE FUNCTIONS:
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

    public virtual bool ForceGameEnd()
    {
        //E.G:
        //if (CheckIfAllDecksEmpty())
        //{
        //    return true;
        //}

        return false;
    }

    public virtual void FullyEndGame()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            m_Players[i].EndGame();
        }
        m_InProgress = false;
    }

    //CARD AND DECK FUNCTIONS:
    public virtual List<Card> CreateHand(int _handSize, int _deckIndex = 0)
    {
        List<Card> newHand = new List<Card>();
        for (int i = 0; i < _handSize; i++)
        {
            if (m_InstancedDecks[_deckIndex].cards.Count > 0)
            {
                Card randomCard = (m_InstancedDecks[_deckIndex].DrawRandomCard();
                if (randomCard)
                {
                    newHand.Add(randomCard);
                }
            }
        }
        return newHand;
    }

    public virtual Card DrawFromDeck(int _deckIndex = 0)
    {
        return m_InstancedDecks[_deckIndex].DrawRandomCard();
    }

    public bool CheckIfAnyDecksAreEmpty()
    {
        for (int i = 0; i < m_Decks.Length; i++)
        {
            if (m_InstancedDecks[i].CheckIsEmpty())
                return true;
        }
        return false;
    }

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

    public bool CheckIfAllDecksEmpty()
    {
        return CheckNumberOfDecksEmpty() == m_Decks.Length;
    }




    //public DeckOfCards<T> deckOfCards;
    //public Transform cardHand;
    //public Sprite kittyBack;

    //CardOptions cardOptions;
    //DeckOptions deckOptions;

    ////Deck Type
    //public TypeOfDeck DeckType;     //52 Card deck or custom

    ////DECK
    //public int numberOfDecks;
    //public int deckSize;

    ////HAND
    //public int startingHandSize;   
    //public int minimumSizeOfHand;
    //public int maximumSizeOfHand;

    ////TURNS
    //public int m_CurrentTurn;

    //public int turnOrder; //NEEDS AN ENUM TO SPECIFY HOW WE PICK TURNS. EG RANDOMIZE, IN ORDER, ETC
    //public int turnTimer;  //End the turn after this amount of time. 0 for no turn timer.


    //public DeckOfCards<CardItem> cardDeck;



    //public enum TypeOfDeck
    //{
    //    Standard52CardDeck,
    //    Custom
    //}

    //private void OnValidate()
    //{
    //    cardOptions = GetComponent<CardOptions>();
    //    deckOptions = GetComponent<DeckOptions>();


    //    if (DeckType == TypeOfDeck.Custom)
    //    {

    //    }
    //    else
    //    {

    //    }
    //}

    //private void Start()
    //{
    //    //Debug.Log("Display first few cards");

    //    for (int i = 0; i < 8; i++)
    //    {
    //        Sprite sprite = deckOfCards.itemList[i].m_CardImage;
    //        GameObject card = Instantiate(Resources.Load("TempCard"), cardHand) as GameObject;
    //        card.GetComponent<Image>().sprite = sprite;
    //    }
    //}

    //public void ChangeFace()
    //{
    //    for (int i = 0; i < cardHand.childCount; i++)
    //    {
    //        cardHand.GetChild(i).GetComponent<Image>().sprite = kittyBack;
    //    }
    //}
}
