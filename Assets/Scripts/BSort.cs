using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSort : SortingAlgorithm
{
    // Start is called before the first frame update
    void Start()
    {
        spawnerController.choosenAlgorithm = this;
        if (spawnerController.choosenAlgorithm != this) gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Sort(int[] array, int left, int right)
    {
        for (int j = 0; j <= array.Length - 2; j++)
        {
            for (int i = 0; i <= array.Length - 2; i++)
            {
                if (array[i] > array[i + 1])
                {
                    int temp = array[i + 1];
                    array[i + 1] = array[i];
                    array[i] = temp;
                }
            }
        }
    }
}
