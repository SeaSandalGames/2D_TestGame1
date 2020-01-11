using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// control the player character's movement and actions
public class PlayerController : MonoBehaviour
{
   // ArrayList contacts;

    Collider2D playCollider;
    Rigidbody2D playRigidbody;

    // used to check when player is falling
    private int contacts;
    bool rampContact;

    void Awake()
    {
        contacts = 0;
    }

    void Start()
    {
        playCollider = GetComponent<Collider2D>();
        playRigidbody = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z);
    }

    // get player's transform and move left/right 1 unit
    void Update()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z);
        // using GetButton will allow end-users to change the input button;
        // consider using that
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && contacts > 0)
        {
            //transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z);
            if (Input.GetKeyDown("d"))
                transform.position += Vector3.right;
            else if (Input.GetKeyDown("a"))
                transform.position -= (Vector3.right);

            if (rampContact)
            {
                transform.position += (Vector3.up * 0.5f);
            }
        }

        if (contacts == 0)
            playRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else if (rampContact)
            playRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        else
            playRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        contacts = ++contacts;
        Debug.Log("entered " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Interactable_Ramp")
        { rampContact = true; }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        contacts = --contacts;
        Debug.Log("exited " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Interactable_Ramp")
        { rampContact = false; }
    }

    // read-only
    public int Contacts
    {
        get { return contacts;}
    }
}
