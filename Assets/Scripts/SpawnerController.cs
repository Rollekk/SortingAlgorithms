using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Components")]
    public SortingAlgorithm chosenAlgorithm = null; //Reference to chosen algorithm used for sorting and starting game
    public SortingController sortingController = null; //Reference to controller used for sorting
    CubeController spawnedCube = null; //Script for cube controller, attached to each cube
    public GameObject cubeToSpawn = null; //Prefab of cube to spawn
    public Transform spawnTransform = null; //spawnPoint transform

    [Header("Cubes")]
    [SerializeField] List<CubeController> spawnedCubes = new List<CubeController>(); //list of spawnedCubes
    [SerializeField] bool isSpawned = false; //can next cube be spawned

    public int maxSpawnedObjects = 10; //maximum objects that can be spawned
    int numberOfSpawnedCubes = 0; //current number of spawned objects

    [Header("Spawner")]
    public bool canStartGame = false; //can the game be started/ui is hidden, algorithm is chosen
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
        if(canStartGame)
        {
            //if there is, start the game
            if (canMoveToNext) MoveSpawnerToNextLocation(); //When spawner can move, move to next location
            else if (!isSpawned && numberOfSpawnedCubes < maxSpawnedObjects) //If spawner can't move just spawn another cube
            {
                isSpawned = true;
                SpawnNewCube();
            }
            //If its not sorting currenlty and all cubes have been spawned, Sort with selected algirthm
            if (!sortingController.isSorting && !isSpawned && numberOfSpawnedCubes >= maxSpawnedObjects)
                sortingController.SortAllCubes(spawnedCubes);
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
