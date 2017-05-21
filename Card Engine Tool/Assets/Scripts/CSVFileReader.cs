using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Text;
using System.IO;
public static class CSVFileReader {

    private static char LineSeperater = '\n';
    private static char fieldSeperator = ',';

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

        Debug.Log(lineNumber);
        Debug.Log(fieldsPerline);


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

    public static string WriteData(string [,] dataToWrite)
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



}
