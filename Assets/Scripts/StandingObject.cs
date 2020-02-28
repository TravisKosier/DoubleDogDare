using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingObject : MonoBehaviour
{
    public string flavorText;
    private Rigidbody rb;
    public Transform playerCamera;
    public bool isSeen;

    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.transform == this.transform) //Object is being looked at by player
            {
                isSeen = true;
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
