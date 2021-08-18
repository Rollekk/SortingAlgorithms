using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSort : SortingAlgorithm
{
    // Start is called before the first frame update
    void Start()
    {
        if (spawnerController.choosenAlgorithm != this) gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Sort(int[] array, int left, int right)
    {
        
    }
}
