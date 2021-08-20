using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSort : SortingAlgorithm
{
    List<CubeController> grayCubes = new List<CubeController>();

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
    public override IEnumerator Sort(CubeController[] array, int left, int right)
    {
        int i = left;
        int j = right;
        CubeController pivot = array[(left + right) / 2];
        Debug.Log(pivot.cubeNumber);
        pivot.SetCubeToColor(Color.white);
        //While left and right side are not close to eachother, check...
        while (i < j)
        {
            array[i].SetCubeToColor(Color.blue);
            array[j].SetCubeToColor(Color.blue);
            yield return new WaitForSeconds(1.0f);
            //if number in array on left is less than pivot
            while (array[i].cubeNumber < pivot.cubeNumber)
            {
                //move to another
                array[i].SetCubeToColor(Color.gray);
                grayCubes.Add(array[i]);
                i++;
                array[i].SetCubeToColor(Color.blue);
                yield return new WaitForSeconds(1.0f);
            }
            //if number in array on right is more than pivot
            while (array[j].cubeNumber > pivot.cubeNumber)
            {
                //move to another
                array[j].SetCubeToColor(Color.gray);
                grayCubes.Add(array[j]);
                j--;
                array[j].SetCubeToColor(Color.blue);
                yield return new WaitForSeconds(1.0f);
            }

            array[i].ResetCubeColor();
            array[j].ResetCubeColor();

            //check if left index is not on right index
            if (i <= j)
            {
                array[i].SetCubeToColor(Color.red);
                array[j].SetCubeToColor(Color.red);
                yield return new WaitForSeconds(1.0f);

                //Swaps positions of cubes in game
                var tmp2 = array[i].transform.position;
                array[i].transform.position = array[j].transform.position;
                array[i].SetCubeToColor(Color.grey);

                array[j].transform.position = tmp2;
                array[j].SetCubeToColor(Color.grey);
                yield return new WaitForSeconds(1.0f);

                //if it's not swap elements in array
                CubeController tmp = array[i];
                array[i++] = array[j];
                array[j--] = tmp;
            }

        }

        foreach (CubeController cube in grayCubes) cube.ResetCubeColor();
        //pivot.SetCubeToColor(Color.green);

        yield return StartCoroutine(SortSideArrays(left, j, right, i, array));

        //foreach (var cube in array) cube.SetCubeToColor(Color.green);
        //if pivot changes positions dont make it green
    }

    IEnumerator SortSideArrays(int left, int j, int right, int i, CubeController[] array)
    {
        //if left side is not sorted sort it from left to j
        if (left < j) yield return StartCoroutine(Sort(array, left, j));
        //if right side is not sorted sort it from i to right
        if (i < right) yield return StartCoroutine(Sort(array, i, right));
    }
}
