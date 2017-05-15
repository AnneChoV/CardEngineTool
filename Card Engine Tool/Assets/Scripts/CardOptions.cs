using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardOptions : MonoBehaviour {

    public Text emptyTextPrefab;
    public Image emptyImagePrefab;
    public GameObject card;

    public Text[] textElements;
    public Image[] imageElements;

    private void OnValidate()
    {
        UpdateTextElements();
        UpdateImageElements();
    }

    void UpdateTextElements()
    {
        if (card.GetComponentsInChildren<Text>().Length > textElements.Length)
        {
            int numberToDelete = card.GetComponentsInChildren<Text>().Length - textElements.Length - 1;
            for (; numberToDelete >= 0; numberToDelete--)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(card.transform.GetComponentsInChildren<Text>()[card.transform.GetComponentsInChildren<Text>().Length - 1].gameObject);
                };
            }
        }
        else
        {
            while (card.GetComponentsInChildren<Text>().Length < textElements.Length)
            {
                Instantiate(emptyTextPrefab, card.transform);
            }
        }

        UnityEditor.EditorApplication.delayCall += () =>
        {
            textElements = card.transform.GetComponentsInChildren<Text>();
        };
    }

    void UpdateImageElements()
    {
        if (card.GetComponentsInChildren<Image>().Length > imageElements.Length)
        {
            int numberToDelete = card.GetComponentsInChildren<Image>().Length - imageElements.Length - 1;
            for (; numberToDelete >= 0; numberToDelete--)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(card.transform.GetComponentsInChildren<Image>()[card.transform.GetComponentsInChildren<Image>().Length - 1].gameObject);
                };
            }
        }
        else
        {
            while (card.GetComponentsInChildren<Image>().Length < imageElements.Length)
            {
                Debug.Log("Creating");
                Instantiate(emptyImagePrefab, card.transform);
            }
        }

        UnityEditor.EditorApplication.delayCall += () =>
        {
            imageElements = card.transform.GetComponentsInChildren<Image>();
        };
    }

}



