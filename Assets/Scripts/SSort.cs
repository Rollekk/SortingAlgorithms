using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSort : SortingAlgorithm
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
        for (int i = 0; i < array.Length - 1; i++)
        {
            int smallest = i;
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < array[smallest])
                {
                    smallest = j;
                }
            }
            int temp = array[smallest];
            array[smallest] = array[i];
            array[i] = temp;
        }
    }
}
