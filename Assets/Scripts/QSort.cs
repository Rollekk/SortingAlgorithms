using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSort : SortingAlgorithm
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

    //Quick sort algorithm
    public override void Sort(CubeController[] array, int left, int right)
    {
        var i = left;
        var j = right;
        var pivot = array[(left + right) / 2].cubeNumber;
        while (i < j)
        {
            while (array[i].cubeNumber < pivot) i++;
            while (array[j].cubeNumber > pivot) j--;
            if (i <= j)
            {
                //Swaps positions of cubes in game
                SwapPosition(array, i, j);

                //Swaps positions of cubes in given array
                CubeController tmp = array[i];
                array[i++] = array[j];
                array[j--] = tmp;
            }
        }
        if (left < j) Sort(array, left, j);
        if (i < right) Sort(array, i, right);
    }
}
