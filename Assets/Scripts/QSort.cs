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

    //Quick sort algorithm
    public override IEnumerator Sort(CubeController[] array, int left, int right)
    {
        int i = left;
        int j = right;

        CubeController pivot = array[(left + right) / 2];
        pivot.SetCubeToColor(Color.magenta); // mark as magenta pivot cube

        //While left and right side are not close to eachother, check...
        while (i < j)
        {
            array[i].SetCubeToColor(Color.blue); // mark as blue next cube to check
            array[j].SetCubeToColor(Color.blue); // mark as blue next cube to check
            yield return new WaitForSeconds(sortSpeed);

            //if number in array on left is less than pivot...
            while (array[i].cubeNumber < pivot.cubeNumber)
            {
                //...move to another cube
                array[i].SetCubeToColor(Color.gray); // mark as grey cube that have been checked
                i++;
                array[i].SetCubeToColor(Color.blue); // mark as blue next cube to check
                yield return new WaitForSeconds(sortSpeed);
            }
            //if number in array on right is more than pivot...
            while (array[j].cubeNumber > pivot.cubeNumber)
            {
                //...move to another cube
                array[j].SetCubeToColor(Color.gray); //mark as grey cubes already checked
                j--;
                array[j].SetCubeToColor(Color.blue); //mark as blue next cube to check
                yield return new WaitForSeconds(sortSpeed);
            }

            //check if left index is not on right index
            if (i <= j)
            {
                if (i != j)
                {
                    swapCount++;
                    gameUI.UpdateSwapCounterText(swapCount);
                }
                //mark as blue cubes that will be moved
                array[i].SetCubeToColor(Color.red);
                array[j].SetCubeToColor(Color.red);
                yield return new WaitForSeconds(sortSpeed);

                //Swaps positions of cubes in game
                Vector3 tmp = array[i].transform.position;
                array[i].transform.position = array[j].transform.position;
                //check for pivot, leave pivot color if true
                if (array[i] != pivot) array[i].SetCubeToColor(Color.grey); //mark as grey cubes already checked
                else array[i].SetCubeToColor(Color.magenta);

                array[j].transform.position = tmp;
                //check for pivot, leave pivot color if true
                if (array[j] != pivot) array[j].SetCubeToColor(Color.grey); //mark as grey cubes already checked
                else array[i].SetCubeToColor(Color.magenta);

                //swap cubes in given array
                CubeController temp = array[i];
                array[i++] = array[j];
                array[j--] = temp;
                yield return new WaitForSeconds(sortSpeed);
            }

        }
        //reset all cubes colors for next loop iteration
        foreach (CubeController cube in array) cube.ResetCubeColor();

        //Start recursion with coroutine, to wait for both sides to finish
        yield return StartCoroutine(SortSideArrays(left, j, right, i, array));
        pivot.SetCubeToColor(Color.green); //mark as green pivot when finished
    }

    IEnumerator SortSideArrays(int left, int j, int right, int i, CubeController[] array)
    {
        //if left side is not sorted sort it from left to j
        if (left < j) yield return StartCoroutine(Sort(array, left, j));
        //if right side is not sorted sort it from i to right
        if (i < right) yield return StartCoroutine(Sort(array, i, right));
    }
}
