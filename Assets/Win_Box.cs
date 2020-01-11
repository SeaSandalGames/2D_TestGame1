using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Box : MonoBehaviour
{
    void OnTriggerEnter2d(Collider2D collider)
    {
        Debug.Log("OnTriggerEnter2D");
    }
}
