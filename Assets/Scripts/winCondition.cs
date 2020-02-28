using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winCondition : MonoBehaviour
{
   


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Win"))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(2); //End Screen
        }
    }
}
