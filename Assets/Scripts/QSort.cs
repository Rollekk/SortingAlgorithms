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

        //Set new sortingMachine location, middle of two cubes to pickup
        sortingController.SetNewLocation(pivot.transform.position.x);
        yield return new WaitForSeconds(sortSpeed);

        //While left and right side are not close to eachother, check...
        while (i < j)
        {
            array[i].SetCubeToColor(Color.blue); // mark as blue next cube to check
            array[j].SetCubeToColor(Color.blue); // mark as blue next cube to check

            //Set new sortingMachine location
            sortingController.SetNewLocation(array[i].transform.position.x);
            yield return new WaitForSeconds(sortSpeed);

            //Pickup first cube on left
            sortingController.PickupCube(array[i], true);
            yield return new WaitForSeconds(sortSpeed);

            //Set new sortingMachine location, middle of two cubes to pickup
            sortingController.SetNewLocation(array[j].transform.position.x);
            yield return new WaitForSeconds(sortSpeed);

            //Pickup first cube on right
            sortingController.PickupCube(array[j], false);
            yield return new WaitForSeconds(sortSpeed);

            //Add and update comparison counter
            swapCount++;
            gameUI.UpdateSwapCounterText(swapCount);

            //if number in array on left is less than pivot...
            while (array[i].cubeNumber < pivot.cubeNumber)
            {
                //Drop current cube
                sortingController.PutDownCube(false, true);
                yield return new WaitForSeconds(sortSpeed);

                //...move to another cube
                array[i].SetCubeToColor(Color.gray); // mark as grey cube that have been checked
                i++;
                array[i].SetCubeToColor(Color.blue); // mark as blue next cube to check

                //check if should put down cube...
                if (i >= j)
                {
                    sortingController.PutDownCube(array[i]);
                    yield return new WaitForSeconds(sortSpeed);
                }
                else //or pick another one to compare
                {
                    //Set new sortingMachine location
                    sortingController.SetNewLocation(array[i].transform.position.x);
                    yield return new WaitForSeconds(sortSpeed);

                    //Pickup first smallest cube
                    sortingController.PickupCube(array[i], true);
                    //Add and update comparison counter
                    swapCount++;
                    gameUI.UpdateSwapCounterText(swapCount);
                    yield return new WaitForSeconds(sortSpeed);
                }
            }
            //if number in array on right is more than pivot...
            while (array[j].cubeNumber > pivot.cubeNumber)
            {
                //Drop current cube
                sortingController.PutDownCube(false, false);
                yield return new WaitForSeconds(sortSpeed);
                //is on left, picks up to right and drops it on next location
                //...move to another cube
                array[j].SetCubeToColor(Color.gray); //mark as grey cubes already checked
                j--;
                array[j].SetCubeToColor(Color.blue); //mark as blue next cube to check

                if(j <= i)
                {
                    //Pickup first smallest cube
                    sortingController.PutDownCube(array[j]);
                    yield return new WaitForSeconds(sortSpeed);
                }
                else
                {
                    //Set new sortingMachine location
                    sortingController.SetNewLocation(array[j].transform.position.x);
                    yield return new WaitForSeconds(sortSpeed);

                    //Pickup first smallest cube
                    sortingController.PickupCube(array[j], false);
                    //Add and update comparison counter
                    swapCount++;
                    gameUI.UpdateSwapCounterText(swapCount);
                    yield return new WaitForSeconds(sortSpeed);
                }
            }

            //check if left index is not on right index
            if (i <= j)
            {
                //mark as blue cubes that will be moved
                array[i].SetCubeToColor(Color.red);
                array[j].SetCubeToColor(Color.red);
                yield return new WaitForSeconds(sortSpeed);

                //Put down first cube in second cube position
                sortingController.PutDownCube(true, true);
                yield return new WaitForSeconds(sortSpeed);

                //Put down second cube in first cube position
                sortingController.PutDownCube(true, false);
                yield return new WaitForSeconds(sortSpeed);

                //check for pivot, leave pivot color if true
                if (array[i] != pivot) array[i].SetCubeToColor(Color.grey); //mark as grey cubes already checked
                else array[i].SetCubeToColor(Color.magenta);

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

        //mark every sorted cube as green
        for(int k = 0; k <= System.Array.IndexOf(array, pivot); k++) array[k].SetCubeToColor(Color.green);
    }

    //Coroutine for sorting side arrays
    IEnumerator SortSideArrays(int left, int j, int right, int i, CubeController[] array)
    {
        //if left side is not sorted sort it from left to j
        if (left < j) yield return StartCoroutine(Sort(array, left, j));
        //if right side is not sorted sort it from i to right
        if (i < right) yield return StartCoroutine(Sort(array, i, right));
    }
}
