using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMov : MonoBehaviour
{
    public Transform objective;

    public float velUp = 5.0f;
    public float velDown = 1.5f;

    public float minDistance = 5.0f;
    public float maxDistance = 20.0f;

    private bool towardsObjective = true; // empieza abajo y sube hasta una dist

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        float dt = Time.deltaTime;

        if (objective !=null)
        {
            //state of the movement
            if(objective.transform.position.y - transform.position.y > minDistance)
            {
                towardsObjective = false;
            }else if (objective.transform.position.y - transform.position.y < maxDistance)
            {
                towardsObjective = true;
            }
            //behaviour
            if (towardsObjective)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, -velUp * dt, 0);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, velDown * dt, 0);
            }

            //this.transform.LookAt(objective);
            //transform.position = Vector3.MoveTowards(transform.position, objective.transform.position, dt);
        }
    }
        
}
