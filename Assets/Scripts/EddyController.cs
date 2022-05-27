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

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float diff = collision.gameObject.transform.position.y - zeroPosition.position.y;

            Debug.Log("d " + diff);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,-1,0) * forceModule * (1 - (diff * 0.1f)), ForceMode.Impulse);

        }
    }

    #endregion

}
