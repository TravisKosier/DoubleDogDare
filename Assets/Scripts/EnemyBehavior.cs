using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    private bool seesPlayer;
    public GameObject player;
    public float dist;
    public GameObject visionCone;
    public Transform soundSource;
    public bool contact;
    public AudioSource bark1Source;
    public AudioSource bark2Source;
    public AudioClip bark1;
    public AudioClip bark2;
    public bool canBark;
    public bool canBark2;
    public float chaseTimer;

    public enum EnemyStates
    {
        PATROL,
        CHASE,
        DISTRACTED
    }
    public EnemyStates enemyState;

    int m_CurrentWaypointIndex;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other != null)
        {
            if (other.gameObject.CompareTag("Distraction"))
            {
                try //This throws error for some reason, even though the function works as intended
                {
                    if (other.transform.parent.gameObject.GetComponent<ObjectMover>().GetAudible())
                    {
                        contact = true;
                        soundSource = other.transform;
                        enemyState = EnemyStates.DISTRACTED;
                    }
                }
                catch { }
            }
        } 
    }

    void Update()
    {
        switch (enemyState) //Alert or not alert
        {
            case EnemyStates.PATROL:
                {
                    StopCoroutine(Barktime(0));
                    StopCoroutine(Barktime2(0));
                    seesPlayer = visionCone.GetComponent<Detection>().getSeesPlayer(); //Is player seen?
                    if (!seesPlayer) //If no, keep patrolling
                    {
                        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
                        {
                            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                        }
                    }
                    else //If yes, start pursuing towards player
                    {
                        enemyState = EnemyStates.CHASE;
                    }
                }
                break;
            case EnemyStates.CHASE:
                {
                    StopCoroutine(Barktime2(0));
                    if (canBark == true)
                    {
                        StartCoroutine(Barktime(0));
                    }
                    seesPlayer = visionCone.GetComponent<Detection>().getSeesPlayer(); //Is player seen?
                    if (seesPlayer) //If yes, keep pursuing
                    {
                        navMeshAgent.SetDestination(player.transform.position); //Move towards player
                    }
                    else //If no, stop pursuing, start patrolling
                    {
                        chaseTimer += Time.deltaTime; //Start counting for 3 seconds after player breaks LoS
                        if (chaseTimer > 3.0f) //After 3 seconds, enemy gives up
                        {
                            enemyState = EnemyStates.PATROL;
                            chaseTimer = 0f;
                        }
                        else //During the 3 seconds, continue to chase
                        {
                            navMeshAgent.SetDestination(player.transform.position); //Move towards player
                        }
                    }
                }
                break;
            case EnemyStates.DISTRACTED:
                {
                    StopCoroutine(Barktime(0));
                    if (canBark2 == true)
                    {
                        StartCoroutine(Barktime2(0));
                    }
                    seesPlayer = visionCone.GetComponent<Detection>().getSeesPlayer(); //Is player seen?
                    if (!seesPlayer) //If no, keep moving to source of sound
                    {
                        navMeshAgent.SetDestination(soundSource.position); //Move towards sound
                        dist = Vector3.Distance(this.transform.position, soundSource.position); //Check if enemy has reached source of noise
                        if (dist <= 4) //If very close to source of sound
                        {
                            enemyState = EnemyStates.PATROL; //Return to patrolling
                            soundSource = null;
                            contact = false;
                        }
                    }
                    else //If yes, start pursuing towards player
                    {
                        enemyState = EnemyStates.CHASE;
                    }
                }
                break;
        }         
    }

    IEnumerator Barktime(float waitTime)// Invincible timer
    {
        canBark = false;
        bark1Source.clip = bark1;
        //bark.PlayDelayed(1);
        bark1Source.PlayOneShot(bark1);
        yield return new WaitForSeconds(3f);
        canBark = true;
    }

    IEnumerator Barktime2(float waitTime)// Invincible timer
    {
        canBark2 = false;
        bark2Source.clip = bark2;
        //bark.PlayDelayed(1);
        bark2Source.PlayOneShot(bark2);
        yield return new WaitForSeconds(2.5f);
        canBark2 = true;
    }
}
