using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SimpleCardItemEditor : EditorWindow {


    public SimpleDeckOfCards deckOfCards;
    private int viewIndex = 1; // this is for view scrolling through the cards

    [MenuItem("Window/Simple Card Editor %#r")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(SimpleCardItemEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            deckOfCards = (SimpleDeckOfCards)AssetDatabase.LoadAssetAtPath(objectPath, typeof(SimpleDeckOfCards));
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
            OpenItemList();
        }

        // Create a new deck
        if (GUILayout.Button("New Deck"))
        {
            CreateNewItemList();
        }
        GUILayout.EndHorizontal();

        if (deckOfCards == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            // Create a new deck
            if (GUILayout.Button("Create New Deck", GUILayout.ExpandWidth(false)))
            {
                CreateNewItemList();
            }

            // Open an existing deck
            if (GUILayout.Button("Open Existing Deck", GUILayout.ExpandWidth(false)))
            {
                OpenItemList();
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
                if (viewIndex < deckOfCards.itemList.Count)
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

            if (deckOfCards.itemList == null)
            {
                // Do nothing
            }

            else if (deckOfCards.itemList.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Card", viewIndex, GUILayout.ExpandWidth(false)), 1, deckOfCards.itemList.Count);
                EditorGUILayout.LabelField("of   " + deckOfCards.itemList.Count.ToString() + "  cards", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                SimpleCardItem card = (SimpleCardItem)(deckOfCards.itemList[viewIndex - 1]);

                card.m_CardName = EditorGUILayout.TextField("Card Name", card.m_CardName);
                card.m_CardImage = (Sprite)(EditorGUILayout.ObjectField("Card Icon", card.m_CardImage, typeof(Sprite), false));
                card.m_CardSuit = EditorGUILayout.IntField("Card Suit", card.m_CardSuit);
                card.m_CardRank = EditorGUILayout.IntField("Card Rank", card.m_CardRank);

                GUILayout.Space(10);
            }
            else
            {
                GUILayout.Label("This Deck is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(deckOfCards);
        }
    }

    void CreateNewItemList()
    {
        viewIndex = 1;
        deckOfCards = SimpleDeckOfCards.CreateDeckAsset();
        if (deckOfCards)
        {
            deckOfCards.itemList = new List<SimpleCardItem>();
            string relPath = AssetDatabase.GetAssetPath(deckOfCards);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Deck", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            deckOfCards = AssetDatabase.LoadAssetAtPath(relPath, typeof(SimpleDeckOfCards)) as SimpleDeckOfCards;
            if (deckOfCards.itemList == null)
                deckOfCards.itemList = new List<SimpleCardItem>();
            if (deckOfCards)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
            AssetDatabase.Refresh();
        }
    }

    void AddItem()
    {
        SimpleCardItem newItem = new SimpleCardItem();
        deckOfCards.itemList.Add(newItem);
        viewIndex = deckOfCards.itemList.Count;
    }

    void DeleteItem(int index)
    {
        deckOfCards.itemList.RemoveAt(index);
    }
}
