using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Private Attributes
    private float timeToLive;
    private float timer;
    private bool onePearl = true;
    #endregion

    #region Public Attributes
    public float timeAlive = 5f;
    public GameObject pearlPrefab;
    

#endregion

#region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        timeToLive = timeAlive;
        timer = timeAlive;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer<= 0)
        {
            EndBullet();

        }

    }
#endregion

#region HumanMade Methods

    public void EndBullet()
    {

        timeAlive = timeToLive;
        timer = timeToLive;
        this.gameObject.SetActive(false);

        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target" && onePearl)
        {
            
            Instantiate<GameObject>(pearlPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            EndBullet();

            onePearl = false;// para que salga dos veces
        }
    }

    #endregion

}
