using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Text;
using System.IO;
public static class CSVFileReader {

    private static char LineSeperater = '\n';
    private static char fieldSeperator = ',';


    //EXAMPLE OF HOW TO USE THE FUNCTIONS:
    private static void Start()
    {
       
        //    GetNumberFromFile("CSV - Sheet1", 0, 0);        //This particular call reads from a CSV called "CSV - Sheet1" that is directly located in Resources. If you wanted a folder within resources, you could add the path to the front of the string, eg. "CSVSheets/CSV - Sheet1".



        // THE WRITER - I can't put data into one part of the thing easily, I'd have to pull all the other data, 
        // then change a value then re-write the whole text file, and that's super slow. 
        // It's faster if we later create a function that compiles all the data we have from inside the scripts into a 2D string [,] and send that to the CSV using the function I have written. 

        //Load our other file
        //string[,] dataString = ReadData((TextAsset)Resources.Load("CSV - Sheet1"));
        //Write the other file to another file.
        //WriteData("CSV - Sheet2", dataString);
    }





    //Returns all of the data from the CSV file.
    public static string[,] ReadData(TextAsset CSVtextFile)
    {
        int lineNumber = CSVtextFile.text.Split(LineSeperater).Length -1;    //Length returns a number one higher than it really is.
        int fieldsPerline;


        if (lineNumber == 1 || lineNumber == 0)
        {
            fieldsPerline = (CSVtextFile.text.Split(fieldSeperator).Length - 1);
        }
        else
        {
            fieldsPerline = (CSVtextFile.text.Split(fieldSeperator).Length - 1) / (lineNumber - 1);
        }  

        //Since there are no commas at the end of the line, lineNumber needs to be negated by one because each line is missing one.


        string[,] stringArray;
        stringArray = new string[lineNumber +1, fieldsPerline + 1];

        string[] Lines = CSVtextFile.text.Split(LineSeperater);   //Split each line into which character we are talking about.
        for (int x = 0; x <= lineNumber; x++)
        {
            string[] fields = Lines[x].Split(fieldSeperator);
            for (int y = 0; y <= fieldsPerline; y++)
            {
                //stringArray.
                stringArray[x, y] = fields[y];
            }
        }
        return stringArray;
    }

    public static string WriteData(string dataPath, string [,] dataToWrite)
    {
        string filePath = Application.dataPath + "/Resources/Saved_data2.csv";
        string textOutput = "";


        int upperBound = dataToWrite.GetLength(0) - 1;
        for (int y = 0; y < dataToWrite.GetLength(1); y++)
        {
            for (int x = 0; x < dataToWrite.GetLength(0); x++)
            {
                textOutput += dataToWrite[x, y];
                if (x != upperBound)
                {
                    textOutput += LineSeperater;
                }
            }
            textOutput += fieldSeperator;
        }

        textOutput = textOutput.Substring(0, textOutput.Length - 1);

        System.IO.File.WriteAllText(filePath, textOutput);

        return textOutput;
    }

    public static int GetNumberFromFile(string _dataSheetPath, int _column, int _row)
    {
        TextAsset CSV_File = (TextAsset)Resources.Load(_dataSheetPath);

        string[,] CSVData = CSVFileReader.ReadData(CSV_File);


        Debug.Log(CSVData.Length);
        int iCSVData = int.Parse(CSVData[_row, _column]);   //IF THEYRE THE WRONG WAY ROUND FLIP HERE.

        return iCSVData;
    }



}
