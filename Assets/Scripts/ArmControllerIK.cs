using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControllerIK : MonoBehaviour
{
    #region Private Attributes
    private GameObject target;
    private GameObject[] allPearls;

    private AudioManager audioM;
  
    #endregion

    #region Public Attributes
    public bool isSearching = false;
    
    public GameObject targetToBeHidden;
    public GameObject targetToSearch;
    public Transform[] armParts;
    //public GameObject toDestroy;
    public GameObject hook;

    public GameObject submarine;

    public bool hasSoundedYet = false;//lo siento
    #endregion

    #region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        allPearls = GameObject.FindGameObjectsWithTag("Pearl");
        submarine.GetComponent<SubmarineController>().setAllPearls(allPearls.Length);
        //Podriamos inicializar aqui el brazo pero pa que, lo importante es que sean hijos uno de otros

        audioM = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSearching)
        {
            target = targetToSearch;

            if(targetToSearch == null)
            {
                target = targetToBeHidden;
              
            }
            CheckSound();
        }
        else
        {
            target = targetToBeHidden;
          
        }

        UpdateArmController(Time.deltaTime);

    }
    #endregion

    #region HumanMade Methods

    public void SearchTarget()
    {
        allPearls = GameObject.FindGameObjectsWithTag("Pearl");
        
        if (allPearls.Length > 0)
        {
            int pearlIndex = 0;
            float distToPearl = (allPearls[0].transform.position - this.transform.position).magnitude;

            for (int i = 0; i < allPearls.Length; i++)
            {
                if (allPearls[i] != null && distToPearl > (allPearls[i].transform.position - this.transform.position).magnitude)
                {
                    distToPearl = (allPearls[i].transform.position - this.transform.position).magnitude;
                    pearlIndex = i;
                }
            }

            targetToSearch = allPearls[pearlIndex];
        }
        else
        {
            targetToSearch = null;

        }
        
    }


    public void UpdateArmController(float dt)
    {
        Vector3 currentDir;

        for (int i = 0; i < armParts.Length; i++)
        {
            if (true)
            {
                if (armParts[i].childCount == 0)
                {
                    currentDir = -armParts[i].forward;
                }
                else
                {
                    currentDir = (armParts[armParts.Length - 1].position - armParts[i].position);
                }
            }
            else
            {
                //currentDir = armParts[i].forward;
            }
            if(target != null)
            {
                Vector3 goalDirection = target.transform.position - armParts[i].position;
                Quaternion goalOrientation = Quaternion.FromToRotation(currentDir, goalDirection) * armParts[i].rotation;
                Quaternion newOrientation = Quaternion.Slerp(armParts[i].rotation, goalOrientation, 1.0f * dt);

            if (armParts[i].parent == null || transform.GetComponent<ArmAttributes>() == null || Vector3.Angle(armParts[i].parent.transform.forward, newOrientation * -Vector3.forward) < transform.GetComponent<ArmAttributes>().angleLimit)
            {
                armParts[i].rotation = newOrientation;

            }
            }
        }
    }

    public void CheckSound()
    {
        if (!hasSoundedYet)
        {
            audioM.Play("Arm");
            hasSoundedYet = true;
        }
    }

    #endregion

}
