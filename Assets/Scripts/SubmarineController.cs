using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineController : MonoBehaviour
{
    #region Private Attributes
    public Rigidbody submarineRg;
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


    private bool keyW;
    private bool keyA;
    private bool keyS;
    private bool keyD;
    private bool keyQ;
    private bool keyE;
    private bool key1;
    private bool key2;
    private bool key3;
    private bool key4;
    private bool keySpace;

    public Quaternion desiredRotation = Quaternion.Euler(-90,0,0);

    private bool currentlyCorrecting=false;
    private bool submarineStopped=false;

    private float timerBullet = 0f;
    private float totalTimeToCorrect = 3.0f;
    public float timeToCorrect = 0;
    public float SlerpTime = 0;

    private int pearlsCollected = 0;
    private int pearlsToCollect = 0;

    private AudioManager audio;
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

    public Transform placeToShoot;

    public float accelMultiplier;
    public float accelRotationMultiplier;
    public float bulletImpulse;
    public float timeToShoot;

    public GameObject Arm;

    public Text ScoreText;



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


        ScoreText.text = "Pearls: " + pearlsCollected + "/" + pearlsToCollect;

        audio = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        UpdateInputMovement();

        UpdateFire(Time.deltaTime);

        //Debug.Log("Pearls: " + pearlsCollected);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = Time.deltaTime;

        UpdateMovement(dt);
    }
#endregion

#region HumanMade Methods
    public void setAllPearls(int p)
    {
        pearlsToCollect = p;
        ScoreText.text = "Pearls: " + pearlsCollected + "/" + pearlsToCollect;
    }



    private void UpdateInputMovement()
    {
        keyW = Input.GetKey(KeyCode.W);
        keyA = Input.GetKey(KeyCode.A);
        keyS = Input.GetKey(KeyCode.S);
        keyD = Input.GetKey(KeyCode.D);
        keyQ = Input.GetKey(KeyCode.Q);
        keyE = Input.GetKey(KeyCode.E);
        key1 = Input.GetKey(KeyCode.Alpha1);
        key2 = Input.GetKey(KeyCode.Alpha2);
        key3 = Input.GetKey(KeyCode.Alpha3);
        key4 = Input.GetKey(KeyCode.Alpha4);
        keySpace = Input.GetKey(KeyCode.Space);
//SHit ya lo siento

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Arm.GetComponent<ArmControllerIK>().isSearching)
            {
                audio.Play("Arm");
                Arm.GetComponent<ArmControllerIK>().isSearching = false;
            }
            else
            {
                //if (Arm.GetComponent<ArmControllerIK>().toDestroy != null)
                //{
                //    Arm.GetComponent<ArmControllerIK>().toDestroy.SetActive(false);
                //}
                audio.Play("Arm");
                Arm.GetComponent<ArmControllerIK>().SearchTarget();
                Arm.GetComponent<ArmControllerIK>().isSearching = true;
            }

        }
    }


    public void addPearlCollected()
    {
        pearlsCollected++;
        ScoreText.text = "Pearls: " + pearlsCollected + "/" + pearlsToCollect;
        audio.Play("Arm");
        Arm.GetComponent<ArmControllerIK>().hasSoundedYet = false;
    }


    private void UpdateMovement(float dt)
    {
        if (!currentlyCorrecting)
        {
            if (keyW)
            {
                FwdRg.AddForce(FwdRg.transform.forward.normalized * -accelMultiplier, ForceMode.Force);

            }

            if (keyS)
            {
                BackwRg.AddForce(BackwRg.transform.forward.normalized * accelMultiplier, ForceMode.Force);
            
            }//Palante patras

            if (keyQ)
            {
                propRightUpRg.AddForce(propRightUpRg.transform.right.normalized * accelRotationMultiplier, ForceMode.Force);
                propLeftDownRg.AddForce(propLeftDownRg.transform.right.normalized * -accelRotationMultiplier, ForceMode.Force);

            }

            if (keyE)
            {
                propRightDownRg.AddForce(propRightDownRg.transform.right.normalized * accelRotationMultiplier, ForceMode.Force);
                propLeftUpRg.AddForce(propLeftUpRg.transform.right.normalized * -accelRotationMultiplier, ForceMode.Force);

            }//Rotacion momentazgo

            if (keyA)
            {
                propRightUpRg.AddForce(propRightUpRg.transform.right.normalized * -accelMultiplier, ForceMode.Force);
                propRightDownRg.AddForce(propRightDownRg.transform.right.normalized * -accelMultiplier, ForceMode.Force);
            }

            if (keyD)
            {
                propLeftDownRg.AddForce(propLeftDownRg.transform.right.normalized * accelMultiplier, ForceMode.Force);
                propLeftUpRg.AddForce(propLeftUpRg.transform.right.normalized * accelMultiplier, ForceMode.Force);

            }//Movimiento lateral

            if (key1)
            {
                propUpFrontRg.AddForce(propUpFrontRg.transform.up.normalized * accelMultiplier, ForceMode.Force);
                propUpRearRg.AddForce(propUpRearRg.transform.up.normalized * accelMultiplier, ForceMode.Force);
            }

            if (key2)
            {
                propDownFrontRg.AddForce(propDownFrontRg.transform.up.normalized * -accelMultiplier, ForceMode.Force);
                propDownRearRg.AddForce(propDownRearRg.transform.up.normalized * -accelMultiplier, ForceMode.Force);

            }//Subida y bajada


            if (key3)
            {
                propUpFrontRg.AddForce(propUpFrontRg.transform.up.normalized * -accelRotationMultiplier, ForceMode.Force);
                propDownRearRg.AddForce(propDownRearRg.transform.up.normalized * accelRotationMultiplier, ForceMode.Force);
            
            }

            if (key4)
            {
                propDownFrontRg.AddForce(propDownFrontRg.transform.up.normalized * accelRotationMultiplier, ForceMode.Force);
                propUpRearRg.AddForce(propUpRearRg.transform.up.normalized * -accelRotationMultiplier, ForceMode.Force);

            }//Cabeceo re duro
            if (keySpace)
            {
                currentlyCorrecting = true;
            }
        }
        else
        {
            if (!submarineStopped)
            {
                submarineStopped = true;
                submarineRg.velocity = Vector3.zero;
                submarineRg.angularVelocity = Vector3.zero; 
                FwdRg.velocity = Vector3.zero;
                FwdRg.angularVelocity = Vector3.zero;
                BackwRg.velocity = Vector3.zero;
                BackwRg.angularVelocity = Vector3.zero;
                propLeftUpRg.velocity = Vector3.zero;
                propLeftUpRg.angularVelocity = Vector3.zero;
                propLeftDownRg.velocity = Vector3.zero;
                propLeftDownRg.angularVelocity = Vector3.zero;
                propRightUpRg.velocity = Vector3.zero;
                propRightUpRg.angularVelocity = Vector3.zero;
                propRightDownRg.velocity = Vector3.zero;
                propRightDownRg.angularVelocity = Vector3.zero;
                propUpFrontRg.velocity = Vector3.zero;
                propUpFrontRg.angularVelocity = Vector3.zero;
                propUpRearRg.velocity = Vector3.zero;
                propUpRearRg.angularVelocity = Vector3.zero;
                propDownFrontRg.velocity = Vector3.zero;
                propDownFrontRg.angularVelocity = Vector3.zero;
                propDownRearRg.velocity = Vector3.zero;
                propDownRearRg.angularVelocity = Vector3.zero;
            }

            timeToCorrect += dt;
            SlerpTime = timeToCorrect / totalTimeToCorrect;
            Quaternion.Slerp(submarine.transform.rotation,desiredRotation, SlerpTime);
            if (SlerpTime >= 1)
            {
                SlerpTime = 0;
                timeToCorrect = 0;
                currentlyCorrecting = false;
                submarineStopped = false;
            }
        }


    }


    private void UpdateFire(float dt) {

        timerBullet += dt;

        if (Input.GetKeyDown(KeyCode.Mouse0) && timerBullet >= timeToShoot)
        {
            GameObject go = PoolManager.instance.GetObject(placeToShoot.position,"Bullet");

            if(go != null)
            {
                go.GetComponent<Rigidbody>().AddForce(-placeToShoot.up.normalized * bulletImpulse, ForceMode.Impulse);
            }

            timerBullet = 0;
            //Disparar
            audio.Play("Torpedo");
        }

    }
#endregion

}
