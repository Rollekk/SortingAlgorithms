using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MachineController
{
    [Header("Components")]
    public SortingAlgorithm chosenAlgorithm = null; //Reference to chosen algorithm used for sorting and starting game
    public SortingController sortingController = null; //Reference to controller used for sorting
    CubeController spawnedCube = null; //Script for cube controller, attached to each cube
    public GameObject cubeToSpawn = null; //Prefab of cube to spawn

    [Header("Cubes")]
    [SerializeField] List<CubeController> spawnedCubes = new List<CubeController>(); //list of spawnedCubes

    public int maxSpawnedObjects = 10; //maximum objects that can be spawned
    int numberOfSpawnedCubes = 0; //current number of spawned objects

    [Header("Spawner")]
    public bool canStartGame = false; //can the game be started/ui is hidden, algorithm is chosen

    // Update is called once per frame
    void Update()
    {
        //Check if there is any Algorithm
        if(canStartGame)
        {
            //if there is, start the game
            if (canMoveToNext) MoveToNextLocation(); //When spawner can move, move to next location
            else if (!isSpawned && numberOfSpawnedCubes < maxSpawnedObjects) //If spawner can't move, spawn another cube
            {
                isSpawned = true;
                SpawnNewCube();
            }
            //If its not sorting currenlty and all cubes have been spawned, Sort with selected algirthm
            if (!sortingController.isSorting && !isSpawned && numberOfSpawnedCubes >= maxSpawnedObjects)
            {
                sortingController.StartCoroutine(sortingController.StartSorting(spawnedCubes));
                gameObject.SetActive(false);
            }
        }
    }

    //Spawn new cube, set its parameters and add it to list
    void SpawnNewCube()
    {
        //Spawn new cube and set its parameters
        spawnedCube = Instantiate(cubeToSpawn, spawnTransform.position, transform.rotation).GetComponentInChildren<CubeController>();
        //Generate cube number
        spawnedCube.GenerateCubeNumber();
        spawnedCube.spawnerController = this;

        //Add cube to list of cubes
        spawnedCubes.Add(spawnedCube);

        //Increment number of generated cubes
        numberOfSpawnedCubes++;
    }
}
