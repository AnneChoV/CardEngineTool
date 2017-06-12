using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoFishManager : GameManager
{
    public Text m_winnerDisplayText;
    public override bool ForceGameEnd()
    {
        if (m_InstancedDecks[0].cards.Count == 0 && CheckIfAllPlayersHandsEmpty())
        {
            GoFishPlayer winner = FindPlayerWithMostPoints();
            DisplayWinner(winner);
            return true;
        }
        return false;
    }

    public void DisplayWinner(GoFishPlayer _winner)
    {
        m_winnerDisplayText.gameObject.SetActive(true);
        m_winnerDisplayText.text = _winner.name + " wins!";
    }

    public GoFishPlayer FindPlayerWithMostPoints()
    {
        GoFishPlayer highestScorePlayer = new GoFishPlayer();
        for (int i = 0; i < m_Players.Length; i++)
        {
            GoFishPlayer gf_Player = (GoFishPlayer)m_Players[i];
            if (gf_Player.m_Score > highestScorePlayer.m_Score)
            {
                highestScorePlayer = gf_Player;
            }
        }
        return highestScorePlayer;
    }

    public bool CheckIfAllPlayersHandsEmpty()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            if (m_Players[i].GetCurrentHandSize() > 0)
            {
                return false;
            }
        }
        return true;
    }
}
