using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoFishManager : GameManager
{
    public override bool ForceGameEnd()
    {
        if (m_InstancedDecks[0].cards.Count == 0 && CheckIfAllPlayersHandsEmpty())
        {
            return true;
        }
        return false;
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
