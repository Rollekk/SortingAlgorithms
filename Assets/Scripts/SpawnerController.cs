using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Components")]
    public SortingAlgorithm choosenAlgorithm = null; //Choosen algorithm with starting UI
    CubeController spawnedCube = null; //Script for cube controller, attached to each cube
    public GameObject cubeToSpawn = null; //Prefab of cube to spawn

    [Header("Cubes")]
    [SerializeField] List<CubeController> spawnedCubes = new List<CubeController>(); //list of spawnedCubes
    [SerializeField] List<int> cubesNumbers = new List<int>(); //list of cubes numbers that were generated
    [SerializeField] bool isSpawned = false; //can next cube be spawned
    bool isSorting = false; //is algorithm sorting at that moment

    public int maxSpawnedObjects = 10; //maximum objects that can be spawned
    int numberOfSpawnedCubes = 0; //current number of spawned objects

    [Header("Spawner")]
    float lerpTimer = 0.0f; //Timer for lerping between two vectors
    [SerializeField] float spawnerMoveSpeed = 0.1f; //Speed withi which spawner is moving
    [SerializeField] float spawnerStep = 0.5f; //Amount of distance between points that spawn cubes
    bool canMoveToNext = false; //can spawner move to next location

    Vector3 spawnerStartingLocation; //Starting location of moving spawner
    Vector3 spawnerEndingLocation; //Ending location of moving spawner


    // Update is called once per frame
    void Update()
    {
        //Check if there is any Algorithm
        if(choosenAlgorithm)
        {
            //if there is, start the game
            if (canMoveToNext) MoveSpawnerToNextLocation(); //When spawner can move, move to next location
            else if (!isSpawned && numberOfSpawnedCubes < maxSpawnedObjects) //If spawner can't move just spawn another cube
            {
                isSpawned = true;
                SpawnNewCube();
            }
            //If its not sorting currenlty and all cubes have been spawned, Sort with selected algirthm
            if (!isSorting && !isSpawned && numberOfSpawnedCubes >= maxSpawnedObjects) SortAllCubes();
        }
    }

    //Spawn new cube, set its parameters and add it to list
    void SpawnNewCube()
    {
        //Spawn new cube and set its parameters
        spawnedCube = Instantiate(cubeToSpawn, transform.position, transform.rotation).GetComponentInChildren<CubeController>();
        //Generate cube number
        spawnedCube.GenerateCubeNumber();
        spawnedCube.spawnerController = this;

        //Add cube to list of cubes
        spawnedCubes.Add(spawnedCube);
        cubesNumbers.Add(spawnedCube.cubeNumber);

        //Increment number of generated cubes
        numberOfSpawnedCubes++;
    }

    //Apply choosen sorting algorithm to cubes
    void SortAllCubes()
    {
        //Create init array made of cube list
        int[] tmpArray = cubesNumbers.ToArray();

        //Sort just array 
        choosenAlgorithm.Sort(tmpArray, 0, spawnedCubes.Count - 1);

        //Clear list and add all sorted elements
        cubesNumbers.Clear();
        cubesNumbers.AddRange(tmpArray);
        isSorting = true;
    }

    //Move spanwer object to next position
    void MoveSpawnerToNextLocation()
    {
        //Transform its position to new location with lerpTimer and spawnerMoveSpeed
        transform.position = Vector3.Lerp(spawnerStartingLocation, spawnerEndingLocation, lerpTimer);
        lerpTimer += spawnerMoveSpeed * Time.deltaTime;

        //Check if its at destined location
        if (transform.position == spawnerEndingLocation)
        {
            //If it is stop moving and spawn new cube
            canMoveToNext = false;
            isSpawned = false;
            lerpTimer = 0.0f;
        }
    }

    //Set spawner new location
    public void SetSpawnerNewLocation()
    {
        //Start is always current location, ending is current location plus step
        spawnerStartingLocation = transform.position;
        spawnerEndingLocation = new Vector3(transform.position.x + spawnerStep, transform.position.y, transform.position.z);
        canMoveToNext = true;
    }
}
