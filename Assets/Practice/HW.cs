using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW : MonoBehaviour
{
    [SerializeField] string[] texts;
    [SerializeField] string text;
    [SerializeField] int duplicants;

    void OnValidate()
    {

    }

    void Start()
    {
        Flip(texts);
    }

    void Flip(string[] texts)
    {
        string[] newTexts = new string[texts.Length];
        for (int i = 0; i < texts.Length; i++)
        {
            newTexts[texts.Length - 1 - i] = texts[i];
        }
        for (int i = 0; i < texts.Length; i++)
            texts[i] = newTexts[i];

    }


    int DuplicantCount(string text)
    {
        List<char> alreadyUsed = new List<char>();
        List<char> alreadyCounted = new List<char>();

        int duplicantCount = 0;

        foreach (char c in text)
        {
            if (alreadyUsed.Contains(c))
            {
                alreadyUsed.Add(c);
            }
            else
            {
                if (alreadyCounted.Contains(c))
                {
                    duplicantCount++;
                    alreadyCounted.Add(c);
                }
            }
        }
        return duplicants;
    
    }



}
