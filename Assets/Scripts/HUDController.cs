using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text objectiveText;
    public Text contextualText;
    public GameObject player;
    public GameObject[] distractions;
    public GameObject mcguffin;
    public GameObject doghouse;
    public GameObject door;
    private int step = 0;
    private bool seesDist = false;
    public Image fadeInBox;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        objectiveText.text = "";
        contextualText.text = "";
        fadeInBox.canvasRenderer.SetAlpha(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10.0f && step == 0)
        {
            fadeInBox.CrossFadeAlpha(0, 2, false);
            step = 1;
            objectiveText.text = "Objective: Enter the House.";
        }
        if(step == 0)
        {
            contextualText.text = "Hint: Press the 'P' key, or 'Start' on a controller, to pause the game.";

        }

        //Handle objective
        if (step == 1 && door.transform.parent.GetComponent<SpawnEnemy >().doorOpen) // Player hits E to open the door
        {
            objectiveText.text = "Objective: Find something in the house that belonged to the owner.";
            step = 2;
        }
        if (step == 2 && player.GetComponent<PlayerMover>().currentRoom == "mcguffinroom") //Player reaches final room
        {
            objectiveText.text = "Objective: Take the collar.";
            step = 3;
        }
        if (step == 3 && mcguffin.GetComponent<ObjectMover>().isHolding) //Collar has been picked up
        {
            objectiveText.text = "Objective: Escape with the collar.";
            step = 4;
        }
        if (step == 4 && player.GetComponent<PlayerMover>().currentRoom == "yard") //Player has made it back to the yard
        {
            objectiveText.text = "Objective: Return the collar to the doghouse.";
            step = 5;
        }

        //Handle contextual text
        foreach (var dist in distractions)
        {
            if (dist.GetComponent<ObjectMover>().isSeen) //Player looking at distration
            {
                seesDist = true;
            }
        }
        if (seesDist)
        {
            contextualText.text = "This could make a loud noise somewhere else when thrown.\n\nClick and hold or press X (on controller) to pick up, and let go or press X again to throw.";
        }
        else if (door.GetComponent<OpenableDoor>().isSeen) // Player looking at door
        {
            contextualText.text = door.GetComponent<OpenableDoor>().flavorText;
        }
        else if(doghouse.GetComponent<StandingObject>().isSeen) // Player looking at door
        {
            contextualText.text = doghouse.GetComponent<StandingObject>().flavorText;
        }
        else if(mcguffin.GetComponent<ObjectMover>().isSeen) // Player looking at macguffin
        {
            contextualText.text = "Take this and get out with it.\n";
        }
        else if(timer > 10.0f) //Nothing
        {
            contextualText.text = "";
        }
        seesDist = false;
    }
}
