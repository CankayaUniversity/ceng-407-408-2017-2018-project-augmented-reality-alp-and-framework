using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void OrientationMode()
    {
        SceneManager.LoadScene("OrientationMode");
    }

    public void SystemDashboard()
    {
        SceneManager.LoadScene("SystemDashboard");
    }

    public void Quit()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
}
