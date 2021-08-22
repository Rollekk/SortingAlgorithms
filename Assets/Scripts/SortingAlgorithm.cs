using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class made for inheritance
public class SortingAlgorithm : MonoBehaviour
{

    [Header("Components")]
    public SpawnerController spawnerController = null; //Component needed for setting chosen Algorithm
    public float sortSpeed = 1.0f; //time for how fast should algorithm sort

    [SerializeField] protected GameUIController gameUI = null; //UI 
    protected int swapCount = 0; //counter for swaps made with algorithm
    protected string algorithmName; //chosen algorithm name

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameObject.SetActive(false);

        algorithmName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Used to override and implement specific algorithm
    virtual public IEnumerator Sort(CubeController[] array, int left, int right)
    {
        return null;
    }

    //Set algorithm on button click
    public void SetAlgorithm()
    {
        spawnerController.chosenAlgorithm = this; //set chosen algorithm to one that is clicked with button
        gameUI.SetAlgorithmName(algorithmName); //set tmp.text name to chosen algorithm name
    }

    //Start game with chosen algorithm
    public void StartSorting()
    {
        //if chosen algorithm is set
        if(spawnerController.chosenAlgorithm)
        {
            spawnerController.chosenAlgorithm.sortSpeed = gameUI.sortingSpeed; //set sort speed to slider value from gameUI
            gameUI.SetSortingSpeedText(); //set speed text

            spawnerController.maxSpawnedObjects = gameUI.cubesNumber; //set max spawned objects to slider value from gameUI

            gameUI.HideButtons(); //hide all buttons
            spawnerController.chosenAlgorithm.gameObject.SetActive(true); //activate only chosen algorithm
            spawnerController.canStartGame = true; //start game
        }
    }
}
