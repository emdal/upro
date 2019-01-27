using UnityEngine;
using System.Collections;
using System.Text;
using System;
using UnityEngine.UI;
using UnityEditor;

public class Utilities 
{

    public static IEnumerable ParseText(string line, char delimiter, char textQualifier)
    {

        if (line == null)
            yield break;

        else
        {
            char prevChar = '\0';
            char nextChar = '\0';
            char currentChar = '\0';

            bool inString = false;

            StringBuilder token = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                currentChar = line[i];

                if (i > 0)
                    prevChar = line[i - 1];
                else
                    prevChar = '\0';

                if (i + 1 < line.Length)
                    nextChar = line[i + 1];
                else
                    nextChar = '\0';

                if (currentChar == textQualifier && (prevChar == '\0' || prevChar == delimiter) && !inString)
                {
                    inString = true;
                    continue;
                }

                if (currentChar == textQualifier && (nextChar == '\0' || nextChar == delimiter) && inString)
                {
                    inString = false;
                    continue;
                }

                if (currentChar == delimiter && !inString)
                {
                    yield return token.ToString();
                    token = token.Remove(0, token.Length);
                    continue;
                }

                token = token.Append(currentChar);

            }

            yield return token.ToString();

        }
    }

    public static DateTime getFutureTime()
    {
        return DateTime.Now.AddYears(100);
    }


    public static int getNLineFitVertical(Text text)
    {
        int nLines = 0;
        string fakeText = "";
        TextGenerator textGen = new TextGenerator();
        while (true)
        {

            if (textGen.GetPreferredHeight(fakeText, text.GetGenerationSettings(new Vector2(0, 0))) >= text.rectTransform.rect.height)
                break;

            nLines++;
            fakeText += "s\n";
        }
        return nLines;
    }

    public static bool isPlacableOnLine(Text text, string line)
    {
        TextGenerator textGen = new TextGenerator();
        return textGen.GetPreferredWidth(line, text.GetGenerationSettings(new Vector2(0, 0))) < text.rectTransform.rect.width;
    }


    public static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;

    }
}
