using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardItemEditor : EditorWindow {

    public DeckOfCards deckOfCards;
    private int viewIndex = 1;

    [MenuItem ("Window/Card Editor %#e")]
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
            if (GUILayout.Button("Show Deck"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = deckOfCards;
            }
        }

        if (GUILayout.Button("Open Deck"))
        {
            OpenItemList();
        }

        if (GUILayout.Button("New Deck"))
        {
            CreateNewItemList();
            //EditorUtility.FocusProjectWindow();
            //Selection.activeObject = deckOfCards;
        }
        GUILayout.EndHorizontal();

        if (deckOfCards == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            if (GUILayout.Button("Create New Deck", GUILayout.ExpandWidth(false)))
            {
                CreateNewItemList();
            }

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
            
            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                {
                    viewIndex--;
                }
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < deckOfCards.itemList.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }

            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();

            if (deckOfCards.itemList == null)
            {
                Debug.Log("Why is this null");
            }

            if (deckOfCards.itemList.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, deckOfCards.itemList.Count);
                EditorGUILayout.LabelField("of   " + deckOfCards.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                deckOfCards.itemList[viewIndex - 1].cardName = EditorGUILayout.TextField("Item Name", deckOfCards.itemList[viewIndex - 1].cardName as string);
                deckOfCards.itemList[viewIndex - 1].cardIcon = EditorGUILayout.ObjectField("Item Icon", deckOfCards.itemList[viewIndex - 1].cardIcon, typeof(Sprite), false) as Sprite;

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
        deckOfCards = CreateDeckOfCards.Create();
        if (deckOfCards)
        {
            deckOfCards.itemList = new List<CardItem>();
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
            deckOfCards = AssetDatabase.LoadAssetAtPath(relPath, typeof(DeckOfCards)) as DeckOfCards;
            if (deckOfCards.itemList == null)
                deckOfCards.itemList = new List<CardItem>();
            if (deckOfCards)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        CardItem newItem = new CardItem();
        newItem.cardName = "New Card";
        deckOfCards.itemList.Add(newItem);
        viewIndex = deckOfCards.itemList.Count;
    }

    void DeleteItem(int index)
    {
        deckOfCards.itemList.RemoveAt(index);
    }
}