using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    Collider2D collide;
    Rigidbody2D rb;

    RaycastHit2D topLeftHit, topRightHit;
    RaycastHit2D bottomLeftHit, bottomRightHit;
    RaycastHit2D topHit;
    RaycastHit2D groundHit;

    // LR: left-ray, RR: right-ray
    Vector3 topLROffset, topRROffset;
    Vector3 bottomLROffset, bottomRROffset;
    Vector3 topRayOffset;
    Vector3 groundRayOffset;
    Vector3 halfboxOffset;

    bool isCarrying;
    /* TODO */
    bool lose;
    bool win;
    bool moveLeft;
    bool moveRight;
    bool halfboxLeft;
    bool halfboxRight;

    /* TODO: move fast by holding down movement; toggle */
    [SerializeField]
    bool moveFast;

    float offsetX, offsetY;
    float topOffset;
    float groundOffset;

    float longRayLength;
    float shortRayLength;

    void Awake()
    { 
        collide = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        // offsets for left and right rays
        offsetX = 0.5f;
        offsetY = 0.25f;

        topLROffset = new Vector3(-offsetX, offsetY, 0f);
        bottomLROffset = new Vector3(-offsetX, -offsetY, 0f);

        topRROffset = new Vector3(offsetX, offsetY, 0f);
        bottomRROffset = new Vector3(offsetX, -offsetY, 0f);

        // offset for top ray
        topOffset = 0.5f;
        topRayOffset = new Vector3(0f, topOffset, 0f);

        // offset for ground ray
        groundOffset = -0.5f;
        groundRayOffset = new Vector3(0f, groundOffset, 0f);

        // offset for putting player ontop a halfbox
        halfboxOffset = new Vector3(0f, 0.5f, 0f);

        longRayLength = 0.5f;
        shortRayLength = 0.01f;

        // bools
        moveLeft = false;
        moveRight = false;
        halfboxLeft = false;
        halfboxRight = false;

        /* TODO */
        win = false;
        moveFast = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Left") && moveLeft && !moveFast)
        {
                transform.position += Vector3.left;

                if (halfboxLeft)
                { transform.position += halfboxOffset; }

            // set the transform.position at a whole number
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        }
        else if (Input.GetButtonDown("Right") && moveRight && !moveFast)
        { 
            transform.position += Vector3.right;

            if (halfboxRight)
            { transform.position += halfboxOffset; }

            // set the transform.position at a whole number
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        }

        /* TODO: moving fast */
        if (Input.GetButton("Left") && moveLeft && moveFast)
        {
            transform.position += Vector3.left;
            if (halfboxLeft)
            { transform.position += halfboxOffset; }
            // set the transform.position at a whole number
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        }
        else if (Input.GetButton("Right") && moveRight && moveFast)
        {
            transform.position += Vector3.right;

            if (halfboxRight)
            { transform.position += halfboxOffset; }

            // set the transform.position at a whole number
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        }

        // if player carries interactable using left or right carry button
        if (Input.GetButtonDown("Left-Int"))
        {
            if (!isCarrying && bottomLeftHit.collider != null)
            { 
                Carry(bottomLeftHit.transform);
            }
            else if (isCarrying)
            {
                Drop(topHit.transform, Vector3.left);
            }
        }

        if (Input.GetButtonDown("Right-Int"))
        {
            if (!isCarrying && bottomRightHit.collider != null)
            { 
                Carry(bottomRightHit.transform);
            }
            else if (isCarrying)
            {
                Drop(topHit.transform, Vector3.right);
            }
        }
    }

    void FixedUpdate()
    {
        // create ray detecting ground and interactables
        // "~" used to detect all colliders except for the Player layer
        // example of it under: file:///C:/Users/jemub/Documents/UnityManual_2019.3/Documentation/en/ScriptReference/Physics.Raycast.html

        topLeftHit = Physics2D.Raycast(transform.position + topLROffset, Vector2.left, shortRayLength, ~LayerMask.GetMask("Player"));
        bottomLeftHit = Physics2D.Raycast(transform.position + bottomLROffset, Vector2.left, shortRayLength, ~LayerMask.GetMask("Player"));
        
        topRightHit = Physics2D.Raycast(transform.position + topRROffset, Vector2.right, shortRayLength, ~LayerMask.GetMask("Player"));
        bottomRightHit = Physics2D.Raycast(transform.position + bottomRROffset, Vector2.right, shortRayLength, ~LayerMask.GetMask("Player"));

        // different from other rays b/c only looking for Interactables on top of player
        topHit = Physics2D.Raycast(transform.position + topRayOffset, Vector2.up, longRayLength, LayerMask.GetMask("Interactable"));

        groundHit = Physics2D.Raycast(transform.position + groundRayOffset, Vector2.down, shortRayLength, ~LayerMask.GetMask("Player"));

        /* TODO: Debug Rays */
        Debug.DrawRay(transform.position + topLROffset, Vector2.left, Color.red);
        Debug.DrawRay(transform.position + topRROffset, Vector2.right, Color.red);
        Debug.DrawRay(transform.position + bottomLROffset, Vector2.left, Color.blue);
        Debug.DrawRay(transform.position + bottomRROffset, Vector2.right, Color.blue);
        Debug.DrawRay(transform.position + topRayOffset, Vector2.up, Color.white);
        Debug.DrawRay(transform.position + groundRayOffset, Vector2.down, Color.green);

        /* TODO: testing diagonal ray */
        Debug.DrawRay(transform.position, (Vector2.left + Vector2.down), Color.black);

        // what's next to the player?
        if (bottomLeftHit.collider == null)
        { 
            moveLeft = true;
            halfboxLeft = false;
        }
        else if (bottomLeftHit.collider != null)
        { 
            if (topLeftHit.collider == null)
            {
                moveLeft = true;
                halfboxLeft = true;
            }
            else if (topLeftHit.collider != null)
            {
                moveLeft = false;
                halfboxLeft = false;
            }
        }

        if (bottomRightHit.collider == null)
        { 
            moveRight = true;
            halfboxRight = false;
        }
        else if (bottomRightHit.collider != null)
        { 
            if (topRightHit.collider == null)
            {
                moveRight = true;
                halfboxRight = true;
            }
            else if (topRightHit.collider != null)
            { 
                moveRight = false;
                halfboxRight = false; 
            }
        }

        if (topHit.collider == null)
        { 
            isCarrying = false;
        }
        else
        { 
            isCarrying = true;
        }

        // is the player standing on anything?
        if (groundHit.collider == null)
        {   // make player fall
            Fall();
        }
        else if (groundHit.collider.tag == "Lose_Box")
        {   // destroy the player if they hit the Lose Box
            Destroy(gameObject);
        }
    }

    void Carry(Transform box)
    {
        /* TODO: may have to change this to something more efficient */
        box.gameObject.GetComponent<Interactable>().StayOnTop = true;
        box.position = transform.position + Vector3.up;
    }

    void Drop(Transform box, Vector3 direction)
    { 
        /* TODO: may have to change this to something more efficient */
        box.gameObject.GetComponent<Interactable>().StayOnTop = false;
        box.transform.position += direction;
    }

    // start player falling
    void Fall()
    {
        transform.position += Vector3.down * 0.05f;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Entered OnTriggerEnter2D");
    }
}
