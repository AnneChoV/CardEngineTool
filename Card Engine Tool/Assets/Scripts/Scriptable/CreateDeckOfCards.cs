using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateDeckOfCards
{
    [MenuItem("Assets/Create/Card Item List")]

    public static DeckOfCards Create()
    {
        DeckOfCards asset = ScriptableObject.CreateInstance<DeckOfCards>();

        AssetDatabase.CreateAsset(asset, "Assets/Decks/ScriptableObjects/DeckOfCards.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
