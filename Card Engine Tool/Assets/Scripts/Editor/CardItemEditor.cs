using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CardItemEditor : EditorWindow
{

    public DeckOfCards deckOfCards;
    [HideInInspector]
    public string DeckAssetName;
    [HideInInspector]
    public string DeckName;

    private int viewIndex = 1; // this is for view scrolling through the cards

    [MenuItem("Window/Card Editor %#e")]

    static void Init()
    {
        EditorWindow.GetWindow(typeof(CardItemEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            deckOfCards = AssetDatabase.LoadAssetAtPath(objectPath, typeof(DeckOfCards)) as DeckOfCards;
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Card Editor", EditorStyles.boldLabel);

        if (deckOfCards != null)
        {
            // Shows the currently selected deck in the inspector
            if (GUILayout.Button("Show Deck"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = deckOfCards;
            }
        }

        // opens a window to select a deck
        if (GUILayout.Button("Open Deck"))
        {
            OpencardList();
        }

        // Create a new deck
        if (GUILayout.Button("New Deck"))
        {
            CreateNewcardList();
        }
        GUILayout.EndHorizontal();

        if (deckOfCards == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            // Create a new deck
            if (GUILayout.Button("Create New Deck", GUILayout.ExpandWidth(false)))
            {
                CreateNewcardList();
            }

            // Open an existing deck
            if (GUILayout.Button("Open Existing Deck", GUILayout.ExpandWidth(false)))
            {
                OpencardList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (deckOfCards != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            // scrolling through list
            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                {
                    viewIndex--;
                    AssetDatabase.Refresh();
                }
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < deckOfCards.cardList.Count)
                {
                    viewIndex++;
                    AssetDatabase.Refresh();
                }
            }

            GUILayout.Space(60);

            // add/removing cards from list
            if (GUILayout.Button("Add Card", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }

            if (GUILayout.Button("Delete Card", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();

            if (deckOfCards.cardList == null)
            {
                // Do nothing
            }

            else if (deckOfCards.cardList.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Card", viewIndex, GUILayout.ExpandWidth(false)), 1, deckOfCards.cardList.Count);
                EditorGUILayout.LabelField("of   " + deckOfCards.cardList.Count.ToString() + "  cards", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                if (deckOfCards.cardList[viewIndex - 1].m_Name== null)
                {
                    deckOfCards.cardList[viewIndex - 1].m_Name = "New Card";
                }

                // Edit stuff here !!!!!!!!!!!!!!!!!!!!!!!!!!!
                deckOfCards.cardList[viewIndex - 1].m_Name = EditorGUILayout.TextField("Card Name", deckOfCards.cardList[viewIndex - 1].m_Name as string);
                deckOfCards.cardList[viewIndex - 1].m_Rank = EditorGUILayout.IntField("Card Rank", deckOfCards.cardList[viewIndex - 1].m_Rank);
                deckOfCards.cardList[viewIndex - 1].m_Suit = EditorGUILayout.IntField("Card Suit", deckOfCards.cardList[viewIndex - 1].m_Suit);
                deckOfCards.cardList[viewIndex - 1].m_Image = EditorGUILayout.ObjectField("Card Icon", deckOfCards.cardList[viewIndex - 1].m_Image, typeof(Sprite), false) as Sprite;


                GUILayout.Space(10);

                DeckAssetName = EditorGUILayout.TextField("Deck Asset Name", DeckAssetName);

                GUILayout.Space(10);
            }
            else
            {
                GUILayout.Label("This Deck is Empty.");
            }

            GUILayout.Space(30);

            // add/removing cards from list
            if (GUILayout.Button("Create Deck Asset", GUILayout.ExpandWidth(false)))
            {
                CreateDeckAsset();
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(deckOfCards);
        }
    }

    void CreateNewcardList()
    {
        viewIndex = 1;
        deckOfCards = CreateDeckOfCards.Create();
        if (deckOfCards)
        {
            deckOfCards.cardList = new List<CardItem>();
            string relPath = AssetDatabase.GetAssetPath(deckOfCards);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpencardList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Deck", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            deckOfCards = AssetDatabase.LoadAssetAtPath(relPath, typeof(DeckOfCards)) as DeckOfCards;
            if (deckOfCards.cardList == null)
                deckOfCards.cardList = new List<CardItem>();
            if (deckOfCards)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
            AssetDatabase.Refresh();
        }
    }

    void AddItem()
    {
        CardItem newItem = new CardItem();
        deckOfCards.cardList.Add(newItem);
        viewIndex = deckOfCards.cardList.Count;
    }

    void DeleteItem(int index)
    {
        deckOfCards.cardList.RemoveAt(index);
    }

    void CreateDeckAsset()
    {
        GameObject DeckGO = new GameObject("Standard Deck");
        Deck d = DeckGO.AddComponent<Deck>();
        for (int i = 0; i < deckOfCards.cardList.Count; i++)
        {
                GameObject CardGO = Instantiate(Resources.Load<GameObject>("CardPrefab/CardPrefab"));
                string fullName = deckOfCards.cardList[i].m_Name;
                CardGO.name = fullName;
                CardGO.transform.parent = DeckGO.transform;

                Card card = CardGO.GetComponent<Card>();

            // If you want to build a standard deck, add suit, value, and image (the parameters below) to the CardData class
            card.m_CardData.m_Name = deckOfCards.cardList[i].m_Name;
            card.m_CardData.m_Suit = deckOfCards.cardList[i].m_Suit;
            card.m_CardData.m_Rank = deckOfCards.cardList[i].m_Rank;
            card.m_CardData.m_Image = deckOfCards.cardList[i].m_Image;

            Image cardSR = card.GetComponent<Image>();
            cardSR.sprite = card.m_CardData.m_Image;

            d.AddCardToTop(card);
        }
        PrefabUtility.CreatePrefab( "Assets/Prefabs/Decks/" + DeckAssetName + ".prefab", DeckGO);
        GameObject.DestroyImmediate(DeckGO);
    }
}