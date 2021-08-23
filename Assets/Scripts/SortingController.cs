using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingController : MachineController
{
    [Header("Components")]
    public SpawnerController spawnerController = null; //Reference to controller used for spawning cubes
    SortingAlgorithm chosenAlgorithm = null; //Reference to chosen algorithm used for sorting and starting game

    [Header("Cubes")]
    [SerializeField] Transform leftCubeTransform = null;
    Vector3 initialLeftCubePosition;
    CubeController leftCube = null;

    [SerializeField] Transform rightCubeTransform = null;
    Vector3 initialRightCubePosition;
    CubeController rightCube = null;

    bool shouldDropCube = false;
    bool shouldTakeLeft = true;

    [Header("Sorting")]
    public bool isSorting = false; //is algorithm sorting at that moment

    private void Update()
    {
        if (canMoveToNext) MoveToNextLocation();
        else if (shouldDropCube) DropCube();
    }

    //Apply chosen sorting algorithm to cubes
    public IEnumerator StartSorting(List<CubeController> spawnedCubes)
    {
        chosenAlgorithm = spawnerController.chosenAlgorithm;

        //set sorting machine location to first cube
        transform.position = new Vector3(spawnedCubes[0].transform.position.x, transform.position.y, spawnedCubes[0].transform.position.z);
        yield return new WaitForSeconds(1.0f);

        //Create init array made of cube list
        CubeController[] tmpArray = spawnedCubes.ToArray();

        //Set sortingController to this
        chosenAlgorithm.sortingController = this;
        //Sort just array 
        StartCoroutine(chosenAlgorithm.Sort(tmpArray, 0, spawnedCubes.Count - 1));

        //Clear list and add all sorted elements
        spawnedCubes.Clear();
        spawnedCubes.AddRange(tmpArray);
        isSorting = true;

    }

    //Pickup cube on sorting machine
    //cubeToPickup is cube being picked up
    //shouldTakeLeft marks if cube should be placed on left or right
    public void PickupCube(CubeController cubeToPickup, bool shouldTakeLeft)
    {
        //check if should be put in left slot
        if (shouldTakeLeft)
        {
            //set initial cube position to current cube position
            initialLeftCubePosition = cubeToPickup.transform.position;
            //set cube transform to sorting machine transform
            cubeToPickup.transform.position = leftCubeTransform.position;
            cubeToPickup.transform.parent = leftCubeTransform;
            //set picked up cube to given in parameter
            leftCube = cubeToPickup;
        }
        else
        {
            //set initial cube position to current cube position
            initialRightCubePosition = cubeToPickup.transform.position;
            //set cube transform to sorting machine transform
            cubeToPickup.transform.position = rightCubeTransform.position;
            cubeToPickup.transform.parent = rightCubeTransform;
            //set picked up cube to given in parameter
            rightCube = cubeToPickup;
        }
    }

    //Put down cube in place
    //shouldSwap places with other cube on sorting machine
    //shouldTakeLeft marks if cube should be placed on left or right
    public void PutDownCube(bool shouldSwap, bool shouldTakeLeft)
    {
        //check if should be put in left slot
        if (shouldTakeLeft)
        {
            //check if should swap positions
            if(shouldSwap) SetNewLocation(initialRightCubePosition.x); //sets position to initial position
            else SetNewLocation(initialLeftCubePosition.x); //sets position to initial position
        }
        else
        {
            if(shouldSwap) SetNewLocation(initialLeftCubePosition.x); //sets position to initial position
            else SetNewLocation(initialRightCubePosition.x); //sets position to initial position
        }
        //should sorting machine move to next position
        canMoveToNext = true;
        //should cube be dropped
        shouldDropCube = true;
        this.shouldTakeLeft = shouldTakeLeft;
    }

    //Drop cube in position
    void DropCube()
    {
        //check which cube to drop
        if (shouldTakeLeft)
        {
            //check if there is cube
            if(leftCube)
            {
                //set cube transform to null
                leftCube.transform.parent = null;
                //set cube position to spawn position
                leftCube.transform.position = spawnTransform.position;
                //set cube to null
                leftCube = null;
            }
        }
        else
        {
            //check if there is cube
            if (rightCube)
            {
                //set cube transform to null
                rightCube.transform.parent = null;
                //set cube position to spawn position
                rightCube.transform.position = spawnTransform.position;
                //set cube to null
                rightCube = null;
            }
        }
        //set to drop cube to false so it's called only once
        shouldDropCube = false;
    }

    //Put down cube in place
    //cube to put down
    public void PutDownCube(CubeController cube)
    {
        //check on which side the cube is on
        if (cube == leftCube)
        {
            //set sorting machine new location
            SetNewLocation(initialLeftCubePosition.x);
            //set shouldTakeLeft to true, to drop left cube
            shouldTakeLeft = true;
        }
        else
        {
            //set sorting machine new location
            SetNewLocation(initialRightCubePosition.x);
            //set shouldTakeLeft to true, to drop right cube
            shouldTakeLeft = false;
        }
        //should sorting machine move to next position
        canMoveToNext = true;
        //should cube be dropped
        shouldDropCube = true;
    }
}
