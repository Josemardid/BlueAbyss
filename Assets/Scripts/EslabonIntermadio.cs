using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EslabonIntermadio : MonoBehaviour
{
    #region Private Attributes

    private Vector3 initialPos;

    private Vector3 toMove;
    private Vector3 maxVel;



    #endregion

    #region Public Attributes
    public float maxSpeed;

    public float diffMove;
    #endregion

    #region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        toMove.x = Mathf.Clamp(this.gameObject.transform.position.x, initialPos.x - diffMove, initialPos.x + diffMove);
        //toMove.y = Mathf.Clamp(this.gameObject.transform.position.y, initialPos.y - diffMove* 0.25f, initialPos.y + diffMove * 0.25f);
        toMove.y = initialPos.y;//Mathf.Clamp(this.gameObject.transform.position.y, initialPos.y - diffMove* 0.25f, initialPos.y + diffMove * 0.25f);
        toMove.z = Mathf.Clamp(this.gameObject.transform.position.z, initialPos.z - diffMove, initialPos.z + diffMove);

        this.gameObject.transform.position = toMove;

        maxVel.x = Mathf.Clamp(this.gameObject.GetComponent<Rigidbody>().velocity.x, -maxSpeed, maxSpeed);
        maxVel.y = Mathf.Clamp(this.gameObject.GetComponent<Rigidbody>().velocity.y, -maxSpeed, maxSpeed);
        maxVel.z = Mathf.Clamp(this.gameObject.GetComponent<Rigidbody>().velocity.z, -maxSpeed, maxSpeed);


        this.gameObject.GetComponent<Rigidbody>().velocity = maxVel;

    }
#endregion

#region HumanMade Methods
#endregion

}
