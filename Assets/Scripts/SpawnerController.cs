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

    protected override void Start()
    {
        base.Start();
    }

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
                StartCoroutine(SpawnNewCube());
            }
            //If its not sorting currenlty and all cubes have been spawned, Sort with selected algirthm
            if (!sortingController.isSorting && !isSpawned && numberOfSpawnedCubes >= maxSpawnedObjects)
            {
                HideTube();
            }
        }
    }

    //Spawn new cube, set its parameters and add it to list
    IEnumerator SpawnNewCube()
    {
        yield return null;
        //Spawn new cube and set its parameters
        spawnedCube = Instantiate(cubeToSpawn, spawnTransform.position, cubeToSpawn.transform.rotation).GetComponentInChildren<CubeController>();
        //Generate cube number
        spawnedCube.GenerateCubeNumber();
        spawnedCube.spawnerController = this;

        //Add cube to list of cubes
        spawnedCubes.Add(spawnedCube);

        //Increment number of generated cubes
        numberOfSpawnedCubes++;
    }

    void HideTube()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, initialPosY, transform.position.z),
            new Vector3(transform.position.x, initialPosY + 5.0f, transform.position.z), lerpTimer);
        lerpTimer += Time.deltaTime;

        if (transform.position.y == initialPosY + 5.0f)
        {
            sortingController.StartCoroutine(sortingController.StartSorting(spawnedCubes));
            lerpTimer = 0.0f;
            gameObject.SetActive(false);
        }

    }
}
