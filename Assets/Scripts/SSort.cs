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

    ////Selection sort algorithm
    public override IEnumerator Sort(CubeController[] array, int left, int right)
    {
        //iterate through whole array
        for (int i = 0; i < array.Length - 1; i++)
        {
            foreach (CubeController cube in array) cube.ResetCubeColor(); //reset not in place cubes for new iteration

            int smallest = i; //set smallest cube to first in array
            array[smallest].SetCubeToColor(Color.magenta); //mark as magenta smallest cube

            //Set new sortingMachine location, middle of two cubes to pickup
            sortingController.SetNewLocation(array[i].transform.position.x);
            yield return new WaitForSeconds(sortSpeed);

            //Pickup first smallest cube
            sortingController.PickupCube(array[i]);
            yield return new WaitForSeconds(sortSpeed);

            for (int j = i + 1; j < array.Length; j++)
            {
                array[j].SetCubeToColor(Color.blue); //mark as blue next cubes to check
                //Set new sortingMachine location
                sortingController.SetNewLocation(array[j].transform.position.x);
                yield return new WaitForSeconds(sortSpeed);

                //check if current cube is smaller than current smalles
                if (array[j].cubeNumber < array[smallest].cubeNumber)
                {
                    //Drop smallest cube in initial position
                    sortingController.PutDownCube(false);
                    yield return new WaitForSeconds(sortSpeed);

                    array[smallest].SetCubeToColor(Color.grey); //mark as grey previous smallest cube

                    //set currenct cube as smallest
                    smallest = j;
                    array[smallest].SetCubeToColor(Color.magenta); //mark as magenta new smallest cube

                    //Set new sortingMachine location
                    sortingController.SetNewLocation(array[smallest].transform.position.x);
                    
                    //Pickup new smallest cube
                    sortingController.PickupCube(array[j]);
                    yield return new WaitForSeconds(sortSpeed);
                }
                else array[j].SetCubeToColor(Color.grey); //mark as grey cubes already checked
            }

            array[smallest].SetCubeToColor(Color.red); //mark as red cubes to move
            array[i].SetCubeToColor(Color.red);  //mark as red cubes to move

            //Put down second cube in first cube position
            if (i == smallest) sortingController.PutDownCube(false);
            else
            {
                //Set new sortingMachine location to nextPickupCube location
                sortingController.SetNewLocation(array[i].transform.position.x);
                //Pickup next cube to swap places with
                sortingController.PickupCube(array[i]);
                yield return new WaitForSeconds(sortSpeed);

                //Put down first cube in second cube position
                sortingController.PutDownCube(true);
                yield return new WaitForSeconds(sortSpeed);

                //Put down second cube in first cube position
                sortingController.PutDownCube(true);

                array[smallest].SetCubeToColor(Color.grey);  //mark as grey cubes already moved

                //Swaps positions of cubes in given array
                CubeController temp = array[smallest];
                array[smallest] = array[i];
                array[i] = temp;

                swapCount++;
                gameUI.UpdateSwapCounterText(swapCount);
            }

            array[i].SetCubeToColor(Color.green); //mark as green cube in place
            yield return new WaitForSeconds(sortSpeed);
        }
        array[array.Length - 1].SetCubeToColor(Color.green); //mark as green last cube when iteration is over
    }
}
