using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    [Header("Components")]
    Canvas canvasUI = null; //UI Canvas
    GameObject buttonsGO = null; //Buttons empty gameobject

    [Header("Texts")]
    TMP_Text swapTMP = null; //Swap counter text
    TMP_Text nameTMP = null; //Algorithm name text
    TMP_Text speedTMP = null; //Algorithm name text

    [Header("Sliders")]
    [SerializeField] TMP_Text sortingNumberTMP = null; //TMP for sorting number slider text
    public float sortingSpeed = 0.1f;

    [SerializeField] TMP_Text cubesNumberTMP = null; //TMP for sorting number slider text
    public int cubesNumber;

    private void Awake()
    {
        canvasUI = GetComponent<Canvas>();
        buttonsGO = transform.Find("AlgorithmButtons").gameObject;

        swapTMP = transform.Find("InGameUI/SwapTMP").GetComponentInChildren<TMP_Text>();
        speedTMP = transform.Find("InGameUI/SpeedTMP").GetComponentInChildren<TMP_Text>();

        nameTMP = transform.Find("NameTMP").GetComponentInChildren<TMP_Text>();
        nameTMP.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Deactivate ingameUI on start
        swapTMP.transform.parent.gameObject.SetActive(false);

        //Set slider text to 0 on start
        sortingNumberTMP.text = "0,1";
        cubesNumberTMP.text = "5";
        cubesNumber = 5;
    }

    //Hide start UI with options
    public void HideButtons()
    {
        swapTMP.transform.parent.gameObject.SetActive(true);
        buttonsGO.SetActive(false);
    }

    //Update swap counter TMP.text
    public void UpdateSwapCounterText(int swapsNumber)
    {
        swapTMP.text = "Swaps: " + swapsNumber.ToString();
    }

    //Set algorithm name TMP.text
    public void SetAlgorithmName(string algorithmName)
    {
        nameTMP.gameObject.SetActive(true);
        nameTMP.text = "Algorithm: " + algorithmName;
    }

    //Set sorting speed TMP.text
    public void SetSortingSpeedText()
    {
        speedTMP.text = "Speed: " + sortingSpeed.ToString();
    }

    //Updating slider text on value changed
    public void UpdateSortingSpeedText(float sliderValue)
    {
        sortingSpeed = (float) System.Math.Round(sliderValue, 1);
        sortingNumberTMP.text = System.Math.Round(sliderValue, 1).ToString();
    }

    //Updating slider text on value changed
    public void UpdateCubesNumberText(float sliderValue)
    {
        cubesNumber = (int) sliderValue;
        cubesNumberTMP.text = sliderValue.ToString();
    }
}
