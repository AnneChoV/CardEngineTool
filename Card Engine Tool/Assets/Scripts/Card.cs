using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    // Fill variables here
    public string m_Name;

    // Go fish variables
    public int m_Suit;
    public int m_Rank;
    public Sprite m_Image;
}

[System.Serializable]
public class Card : MonoBehaviour
{
    public CardData m_CardData = new CardData();
}
