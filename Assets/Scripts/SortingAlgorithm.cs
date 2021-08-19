using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class made for inheritance
public class SortingAlgorithm : MonoBehaviour
{

    [Header("Components")]
    public SpawnerController spawnerController = null; //Component needed for setting chosen Algorithm

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Used to override and implement specific algorithm
    virtual public void Sort(int[] array, int left, int right)
    {

    }

    //Used when button is clicked in UI
    public void SetAlgorithm()
    {
        spawnerController.chosenAlgorithm = this;
    }
}
