using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EddyController : MonoBehaviour
{
    #region Private Attributes



    #endregion

    #region Public Attributes
    public float forceModule;
    public Transform zeroPosition;

    public string axis;//Siempre con MAYUSCULAS

#endregion

#region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region HumanMade Methods

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float diff = 0;
            Vector3 forceDir = new Vector3(0,0,0);

            if (axis == "X")
            {
                diff = collision.gameObject.transform.position.x - zeroPosition.position.x;
            }

            if (axis == "-X" )
            {
                diff = -(collision.gameObject.transform.position.x - zeroPosition.position.x);
                
            }
            if (axis == "Y")
            {
                diff = collision.gameObject.transform.position.y - zeroPosition.position.y;
            }
            if (axis == "-Y")
            {
                diff = -(collision.gameObject.transform.position.y - zeroPosition.position.y);
            }
            if (axis == "Z")
            {
                diff = collision.gameObject.transform.position.z - zeroPosition.position.z;
            }
            if (axis == "-Z")
            {
                diff = -(collision.gameObject.transform.position.z - zeroPosition.position.z);
            }

            forceDir = (this.gameObject.transform.position - zeroPosition.position).normalized;

            Debug.Log("d " + (1 / (diff * 0.1f)) + " // dir " + forceDir);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDir * forceModule * (1/(diff * 0.1f)), ForceMode.Force);

        }
    }

    #endregion

}
