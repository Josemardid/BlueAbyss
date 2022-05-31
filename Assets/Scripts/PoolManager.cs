using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Private Attributes

    private List<GameObject> bulletList = new List<GameObject>();

    #endregion

    #region Public Attributes

    public int numBullets = 15;
    public GameObject bulletPrefab;
    //Se puede escalar a mas listas con otro index y metiendo cosas al if del GetObject


    public static PoolManager instance;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        instance = this;

        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject go = Instantiate(bulletPrefab); 
            go.name = "Bullet-" + i;
            go.SetActive(false);
            bulletList.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#endregion

#region HumanMade Methods

    public GameObject GetObject(Vector3 posToStart, string objName)
    {
        if(objName == "Bullet")
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].activeInHierarchy)
                {
                    bulletList[i].SetActive(true);
                    bulletList[i].transform.position = posToStart;
                    return bulletList[i];
                }
            }
        }


        return null;
    }


#endregion

}
