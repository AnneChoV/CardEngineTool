using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[InitializeOnLoad]
public class Startup : EditorWindow {

    string myString = "Welcome to Card Engine Tool v0.1";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    
    static Startup()
    {
        EditorApplication.update += StartUp;
    }

    static void StartUp()
    {
        EditorApplication.update -= StartUp;
        ShowWindow();
    }

    // Add menu item named "Game Options" to the Window menu
    [MenuItem("Window/Start Up")]

    public static void ShowWindow()
    {
        // Show existing window instance. If one doesn't exist, create one.
        EditorWindow.GetWindow(typeof(Startup));
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }

    static void Update()
    {
        Debug.Log("Updating");
    }
}
