using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMov : MonoBehaviour
{
    public Transform objective;

    public float velUp = 5.0f;
    public float velDown = 1.5f;

    public float maxDistance = 20.0f;

    public bool towardsObjective = true; // empieza abajo y sube hasta una dist

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
            
            //behaviour
            if (towardsObjective)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, velUp, 0);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, -velDown, 0);
            }


            //this.transform.LookAt(objective);
            //transform.position = Vector3.MoveTowards(transform.position, objective.transform.position, dt);
        }
    }
        
}
