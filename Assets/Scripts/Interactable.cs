using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // reference to the player
    GameObject player;

    // same ground from Ball_Controller;
    // TODO: may combine into one inheritable script for player
    // and interactables to get
    RaycastHit2D groundHit;
    Vector3 groundRayOffset;

    [SerializeField]
    bool stayOnTop;

    float groundOffset;
    float groundLength;

    void Awake()
    {
        if (transform.gameObject.tag == "Half_Box")
        { groundOffset = 0.26f; }
        else if (transform.gameObject.tag == "Box")
        { groundOffset = 0.51f; }

        groundRayOffset = new Vector3(0f, -groundOffset, 0f);

        // same as Ball_Controller's playerRayLength
        groundLength = 0.01f;

        stayOnTop = false;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (stayOnTop)
        { Stay(); }
    }

    void FixedUpdate()
    {
        groundHit = Physics2D.Raycast(transform.position + groundRayOffset, Vector2.down, groundLength);

        /* TODO: debug ray */
        Debug.DrawRay(transform.position + groundRayOffset, Vector2.down, Color.yellow);

        if (groundHit.collider == null && !stayOnTop)
        { Fall(); }
    }

    void Fall()
    {
        transform.position += Vector3.down * 0.05f;
        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, 0f);
    }

    void Stay()
    {
        transform.position = player.transform.position + Vector3.up;
    }

    public bool StayOnTop
    { 
        get { return stayOnTop; } 
        set { stayOnTop = value; } 
    }
}
