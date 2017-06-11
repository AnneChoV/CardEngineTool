using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public static class DeckBuilder
{
    private enum StandardDeckSuit
    {
        HEARTS,
        SPADES,
        DIAMONDS,
        CLUBS
    }

    private enum StandardDeckValue
    {
        ACE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING
    }

    private static string m_StandardDeckImagePath = "CardImages/";

    [MenuItem("Assets/Create/Standard Deck Asset")]
    public static void BuildStandardDeck()
    {
        GameObject go = new GameObject("Standard Deck");
        Deck d = go.AddComponent<Deck>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                GameObject go2 = GameObject.Instantiate(Resources.Load<GameObject>("CardPrefab/CardPrefab"));
                string fullName = ((StandardDeckValue)j).ToString().ToPascalCase() + " Of " + ((StandardDeckSuit)i).ToString().ToPascalCase();
                go2.name = fullName;
                go2.transform.parent = go.transform;

                Card card = go2.GetComponent<Card>();

                // If you want to build a standard deck, add suit, value, and image (the parameters below) to the CardData class
                card.m_CardData.m_Name = fullName;
                card.m_CardData.m_Suit = i;
                card.m_CardData.m_Rank = j + 1;
                card.m_CardData.m_Image = Resources.Load<Sprite>(m_StandardDeckImagePath + fullName);

                Image cardSR = card.GetComponent<Image>();
                cardSR.sprite = card.m_CardData.m_Image;

                d.AddCardToTop(card);
            }
        }
        PrefabUtility.CreatePrefab("Assets/Prefabs/Decks/Standard Deck Asset.prefab", go);
        GameObject.DestroyImmediate(go);
    }

    public static string ToPascalCase(this string _s)
    {
        // If there are 0 or 1 characters, just return the string.
        if (_s == null)
        {
            return _s;
        }
        if (_s.Length < 2)
        {
            return _s.ToUpper();
        }

        // Split the string into words.
        string[] words = _s.Split(
            new char[] { },
            System.StringSplitOptions.RemoveEmptyEntries);

        // Combine the words.
        string result = "";
        foreach (string word in words)
        {
            result +=
                word.Substring(0, 1).ToUpper() +
                word.Substring(1).ToLower();
        }

        return result;
    }
}

//public class CreateStandardDeck : MonoBehaviour {

//    private int numberOfCards = 52;

//    public Sprite[] CardFrontImages;

//    int rank;
//    int suit;

//    // Use this for initialization
//    void Start () {
//        CreateNewItemList();

//    }
	
//	// Update is called once per frame
//	void Update () {
		
//	}

//    void CreateNewItemList()
//    {
//        // Start at 1 because it's easier to read
//        //rank = 1;
//        //suit = 1;

//        DeckOfCards deckOfCards = new DeckOfCards();

//        deckOfCards = CreateDeckOfCards.Create();

//        if (deckOfCards)
//        {
//            deckOfCards.itemList = new List<CardItem>();
//        }

//        //for (int i = 0; i < numberOfCards; i++)
//        //{
//        //    CardItem cardItem = new CardItem();

//        //    if (rank > 13)
//        //    {
//        //        Debug.Log("Rank" + rank);
//        //        rank = 1;
//        //        suit++;

//        //        if (suit > 4) // should never reach
//        //        {
//        //            Debug.Log("asdfdsf");
//        //            suit = 1;
//        //        }
//        //    }

//        //    cardItem.m_cardRank = rank;
//        //    rank++;

//        //    cardItem.m_cardSuit = suit;


//        //    deckOfCards.itemList.Add(cardItem);
//        //}

//        CardFrontImages = Resources.LoadAll<Sprite>("");
//        deckOfCards.itemList.Clear();
//        for (int i = 0; i < CardFrontImages.Length; i++)
//        {
//            SimpleCardItem card = new SimpleCardItem();

//            card.m_CardImage = CardFrontImages[i];

//            string cardNameString = CardFrontImages[i].name;
//            card.m_CardName = cardNameString;

//            if (cardNameString.StartsWith("A"))
//            {
//                card.m_CardRank = 1;
//            }
//            if (cardNameString.StartsWith("2"))
//            {
//                card.m_CardRank = 2;
//            }
//            else if (cardNameString.StartsWith("3"))
//            {
//                card.m_CardRank = 3;
//            }
//            else if (cardNameString.StartsWith("4"))
//            {
//                card.m_CardRank = 4;
//            }
//            else if (cardNameString.StartsWith("5"))
//            {
//                card.m_CardRank = 5;
//            }
//            else if (cardNameString.StartsWith("6"))
//            {
//                card.m_CardRank = 6;
//            }
//            else if (cardNameString.StartsWith("7"))
//            {
//                card.m_CardRank = 7;
//            }
//            else if (cardNameString.StartsWith("8"))
//            {
//                card.m_CardRank = 8;
//            }
//            else if (cardNameString.StartsWith("9"))
//            {
//                card.m_CardRank = 9;
//            }
//            else if (cardNameString.StartsWith("10"))
//            {
//                card.m_CardRank = 10;
//            }
//            else if (cardNameString.StartsWith("J"))
//            {
//                card.m_CardRank = 11;
//            }
//            else if (cardNameString.StartsWith("Q"))
//            {
//                card.m_CardRank = 12;
//            }
//            else if (cardNameString.StartsWith("K"))
//            {
//                card.m_CardRank = 13;
//            }

//            // SUITS
//            if (cardNameString.EndsWith("hearts"))
//            {
//                card.m_CardSuit = 1;
//            }
//            else if (cardNameString.EndsWith("spades"))
//            {
//                card.m_CardSuit = 2;
//            }
//            else if (cardNameString.EndsWith("diamonds"))
//            {
//                card.m_CardSuit = 3;
//            }
//            else if (cardNameString.EndsWith("clubs"))
//            {
//                card.m_CardSuit = 4;
//            }

//            deckOfCards.itemList.Add(card);
//        }
//    }
//}
