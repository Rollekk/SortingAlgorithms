using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSort : SortingAlgorithm
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

    //Selection sort algorithm
    public override void Sort(CubeController[] array, int left, int right)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int smallest = i;
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j].cubeNumber < array[smallest].cubeNumber)
                {
                    smallest = j;
                }
            }
            //Swaps positions of cubes in game
            SwapPosition(array, smallest, i);

            //Swaps positions of cubes in given array
            CubeController temp = array[smallest];
            array[smallest] = array[i];
            array[i] = temp;
        }
    }
}
