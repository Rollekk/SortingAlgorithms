using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingController : MonoBehaviour
{
    [Header("Components")]
    public SpawnerController spawnerController = null; //Reference to controller used for spawning cubes
    SortingAlgorithm chosenAlgorithm = null; //Reference to chosen algorithm used for sorting and starting game

    [Header("Sorting")]
    public bool isSorting = false; //is algorithm sorting at that moment

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Apply chosen sorting algorithm to cubes
    public List<CubeController> SortAllCubes(List<CubeController> spawnedCubes)
    {
        chosenAlgorithm = spawnerController.chosenAlgorithm;

        //Create init array made of cube list
        CubeController[] tmpArray = spawnedCubes.ToArray();

        //Sort just array 
        StartCoroutine(chosenAlgorithm.Sort(tmpArray, 0, spawnedCubes.Count - 1));

        //Clear list and add all sorted elements
        spawnedCubes.Clear();
        spawnedCubes.AddRange(tmpArray);
        isSorting = true;

        return spawnedCubes;
    }
}
