using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public Transform player;
    public bool m_IsPlayerInRange;
    private bool seesPlayer;
    public Transform raySpawn;

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    public bool getSeesPlayer()
    {
        return seesPlayer;
    }

    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - raySpawn.position + Vector3.up;
            Ray ray = new Ray(raySpawn.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    seesPlayer = true;
                }
                else
                {
                    seesPlayer = false;
                }
            }
        }
        else
        {
            seesPlayer = false;
        }
    }
}
