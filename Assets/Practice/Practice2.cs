using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice2 : MonoBehaviour
{
    [SerializeField] float[] values;
    [SerializeField] float output;

    void OnValidate()
    {
        output = Max(values);

        int[] myArray; 
        myArray= new int[6];

        for (int i = 0; i < myArray.Length; i++)
            myArray[i] = i + 1;

        
    }






    float Max(float[] values)
    {
        float max = 0;
        for (int i = 0; i < values.Length; i++)
        {
            float element = values[i];
            max = max > element ? max : element;
        }
        return max;
    }
    
    

}
