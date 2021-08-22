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
                yield return new WaitForSeconds(sortSpeed);

                //Check if current cubeNumber is bigger than next one
                if (array[i].cubeNumber > array[i + 1].cubeNumber)
                {
                    array[i].SetCubeToColor(Color.red);  //mark as red cubes to move
                    array[i + 1].SetCubeToColor(Color.red); //mark as red cubes to move
                    yield return new WaitForSeconds(sortSpeed);

                    //Swap positions of cubes in game
                    Vector3 tmp = array[i + 1].transform.position;
                    array[i + 1].transform.position = array[i].transform.position;
                    array[i + 1].SetCubeToColor(Color.grey);  //mark as grey cubes already moved

                    array[i].transform.position = tmp;
                    array[i].ResetCubeColor(); 

                    //Swaps positions of cubes in given array
                    CubeController temp = array[i + 1];
                    array[i + 1] = array[i];
                    array[i] = temp;
                    swapCount++;
                    gameUI.UpdateSwapCounterText(swapCount);
                    yield return new WaitForSeconds(sortSpeed);
                }
                else
                {
                    array[i].SetCubeToColor(Color.grey); //mark as grey cubes already checked
                }
                //if its last iteration, mark last cube as in place (green color)
                if (i + 1 == array.Length - (1 + j)) array[i + 1].SetCubeToColor(Color.green);
            }
        }
        array[0].SetCubeToColor(Color.green); //mark as green first cube at the end of all iterations
    }
}
