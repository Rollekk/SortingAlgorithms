using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    [Header("Position")]
    protected float initialPosY = 0.0f; //initial machine positionY
    protected float lerpTimer = 0.0f; //Timer for lerping between two vectors
    [SerializeField] protected float spawnerMoveSpeed = 0.1f; //Speed withi which spawner is moving
    [SerializeField] protected float spawnerStep = 0.5f; //Amount of distance between points that spawn cubes
    public bool canMoveToNext = false; //can spawner move to next location

    public Vector3 spawnerStartingLocation; //Current position of spawner
    protected Vector3 spawnerEndingLocation; //Position where spawner should be

    [SerializeField] protected bool isSpawned = false; //can next cube be spawned

    [SerializeField] protected Transform spawnTransform = null; //spawnPoint transform

    protected virtual void Start()
    {
        initialPosY = transform.position.y;
    }

    //Move spanwer object to next position
    protected void MoveToNextLocation()
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
    public void SetNewLocation()
    {
        //Start is always current location, ending is current location plus step
        spawnerStartingLocation = transform.position;
        spawnerEndingLocation = new Vector3(transform.position.x + spawnerStep, transform.position.y, transform.position.z);
        canMoveToNext = true;
    }

    //Set spawner new location
    public void SetNewLocation(float desiredX)
    {
        //Start is always current location, ending is current location plus step
        spawnerStartingLocation = transform.position;
        spawnerEndingLocation = new Vector3(desiredX, transform.position.y, transform.position.z);
        canMoveToNext = true;
    }
}
