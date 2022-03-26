using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    #region Private Attributes
    private Rigidbody submarineRg;
    private Rigidbody FwdRg;
    private Rigidbody BackwRg;
    private Rigidbody propLeftUpRg;//estan puestos segun su posicion en el submarino
    private Rigidbody propLeftDownRg;
    private Rigidbody propRightUpRg;
    private Rigidbody propRightDownRg;
    private Rigidbody propUpFrontRg;
    private Rigidbody propUpRearRg;
    private Rigidbody propDownFrontRg;
    private Rigidbody propDownRearRg;

    #endregion

    #region Public Attributes
    public GameObject submarine;
    public GameObject propFwd;
    public GameObject propBackw;
    public GameObject propLeftUp;//estan puestos segun su posicion en el submarino
    public GameObject propLeftDown;
    public GameObject propRightUp;
    public GameObject propRightDown;
    public GameObject propUpFront;
    public GameObject propUpRear;
    public GameObject propDownFront;
    public GameObject propDownRear;

    public float accelMultiplier;
    public float accelRotationMultiplier;

#endregion

#region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        submarineRg = submarine.GetComponent<Rigidbody>();
        FwdRg = propFwd.GetComponent<Rigidbody>();
        BackwRg = propBackw.GetComponent<Rigidbody>();

        propLeftUpRg = propLeftUp.GetComponent<Rigidbody>();
        propLeftDownRg = propLeftDown.GetComponent<Rigidbody>();
        propRightUpRg = propRightUp.GetComponent<Rigidbody>();
        propRightDownRg = propRightDown.GetComponent<Rigidbody>();

        propUpFrontRg = propUpFront.GetComponent<Rigidbody>();
        propUpRearRg = propUpRear.GetComponent<Rigidbody>();
        propDownFrontRg = propDownFront.GetComponent<Rigidbody>();
        propDownRearRg = propDownRear.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = Time.deltaTime;

        UpdateMovement(dt);
    }
#endregion

#region HumanMade Methods

    private void UpdateMovement(float dt)
    {
        if (Input.GetKey(KeyCode.W))
        {
            FwdRg.AddForce(FwdRg.transform.forward.normalized * accelMultiplier, ForceMode.Force);

        }

        if (Input.GetKey(KeyCode.S))
        {
            BackwRg.AddForce(BackwRg.transform.forward.normalized * -accelMultiplier, ForceMode.Force);
            
        }//Palante patras

        if (Input.GetKey(KeyCode.Q))
        {
            propRightUpRg.AddForce(propRightUpRg.transform.right.normalized * -accelRotationMultiplier, ForceMode.Force);
            propLeftDownRg.AddForce(propLeftDownRg.transform.right.normalized * accelRotationMultiplier, ForceMode.Force);

        }

        if (Input.GetKey(KeyCode.E))
        {
            propRightDownRg.AddForce(propRightDownRg.transform.right.normalized * -accelRotationMultiplier, ForceMode.Force);
            propLeftUpRg.AddForce(propLeftUpRg.transform.right.normalized * accelRotationMultiplier, ForceMode.Force);

        }//Rotacion momentazgo

        if (Input.GetKey(KeyCode.A))
        {
            propRightUpRg.AddForce(propRightUpRg.transform.right.normalized * -accelMultiplier, ForceMode.Force);
            propRightDownRg.AddForce(propRightDownRg.transform.right.normalized * -accelMultiplier, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            propLeftDownRg.AddForce(propLeftDownRg.transform.right.normalized * accelMultiplier, ForceMode.Force);
            propLeftUpRg.AddForce(propLeftUpRg.transform.right.normalized * accelMultiplier, ForceMode.Force);

        }//Movimiento lateral

        if (Input.GetKey(KeyCode.Alpha1))
        {
            propUpFrontRg.AddForce(propUpFrontRg.transform.up.normalized * accelMultiplier, ForceMode.Force);
            propUpRearRg.AddForce(propUpRearRg.transform.up.normalized * accelMultiplier, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            propDownFrontRg.AddForce(propDownFrontRg.transform.up.normalized * -accelMultiplier, ForceMode.Force);
            propDownRearRg.AddForce(propDownRearRg.transform.up.normalized * -accelMultiplier, ForceMode.Force);

        }//Subida y bajada


        if (Input.GetKey(KeyCode.Alpha3))
        {
            propUpFrontRg.AddForce(propUpFrontRg.transform.up.normalized * -accelMultiplier, ForceMode.Force);
            propDownRearRg.AddForce(propDownRearRg.transform.up.normalized * accelMultiplier, ForceMode.Force);
            
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            propDownFrontRg.AddForce(propDownFrontRg.transform.up.normalized * accelMultiplier, ForceMode.Force);
            propUpRearRg.AddForce(propUpRearRg.transform.up.normalized * -accelMultiplier, ForceMode.Force);

        }//Cabeceo re duro

    }
#endregion

}
