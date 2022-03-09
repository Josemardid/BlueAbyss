using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    #region Private Attributes
    private Rigidbody submarineRg; 

    #endregion

    #region Public Attributes
    public float accelMultiplier;

#endregion

#region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        submarineRg = this.GetComponent<Rigidbody>();
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            submarineRg.AddForce(this.transform.forward.normalized * accelMultiplier, ForceMode.Force);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            submarineRg.AddForce(this.transform.forward.normalized * -accelMultiplier, ForceMode.Force);
        }
    }
#endregion

}
