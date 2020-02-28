using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject guard;
    public GameObject player;
    public Transform[] waypoints;
    public Transform playerCapsule;
    public bool hasSpawned = false;
    //public float rotationSpeed = 0.1f; //Larger number is slower
    //public Transform startRotation;
    public Transform endRotiation;
    public bool doorOpen;

    private void Start()
    {
        //Set properties of the guard in the EnemyBehavior and Detection scripts
        guard.GetComponent<EnemyBehavior>().player = player;
        guard.GetComponent<EnemyBehavior>().waypoints = waypoints;
        guard.transform.GetChild(1).GetComponent<Detection>().player = playerCapsule; //needs to get Detection script of VisionCapsule child of guard prefab
    }

    private void Update()
    {
        if (doorOpen && !hasSpawned)
        {
            spawnGuard();
        }
        if (hasSpawned)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, endRotiation.rotation, Time.deltaTime);
        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !hasSpawned)
        {
            spawnGuard();
            transform.rotation = Quaternion.Slerp(transform.rotation, endRotiation.rotation, Time.deltaTime);
        }
    }*/

    void spawnGuard()
    {
        Instantiate(guard, spawnPoint);
        hasSpawned = true;
    }
}
