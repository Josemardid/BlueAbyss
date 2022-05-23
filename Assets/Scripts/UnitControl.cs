using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public float linearVelocityMagnitude = 10.0f;

    public float separationFactor = 1.0f;

    public float closeUpFactor = 1.0f;

    public float alignmentFactor = 1.0f;

    public float pathFactor = 1.0f;
    public float predatorFactor;

    public float queryRadius = 100.0f;

    public GameObject[] directionsArray;
    public GameObject predator;
    public float distToPredator;
    public Transform foishDirection;
    public float posDiff = 10.0f;
    public int currentDirection;

    //private float timeToResetDirectionCounter = 10.0f;
    //public float timeToResetDirection = 10.0f;

    ////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
    transform.rotation = Quaternion.AngleAxis(
        Random.Range( 0.0f, 180.0f ),
        new Vector3( Random.Range( -1.0f, 1.0f ), Random.Range( -1.0f, 1.0f ), Random.Range( -1.0f, 1.0f ) )
    ).normalized;

        currentDirection = 0;

    }

    ////////////////////////////////////////////////////////////////////////////////////////

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        UpdateSeparation();

        
        if (CheckFoishInDirection())//if no esta a una distacia del objetivo -> acercalo
        {
            transform.position += Time.deltaTime * linearVelocityMagnitude * transform.forward;
        }
        else
        {
            SelectNewDirection();

        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////

    void UpdateSeparation()
    {
        Vector3 positionAverage = Vector3.zero;
        Vector3 directionAlignment = Vector3.forward;
        int countIndividuals = 0;

        Collider[] colliders = Physics.OverlapSphere( transform.position, queryRadius );
        foreach ( Collider collider in colliders )
        {
            if ( collider.gameObject != gameObject &&
                ( collider.transform.position - transform.position ).magnitude <= queryRadius &&
                collider.GetComponent<UnitControl>() != null )
            {
            ++countIndividuals;
            positionAverage += collider.transform.position;
            directionAlignment += collider.transform.forward;
            }
        }
        if ( countIndividuals > 0 )
        {
            positionAverage /= countIndividuals;
            directionAlignment /= countIndividuals;

            Vector3 directionSeparation = transform.position - positionAverage;
            Vector3 directionCloseUp = -directionSeparation;
            Vector3 directionToKelp= directionsArray[currentDirection].transform.position - this.transform.position;
            Quaternion totalRotation;
            totalRotation = Quaternion.FromToRotation(transform.forward, separationFactor * directionSeparation + closeUpFactor * directionCloseUp + alignmentFactor * directionAlignment + directionToKelp * pathFactor);
            transform.rotation = Quaternion.Slerp(transform.rotation, totalRotation * transform.rotation, Time.deltaTime );
        }
    }
    void SelectNewDirection()
    {
        int randomDir = 0;
       
        randomDir = Random.Range(0, directionsArray.Length - 1);
        foishDirection = directionsArray[randomDir].transform;

    }

    bool CheckFoishInDirection()
    {
        bool inDirection = false;

        if (transform.position.x< foishDirection.position.x- posDiff || transform.position.x > foishDirection.position.x + posDiff)
        {
            inDirection = true;
        }
        else if (transform.position.y < foishDirection.position.y - posDiff || transform.position.y > foishDirection.position.y + posDiff)
        {
            inDirection = true;
        }
        else if (transform.position.z < foishDirection.position.z - posDiff || transform.position.z > foishDirection.position.z + posDiff)
        {
            inDirection = true;
        }
        return inDirection;
    }
    public void SetParameters(float linearVelocityMagnitudePassed,float separationFactorPassed,float closeUpFactorPassed,float alignmentFactorPassed, float pathFactorPassed, float predatorFactorPassed,float queryRadiusPassed,GameObject[] directionsArrayPassed, float posDiffPassed, GameObject predatorPassed,float distToPredatorPassed)
    {
        linearVelocityMagnitude = linearVelocityMagnitudePassed;
        separationFactor = separationFactorPassed;
        closeUpFactor = closeUpFactorPassed;
        alignmentFactor = alignmentFactorPassed;
        pathFactor = pathFactorPassed;
        predatorFactor = predatorFactorPassed;
        queryRadius = queryRadiusPassed;
        directionsArray = directionsArrayPassed;
        foishDirection = directionsArray[0].transform;
        posDiff = posDiffPassed;
        predator = predatorPassed;
        distToPredator = distToPredatorPassed;
}
    }
