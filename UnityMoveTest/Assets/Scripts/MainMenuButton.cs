using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void goToScene()
    {
        if (this.gameObject.name == "2 Players")
        {
            SceneManager.LoadScene("SableLaserPrototipoP2", LoadSceneMode.Single);
        }
        else if(this.gameObject.name == "1 Player")
        {
            SceneManager.LoadScene("SableLaserPrototipoP1", LoadSceneMode.Single);
        }
    }
}
