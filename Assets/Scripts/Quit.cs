using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    
    public void quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton3))//Press Y - Goes to Main Menu
        {
            SceneManager.LoadScene(1); //Loads Main Menu Scene
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))//Press B - Quits Application
        {
            Application.Quit();
        }
    }
}
