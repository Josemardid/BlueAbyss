using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineAnimationController : MonoBehaviour
{
    #region Private Attributes
    private GameObject[] allPearls;
    private GameObject targetToSearch;
    private float timer = 0;
   

    #endregion

    #region Public Attributes
    public float propellerSpeed = 30f;
    public float timeToRotate = 5;

    public GameObject periscope;
    public GameObject propeller;
    public GameObject submarine;

#endregion

#region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        SearchTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        PropellerMovement(dt);
        PeriscopeMovement(dt);
    }
#endregion

#region HumanMade Methods

    public void PropellerMovement(float dt)
    {
    Vector3 subVel = new Vector3(submarine.GetComponent<SubmarineController>().submarineRg.velocity.x, 0, submarine.GetComponent<SubmarineController>().submarineRg.velocity.z);
    float multiplier = subVel.magnitude;
    propeller.transform.Rotate(0,0, dt*multiplier * propellerSpeed);
    }

    public void PeriscopeMovement(float dt)
    {
        timer += dt;
        if(timer > timeToRotate)
        {
            SearchTarget();            
            timer = 0;
        }

        Vector3 lookPos = targetToSearch.transform.position - periscope.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        periscope.transform.rotation = Quaternion.Slerp(periscope.transform.rotation, rotation, dt);


        
    }


    public void SearchTarget()
    {
        allPearls = GameObject.FindGameObjectsWithTag("Pearl");
        submarine.GetComponent<SubmarineController>().setAllPearls(allPearls.Length);
        if (allPearls.Length > 0)
        {
            int pearlIndex = 0;
            float distToPearl = (allPearls[0].transform.position - this.transform.position).magnitude;

            for (int i = 0; i < allPearls.Length; i++)
            {
                if (distToPearl > (allPearls[i].transform.position - this.transform.position).magnitude)
                {
                    distToPearl = (allPearls[i].transform.position - this.transform.position).magnitude;
                    pearlIndex = i;
                }
            }

            targetToSearch = allPearls[pearlIndex];
        }
    }
    #endregion

}
