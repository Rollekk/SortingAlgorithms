using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSort : SortingAlgorithm
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Bubble sort algorithm
    public override void Sort(CubeController[] array, int left, int right)
    {
        for (int j = 0; j <= array.Length - 2; j++)
        {
            for (int i = 0; i <= array.Length - 2; i++)
            {
                if (array[i].cubeNumber > array[i + 1].cubeNumber)
                {
                    //Swaps positions of cubes in game
                    SwapPosition(array, i, i + 1);

                    //Swaps positions of cubes in given array
                    CubeController temp = array[i + 1];
                    array[i + 1] = array[i];
                    array[i] = temp;
                }
            }
        }
    }
}
