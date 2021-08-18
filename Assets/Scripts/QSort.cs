using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSort : SortingAlgorithm
{
    // Start is called before the first frame update
    void Start()
    {
        //spawnerController.choosenAlgorithm = this;
        if (spawnerController.choosenAlgorithm != this) gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Quick sort algorithm
    public override void Sort(int[] array, int left, int right)
    {
        var i = left;
        var j = right;
        var pivot = array[(left + right) / 2];
        while (i < j)
        {
            while (array[i] < pivot) i++;
            while (array[j] > pivot) j--;
            if (i <= j)
            {
                // swap
                var tmp = array[i];
                array[i++] = array[j];  // ++ and -- inside array braces for shorter code
                array[j--] = tmp;
            }
        }
        if (left < j) Sort(array, left, j);
        if (i < right) Sort(array, i, right);
    }
}
