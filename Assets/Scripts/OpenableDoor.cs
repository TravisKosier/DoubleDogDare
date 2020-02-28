using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoor : MonoBehaviour
{
    public Transform playerCamera;
    public bool isSeen;
    public string flavorText;
    
    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.transform == this.transform)//Object is seen
            {
                isSeen = true;
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2)) //Controller support
                {
                    transform.parent.gameObject.GetComponent<SpawnEnemy>().doorOpen = true;
                    flavorText = "The door is stuck open now.";
                }

            }
            else
            {
                isSeen = false;
            }
        }
        else
        {
            isSeen = false;
        }

    }
}
