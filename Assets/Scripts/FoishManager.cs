using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoishManager : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float linearVelocityMagnitude = 10.0f;


    [Range(0.0f, 100.0f)]
    public float separationFactor = 1.0f;
    private float separationFactorPercentage;

    [Range(0.0f, 100.0f)]
    public float closeUpFactor = 1.0f;
    private float closeUpFactorPercentage;

    [Range(0.0f, 100.0f)]
    public float alignmentFactor = 1.0f;
    private float alignmentFactorPercentage;

    [Range(0.0f, 100.0f)]
    public float pathFactor = 1.0f;
    private float pathFactorPercentage;

    [Range(0.0f, 100.0f)]
    public float predatorFactor = 1.0f;
    public float distToPredator;


    public float posDiff;

    public GameObject[] directionsArray;


    public GameObject Foish;
    public GameObject[] allFoish;
    public GameObject predator;


    public float timeToChangeDir = 10.0f;
    public float timePassed = 10.0f;

    private float auxTotal;

    public int numOfFoish = 7;
    // Start is called before the first frame update
    void Start()
    {

        allFoish = new GameObject[numOfFoish];
        GenerateAuxTotal();
        CreateFoish();

    }

    // Update is called once per frame
    void Update()
    {
        timePassed -= Time.deltaTime;
        if (timePassed < 0.0f)
        {
            timePassed = timeToChangeDir;
            int num = Random.Range(0, directionsArray.Length - 1);
            for (int i = 0; i < numOfFoish; i++)
            {
                allFoish[i].GetComponent<UnitControl>().currentDirection = num;
            }
            
        }
    }

    void ChangeDir()
    {

    }
    void GenerateAuxTotal()
    {
        auxTotal = separationFactor + closeUpFactor + alignmentFactor + pathFactor ;
        separationFactorPercentage = separationFactor / auxTotal * 100;
        closeUpFactorPercentage = closeUpFactor / auxTotal * 100;
        alignmentFactorPercentage = alignmentFactor / auxTotal * 100;
        pathFactorPercentage = pathFactor / auxTotal * 100;
    }
    void CreateFoish()
    {
        for(int i = 0; i < numOfFoish; i++)
        {
            GameObject thisFoish=Object.Instantiate(Foish, this.transform);
            thisFoish.GetComponent<UnitControl>().SetParameters(linearVelocityMagnitude,separationFactor,closeUpFactor,alignmentFactor,pathFactor,predatorFactor, numOfFoish, directionsArray,posDiff,predator, distToPredator);
            thisFoish.transform.position += new Vector3(Random.Range(-posDiff, posDiff), Random.Range(-posDiff, posDiff), Random.Range(-posDiff, posDiff));
            allFoish[i] = thisFoish;
        }
    }
}
