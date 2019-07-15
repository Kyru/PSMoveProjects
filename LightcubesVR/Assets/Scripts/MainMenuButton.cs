using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void goToScene()
    {
        if (this.gameObject.name == "2 Players")
        {
            this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 100);
            SceneManager.LoadScene("SableLaserPrototipoP2", LoadSceneMode.Single);
        }
        else if(this.gameObject.name == "1 Player")
        {
            this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 100);
            SceneManager.LoadScene("SableLaserPrototipoP1", LoadSceneMode.Single);
        }
    }
}
