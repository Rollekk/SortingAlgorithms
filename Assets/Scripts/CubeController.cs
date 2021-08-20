using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeController : MonoBehaviour
{
    [Header("Cube")]
    TMP_Text numberTMP; //UI Text showing generated number
    public int cubeNumber; //random cube number generated at start

    [Header("SpawnerController")]
    [HideInInspector] public SpawnerController spawnerController; //spawner controller needed for using methods
    bool shouldSet = true; //should cube set new location

    [Header("Colors")]
    Color initialColor;

    //Whenever game is launched
    private void Awake()
    {
        //Get TMP component
        numberTMP = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        initialColor = gameObject.GetComponent<Renderer>().material.color;
    }

    //Generates new random cube number from 0 to 100
    public void GenerateCubeNumber()
    {
        //Generate number and cast it to int
        cubeNumber = (int)Random.Range(0.0f, 100.0f);

        //Set UI text to this number
        numberTMP.text = cubeNumber.ToString();
    }

    //On collision start
    private void OnCollisionEnter(Collision collision)
    {
        //Check if its with ground and if cube can set new location
        if (collision.collider.CompareTag("Ground") && shouldSet)
        {
            //if it is set new location, once
            spawnerController.SetSpawnerNewLocation();
            shouldSet = false;
        }
    }

    public void SetCubeToColor(Color color)
    {
        //if(gameObject.GetComponent<Renderer>().sharedMaterial.color != Color.green)
            gameObject.GetComponent<Renderer>().sharedMaterial.color = color;
    }

    public void ResetCubeColor()
    {
        //if (gameObject.GetComponent<Renderer>().sharedMaterial.color != Color.green)
            gameObject.GetComponent<Renderer>().sharedMaterial.color = initialColor;
    }
}
