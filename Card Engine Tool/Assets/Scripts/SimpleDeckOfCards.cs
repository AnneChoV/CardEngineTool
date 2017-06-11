using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimpleDeckOfCards : ScriptableObject
{
    public List<SimpleCardItem> itemList;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Simple Card Item List")]
    public static SimpleDeckOfCards CreateDeckAsset()
    {
        SimpleDeckOfCards asset = ScriptableObject.CreateInstance<SimpleDeckOfCards>();

        AssetDatabase.CreateAsset(asset, "Assets/Decks/Simple Deck Of Cards.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
#endif

    public List<SimpleCardItem> ShuffleDeck()
    {
        List<SimpleCardItem> shuffledList = new List<SimpleCardItem>();
        for (int i = itemList.Count; i > 0; i--)
        {
            int randomNumber = Random.Range(0, itemList.Count);
            shuffledList.Add(itemList[randomNumber]);
            itemList.RemoveRange(randomNumber, 1);
        }
        itemList = shuffledList;
        return shuffledList;
    }

    public SimpleCardItem RemoveOneCardFromList(int _index)
    {
        Debug.Log("Reached here1");
        SimpleCardItem cardItem = itemList[_index];
        itemList.RemoveRange(_index, 1);
        return cardItem;
    }
    public SimpleCardItem RemoveOneCardFromList(SimpleCardItem cardItem)
    {        
        itemList.Remove(cardItem);
        return cardItem;
    }

    public List<SimpleCardItem> CreateHand(int _numberOfCards)
    {
        List<SimpleCardItem> newHand = new List<SimpleCardItem>();

        for (int i = _numberOfCards; i > 0; i--)
        {
            Debug.Log("Reachedhere2");
            newHand.Add(itemList[0]);
            Debug.Log(itemList[0]);
            RemoveOneCardFromList(0);
        }
        return newHand;
    }
}
