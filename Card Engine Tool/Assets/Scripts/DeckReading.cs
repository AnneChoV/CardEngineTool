using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeckReading {

    public static List<Card> MakeCardList(string _filePath)
    {
        List<Card> itemList = new List<Card>();

        string[,] CSVFile = CSVFileReader.ReadData(_filePath); //THIS NEEDS TO BE APPENDED IF WE PUT THESE IN A FILE.

        int listSize = CSVFile.GetLength(1);
        Debug.Log(listSize);

        for (int i = 2; i < listSize; i++)  //Start at the second line because this will be the first card.
        {



        }




        return itemList;
    }

	
}
