using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlgorithmTextController : MonoBehaviour
{
    [Header("Components")]
    TMP_Text algorithmName;
    public SpawnerController spawnerController;

    private void Awake()
    {
        algorithmName = GetComponent<TMP_Text>();
        algorithmName.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerController.choosenAlgorithm) SetAglorithmName();
    }

    void SetAglorithmName()
    {
        algorithmName.enabled = true;
        algorithmName.text = spawnerController.choosenAlgorithm.name;
        gameObject.GetComponent<AlgorithmTextController>().enabled = false;
    }
}
