using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWakame : MonoBehaviour
{
    #region Private Attributes
    private Vector3 initialPos;

    private bool altDirection = false;

    private float speedX;
    
    #endregion

    #region Public Attributes
    

    public float maxDiffMovement;

    public float speed;

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
        float dt = Time.deltaTime;


        WakameDance(dt);
        
    }
#endregion

#region HumanMade Methods

    private void WakameDance(float dt)
    {

            speedX = speed;

            if (altDirection)
            {
                this.gameObject.transform.position += new Vector3(speedX, 0, 0) * dt;
            }
            else
            {
                this.gameObject.transform.position -= new Vector3(speedX, 0, 0) * dt;
            }



            if (this.gameObject.transform.position.x <= initialPos.x - maxDiffMovement)
            {

                altDirection = true;
                
            }
            else if (this.gameObject.transform.position.x > initialPos.x + maxDiffMovement)
            {
                altDirection = false;
                
            }



        
    }


#endregion

}
