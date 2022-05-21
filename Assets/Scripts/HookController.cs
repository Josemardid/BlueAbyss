using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    #region Private Attributes
    private GameObject target;
    private bool addObj = false;
    
    #endregion

    #region Public Attributes
    public GameObject submarine;
    public GameObject arm;
    public float maxDiff;
    public bool objGrabbed = false;
    #endregion

    #region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (arm.GetComponent<ArmControllerIK>().isSearching && !objGrabbed)
        {
            target = arm.GetComponent<ArmControllerIK>().targetToSearch;
            if(target != null)
            {
                if ((this.gameObject.transform.position - target.transform.position).magnitude < maxDiff)
                {
                    objGrabbed = true;
                    addObj = true;


                    //No se cuando sumar todo
                }
            }

            

        }

        if (objGrabbed && target != null)
        {
            target.transform.position = this.gameObject.transform.position;
            arm.GetComponent<ArmControllerIK>().isSearching = false;//para que lo guarde
            arm.GetComponent<ArmControllerIK>().toDestroy = target;
        }

        if (target != null && !target.activeInHierarchy)
        {
            objGrabbed = false;
        }


        
    }
#endregion

#region HumanMade Methods



#endregion

}
