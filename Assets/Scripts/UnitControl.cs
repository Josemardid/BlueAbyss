using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
  [Range( 0.0f, 100.0f )]
  public float linearVelocityMagnitude = 10.0f;

  [Range( 0.0f, 100.0f )]
  public float separationFactor = 1.0f;

  [Range( 0.0f, 100.0f )]
  public float closeUpFactor = 1.0f;

  [Range( 0.0f, 100.0f )]
  public float alignmentFactor = 1.0f;

  [Range( 0.0f, 100.0f )]
  private float queryRadius = 100.0f;

  ////////////////////////////////////////////////////////////////////////////////////////

  // Start is called before the first frame update
  void Start()
  {
    transform.rotation = Quaternion.AngleAxis(
      Random.Range( 0.0f, 180.0f ),
      new Vector3( Random.Range( -1.0f, 1.0f ), Random.Range( -1.0f, 1.0f ), Random.Range( -1.0f, 1.0f ) )
    ).normalized;
  }

  ////////////////////////////////////////////////////////////////////////////////////////

  // Update is called once per frame
  void Update()
  {
    UpdateSeparation();

    transform.position += Time.deltaTime * linearVelocityMagnitude * transform.forward;
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

      Quaternion totalRotation = Quaternion.FromToRotation( transform.forward, separationFactor * directionSeparation + closeUpFactor * directionCloseUp + alignmentFactor * directionAlignment );
      transform.rotation = Quaternion.Slerp( transform.rotation, totalRotation * transform.rotation, Time.deltaTime );
    }
  }
}
