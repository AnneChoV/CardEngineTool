using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Deck))]
public class DeckInspector : Editor {
    Deck deck;

    private void OnEnable()
    {
        deck = (Deck)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Hello there!");

        base.OnInspectorGUI();
    }
}
