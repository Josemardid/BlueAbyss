using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehaviour : MonoBehaviour
{
    public float k;
    private float elongation;
    private float daRealXinFormula;

    public GameObject moveObj;

    private float distBtnObjs;
    private Vector3 forceVec;
    
    

    // Start is called before the first frame update
    void Start()
    {
        elongation = (moveObj.transform.position - this.gameObject.transform.position).magnitude;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        distBtnObjs = (moveObj.transform.position - this.gameObject.transform.position).magnitude;

      
        daRealXinFormula = Mathf.Abs(distBtnObjs - elongation);

        forceVec = (this.gameObject.transform.position- moveObj.transform.position).normalized * k * daRealXinFormula;

        moveObj.GetComponent<Rigidbody>().AddForce(forceVec, ForceMode.Force);
        
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(moveObj.transform.position, this.gameObject.transform.position, Color.green);
    }
}
