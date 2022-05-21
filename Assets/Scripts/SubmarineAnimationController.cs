using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineAnimationController : MonoBehaviour
{
    #region Private Attributes
    private GameObject[] allPearls;
    private GameObject targetToSearch;
    private float timer = 0;
    private bool rotating = false;

    #endregion

    #region Public Attributes
    public float propellerSpeed = 30f;
    public float timeToRotate = 5;

    public GameObject periscope;
    public GameObject propeller;

#endregion

#region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        allPearls = GameObject.FindGameObjectsWithTag("Pearl");
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        //PropellerMovement(dt);
        //PeriscopeMovement(dt);
    }
#endregion

#region HumanMade Methods

    public void PropellerMovement(float dt)
    {

        propeller.transform.Rotate(0,dt*propellerSpeed,0);
    }

    public void PeriscopeMovement(float dt)
    {
        timer += dt;
        if(timer > timeToRotate)
        {
            SearchTarget();
            rotating = true;
            timer = 0;
        }
        
        //Quaternion.Slerp(periscope.transform.rotation, )

        //periscope.transform.LookAt(targetToSearch.transform);
        
    }


    public void SearchTarget()
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
    #endregion

}
