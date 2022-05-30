using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    #region Private Attributesç
    private bool isPaused = false;
    #endregion

    #region Public Attributes

    public GameObject pausePanel;
#endregion

#region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            PauseFunction();
        }
    }
#endregion

#region HumanMade Methods

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PauseFunction()
    {
        if (isPaused)
        {
            pausePanel.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
    }


#endregion

}
