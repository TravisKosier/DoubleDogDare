using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Retry : MonoBehaviour
{
    public GameObject pauseScreen;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void retry()
    {
        ChangeScene(1); //SampleScene
    }
    public void mainMenu()
    {
        if (Time.timeScale == 0) // Unpauses
        {
            Time.timeScale = 1;
            //Cursor.lockState = CursorLockMode.Locked;
            pauseScreen.SetActive(false);
            //hidePaused();
        }
        ChangeScene(0);
    }
}
