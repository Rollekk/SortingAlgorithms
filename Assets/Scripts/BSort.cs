using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSort : SortingAlgorithm
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    //Bubble sort algorithm
    public override IEnumerator Sort(CubeController[] array, int left, int right)
    {
        for (int j = 0; j < array.Length - 1; j++)
        {
            foreach (CubeController cube in array) cube.ResetCubeColor(); //reset not in place cubes for new iteration

            for (int i = 0; i < array.Length - (1 + j); i++)
            {
                array[i].SetCubeToColor(Color.blue); //mark as blue next cubes to check
                array[i + 1].SetCubeToColor(Color.blue);  //mark as blue next cubes to check

                //Set new sortingMachine location, middle of two cubes to pickup
                sortingController.SetNewLocation((array[i].transform.position.x + array[i + 1].transform.position.x) / 2);
                yield return new WaitForSeconds(sortSpeed);

                //Pickup both cubes and place them in sorting machine
                sortingController.PickupCube(array[i]);
                sortingController.PickupCube(array[i + 1]);
                yield return new WaitForSeconds(sortSpeed);

                //Check if current cubeNumber is bigger than next one
                if (array[i].cubeNumber > array[i + 1].cubeNumber)
                {
                    array[i].SetCubeToColor(Color.red);  //mark as red cubes to move
                    array[i + 1].SetCubeToColor(Color.red); //mark as red cubes to move
                    yield return new WaitForSeconds(sortSpeed);

                    //Put down first cube in second cube position
                    sortingController.PutDownCube(true);
                    yield return new WaitForSeconds(sortSpeed);

                    //Put down second cube in first cube position
                    sortingController.PutDownCube(true);

                    //mark as grey cubes already moved
                    array[i + 1].SetCubeToColor(Color.grey);  
                    array[i].ResetCubeColor(); 

                    //Swaps positions of cubes in given array
                    CubeController temp = array[i + 1];
                    array[i + 1] = array[i];
                    array[i] = temp;
                    swapCount++;
                    gameUI.UpdateSwapCounterText(swapCount);
                }
                else
                {
                    //Put down first cube in initial position
                    sortingController.PutDownCube(false);
                    yield return new WaitForSeconds(sortSpeed);

                    //Put down second cube in initial position
                    sortingController.PutDownCube(false);

                    //mark as grey cubes already checked
                    array[i].SetCubeToColor(Color.grey); 
                }
                yield return new WaitForSeconds(sortSpeed);
                //if its last iteration, mark last cube as in place (green color)
                if (i + 1 == array.Length - (1 + j)) array[i + 1].SetCubeToColor(Color.green);
            }
        }
        array[0].SetCubeToColor(Color.green); //mark as green first cube at the end of all iterations
    }
}
