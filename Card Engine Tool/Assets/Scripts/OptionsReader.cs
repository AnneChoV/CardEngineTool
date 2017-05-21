using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OptionsReader : MonoBehaviour {

    TextAsset CSV_File;

    public string[,] CSVData;


    private void Start()
    {
        GetNumberOfCards();
    }

    public int GetNumberOfCards()
    {
        CSV_File = (TextAsset)Resources.Load("CSV - Sheet1");

        CSVData = CSVFileReader.ReadData(CSV_File);


        Debug.Log(CSVData.Length);
        int iCSVData = int.Parse(CSVData[0, 0]);


        //Debug.Log(CSVFileReader.WriteData(CSVData));

        return iCSVData;
    }



}
