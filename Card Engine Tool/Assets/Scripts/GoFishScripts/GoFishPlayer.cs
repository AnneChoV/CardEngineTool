using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoFishPlayer : Player, IPointerClickHandler
{
    public Card m_SelectedCard;
    public GoFishPlayer m_SelectedPlayer;

    public int m_Score = 0;

    protected GoFishPlayer GetCurrentPlayer()
    {
        int order = m_GameManager.m_CurrentTurn % m_GameManager.m_OrderPattern.Count;
        Debug.Log("Order: " + order);
        return (GoFishPlayer)m_GameManager.m_Players[m_GameManager.m_OrderPattern[order].PlayerTurns[0]];
    }

    public void OnPointerClick(PointerEventData _data) //We need to make sure the cards that aren't the current players arent bbeing displayed.
    {
        GameObject dataGO = _data.pointerCurrentRaycast.gameObject;
        Card dataGOCard = dataGO.GetComponent<Card>();
        if (dataGOCard)
        {
            if (m_CanPlay && m_InPlay)
            {
                m_SelectedCard = dataGOCard;
            }
        }
        else
        {
            Debug.Log("sending");
            GetCurrentPlayer().SelectPlayer(dataGO);
        }
    }

    public void SelectPlayer(GameObject _dataGO)
    {
        GoFishPlayer dataGOPlayer = _dataGO.GetComponent<GoFishPlayer>();
        if (dataGOPlayer && dataGOPlayer != this)
        {
            m_SelectedPlayer = dataGOPlayer;
        }
    }

    public int CheckHowManyCardsOfRank(int _i)
    {
        int counter = 0;
        for (int j = 0; j < m_Hand.Count; j++)
        {
            if (m_Hand[j].m_CardData.m_Rank == _i)
            {
                counter++;
            }
        }
        return counter;
    }

    public bool CheckIfHasFourCardsOfRank(int _i)
    {
        if (CheckHowManyCardsOfRank(_i) == 4)
        {
            return true;
        }
        return false;
    }

    public Card RemoveOneFromHandOfRank(int _i)
    {
        for (int j = 0; j < m_Hand.Count; j++)
        {
            if (m_Hand[j].m_CardData.m_Rank == _i)
            {
                return HandOverCard(j);
            }
        }
        return null;
    }

    public bool RemoveFourCardsFromHandOfRank(int _i)
    {
        int[] cardIndexes = new int[4];
        int counter = 0;
        for (int j = 0; j < m_Hand.Count; j++)
        {
            if (m_Hand[j].m_CardData.m_Rank == _i)
            {
                cardIndexes[counter] = j;
                counter++;
            }
        }

        if (counter != 4)
        {
            Debug.Log("Something has gone wrong, there aren't four cards of the same type to remove.");
            return false;
        }

        for (int m = 3; m >= 0; m--)
        {
            PlayCard(cardIndexes[m]);
        }

        m_Score++;
        return true;
    }

    private void PerformTurn()
    {
        if (GetCardFromPlayer())
        {          
            if (m_SelectedPlayer.m_Hand.Count == 0)  //Check if enemy has cards left.
            {
                m_SelectedPlayer.SetHand(m_GameManager.CreateHand(7));
            }
        }
        else
        {
            DrawCard();
            EndTurn();
        }
        m_SelectedCard = null;
        m_SelectedPlayer = null;
    }

    public bool GetCardFromPlayer()
    {
        Card stolenCard = m_SelectedPlayer.RemoveOneFromHandOfRank(m_SelectedCard.m_CardData.m_Rank);
        if (stolenCard)
        {
            Debug.Log("Managed to steal " + stolenCard.name + " with " + m_SelectedCard);
            AddCardToHand(stolenCard);
            return true;
        }
        else
        {
            Debug.Log("Failed to steal anything with " + m_SelectedCard);
            return false;
        }
    }

    public override void RefreshHandDisplay()
    {
        Debug.Log("Refreshing");
        for (int i = 0; i < m_Hand.Count; i++)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = 250 + i * 110.0f;
            m_Hand[i].transform.position = newPosition;
        }
    }

    private void TestCardSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_SelectedCard = m_Hand[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_SelectedCard = m_Hand[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_SelectedCard = m_Hand[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_SelectedCard = m_Hand[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_SelectedCard = m_Hand[4];
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            m_SelectedCard = m_Hand[5];
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            m_SelectedCard = m_Hand[6];
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            m_SelectedCard = m_Hand[7];
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            m_SelectedCard = m_Hand[8];
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            m_SelectedCard = m_Hand[9];
        }
    }

    private void TestPlayerSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_SelectedPlayer = (GoFishPlayer)m_GameManager.m_Players[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_SelectedPlayer = (GoFishPlayer)m_GameManager.m_Players[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_SelectedPlayer = (GoFishPlayer)m_GameManager.m_Players[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_SelectedPlayer = (GoFishPlayer)m_GameManager.m_Players[3];
        }

        if (m_SelectedPlayer == this)
        {
            m_SelectedPlayer = null;
        }
    }

    protected void ResolveFourCardCondition()
    {
        for (int i = 0; i < 13; i++)
        {
            if (CheckIfHasFourCardsOfRank(i))
            {
                RemoveFourCardsFromHandOfRank(i);
                if (m_Hand.Count == 0) //Check if player has cards left.
                {
                    SetHand(m_GameManager.CreateHand(7));
                }
            }
        }
    }

    protected void Update()
    {
        if (!m_CanPlay || !m_InPlay)
        {
            return;
        }

        ResolveFourCardCondition();

        // Input
        if (!m_SelectedCard)
        {
            TestCardSelection();
        }
        //else if (!m_SelectedPlayer)
        //{
        //    TestPlayerSelection();
        //}

        if (m_SelectedCard && m_SelectedPlayer)
        {
            PerformTurn();
            ResolveFourCardCondition();
        }
    }
}
