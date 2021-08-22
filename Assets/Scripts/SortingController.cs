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

    public void PickupCube(CubeController cubeToPickup)
    {
        if (!leftCube)
        {
            initialLeftCubePosition = cubeToPickup.transform.position;
            cubeToPickup.transform.position = leftCubeTransform.position;
            cubeToPickup.transform.parent = leftCubeTransform;
            leftCube = cubeToPickup;
        }
        else
        {
            initialRightCubePosition = cubeToPickup.transform.position;
            cubeToPickup.transform.position = rightCubeTransform.position;
            cubeToPickup.transform.parent = rightCubeTransform;
            rightCube = cubeToPickup;
        }
    }

    public void PutDownCube(bool shouldSwap)
    {
        if (leftCube)
        {
            if(shouldSwap) SetNewLocation(initialRightCubePosition.x);
            else SetNewLocation(initialLeftCubePosition.x);
        }
        else
        {
            if(shouldSwap) SetNewLocation(initialLeftCubePosition.x);
            else SetNewLocation(initialRightCubePosition.x);
        }

        canMoveToNext = true;
        shouldDropCube = true;
    }

    void DropCube()
    {
        if(leftCube)
        {
            leftCube.transform.parent = null;
            leftCube.transform.position = spawnTransform.position;
            leftCube = null;
        }
        else
        {
            rightCube.transform.parent = null;
            rightCube.transform.position = spawnTransform.position;
            rightCube = null;
        }
        shouldDropCube = false;
    }
}
