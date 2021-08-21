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
            array[i].SetCubeToColor(Color.magenta); //mark as magenta smallest cube
            yield return new WaitForSeconds(sortTime);

            for (int j = i + 1; j < array.Length; j++)
            {
                array[j].SetCubeToColor(Color.blue); //mark as blue next cubes to check
                yield return new WaitForSeconds(sortTime);

                //check if current cube is smaller than current smalles
                if (array[j].cubeNumber < array[smallest].cubeNumber)
                {
                    array[smallest].SetCubeToColor(Color.grey); //mark as grey previous smallest cube
                    //if it is, set currenct cube as smallest
                    smallest = j;
                    array[smallest].SetCubeToColor(Color.magenta); //mark as magenta new smallest cube
                    yield return new WaitForSeconds(sortTime);
                }
                else array[j].SetCubeToColor(Color.grey); //mark as grey cubes already checked
            }

            array[smallest].SetCubeToColor(Color.red); //mark as red cubes to move
            array[i].SetCubeToColor(Color.red);  //mark as red cubes to move
            yield return new WaitForSeconds(sortTime);

            //Swap positions of cubes in game
            var tmp = array[smallest].transform.position;
            array[smallest].transform.position = array[i].transform.position;
            array[smallest].SetCubeToColor(Color.grey);  //mark as grey cubes already moved

            array[i].transform.position = tmp;

            //Swaps positions of cubes in given array
            CubeController temp = array[smallest];
            array[smallest] = array[i];
            array[i] = temp;

            array[i].SetCubeToColor(Color.green); //mark as green cube in place
            yield return new WaitForSeconds(sortTime);
        }
        array[array.Length - 1].SetCubeToColor(Color.green); //mark as green last cube when iteration is over
    }
}
