using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;

    public float gravity = -9.81f;

    Vector3 velocity;

    public GameObject pauseScreen;

    public string currentRoom;

    public bool stepBool;
    public bool movingBool;
    public AudioClip stepGrassClip;
    public AudioClip stepWoodClip;
    public AudioSource step;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale == 1)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move*speed*Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) //If player is moving, play step audioClip
        {
            if (!step.isPlaying)
            {
                if (currentRoom == "yard")
                {
                    step.PlayOneShot(stepGrassClip);
                }
                else
                {
                    step.PlayOneShot(stepWoodClip);
                }
            }
        }

            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton7))//Code for pause button
        {
            if (Time.timeScale == 1) //Pauses
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                pauseScreen.SetActive(true);
                //showPaused(); 
            }
            else if (Time.timeScale == 0) // Unpauses
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                pauseScreen.SetActive(false);
                //hidePaused();
            }
        }
        if(Time.timeScale == 0)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton3))//Press Y - Goes to Main Menu
            {
                if (Time.timeScale == 0) // Unpauses
                {
                    Time.timeScale = 1;
                    pauseScreen.SetActive(false);
                }
                SceneManager.LoadScene(0); //Loads Main Menu Scene
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton1))//Press B - Quits Application
            {
                Application.Quit();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("McguffinRoom"))
        {
            currentRoom = "mcguffinroom";
        }
        if (other.CompareTag("Hallway"))
        {
            currentRoom = "hallway";
        }
        if (other.CompareTag("Yard"))
        {
            currentRoom = "yard";
        }
    }
}
