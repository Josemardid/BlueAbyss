using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public float k;
    private float elongation;
    private float daRealXinFormula;

    public GameObject mainCamera;

    private float distBtnObjs;
    private Vector3 forceVec;

    public float radiusSphere = 2;

    public float distZ = 15;
    public float distY = 10;
   
    public Transform cameraPosition;


    // Start is called before the first frame update
    void Start()
    {
        cameraPosition.position = new Vector3(this.transform.position.x, this.transform.position.y + distY, this.transform.position.z - distZ);
        elongation = radiusSphere;
        mainCamera.transform.position = cameraPosition.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HookeMov();
        CameraClampSphere();
        mainCamera.transform.LookAt(this.transform);

        
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(cameraPosition.position, this.gameObject.transform.position, Color.green);
        Gizmos.DrawSphere(cameraPosition.position, radiusSphere);
    }

    void HookeMov()
    {

        distBtnObjs = (mainCamera.transform.position - cameraPosition.position).magnitude;


        daRealXinFormula = Mathf.Abs(distBtnObjs - elongation);

        forceVec = (cameraPosition.position - mainCamera.transform.position).normalized * k * daRealXinFormula;

        mainCamera.GetComponent<Rigidbody>().AddForce(forceVec, ForceMode.Force);
    }
    void CameraClampSphere()
    {
        Vector3 newPositionCam;
        newPositionCam.x = Mathf.Clamp(mainCamera.transform.position.x, mainCamera.transform.position.x - radiusSphere, mainCamera.transform.position.x + radiusSphere);
        newPositionCam.y = Mathf.Clamp(mainCamera.transform.position.y, mainCamera.transform.position.y - radiusSphere, mainCamera.transform.position.y + radiusSphere);
        newPositionCam.z = Mathf.Clamp(mainCamera.transform.position.z, mainCamera.transform.position.z - radiusSphere, mainCamera.transform.position.z + radiusSphere);
        mainCamera.transform.position = newPositionCam;

    }
}

