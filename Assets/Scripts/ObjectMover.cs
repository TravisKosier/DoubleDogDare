using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject item;
    public GameObject tempParent;
    public Transform guide;

    public SphereCollider box;

    private Rigidbody rb;

    public float throwStrength=10;

    private int time = 2;

    public bool audible;

    public float fallMultiplier = 2.5f;

    public bool isHolding;

    public Transform playerCamera;

    public bool isSeen;

    public AudioSource hitSound;
    private float timer = 0.0f;
    public bool canGenerateSounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = item.GetComponent<Rigidbody>();
       // box = GetComponentInChildren<SphereCollider>();
            rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > 10.0f)
        {
            canGenerateSounds = true;
        }

        if (rb.velocity.y < 0) //'Realistic' object falling
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.transform == this.transform) //Object is being looked at by player
            {
                isSeen = true;
                if (Input.GetKeyDown(KeyCode.JoystickButton2) && !isHolding)
                {
                    OnMouseDown();
                    
                }
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton2) && isHolding)
            {
                OnMouseUp();
                
            }
            else
            {
                isSeen = false;
            }
        }
        else {
            isSeen = false;
        }
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(guide.position, transform.position) <= 2)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            item.transform.position = guide.transform.position;
            item.transform.rotation = guide.transform.rotation;
            item.transform.parent = tempParent.transform;
        }
        isHolding = true;
    }
  

    void OnMouseUp()
    {
        if (Vector3.Distance(guide.position, transform.position) <= 2)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            item.transform.parent = null;
            item.transform.position = guide.transform.position;
            rb.AddRelativeForce(new Vector3(0, 0, throwStrength), ForceMode.Impulse);
            item.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        isHolding = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor") && canGenerateSounds)
        {
          StartCoroutine(Coroutine());
            hitSound.PlayDelayed(0);
        }
        else
        {
            audible = false; //box.enabled = false;
        }
    }
   
    private IEnumerator Coroutine()
    {
        audible = true; 
        // box.enabled = true;
        yield return new WaitForSeconds(1f);
        //box.enabled = false;
        audible = false;
    }

    public bool GetAudible()
    {
        return audible;
    }
    
}
