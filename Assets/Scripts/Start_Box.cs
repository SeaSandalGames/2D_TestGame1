using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Box : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void Awake()
    {
        // create single Player
        // have to provide rotation so transform.position can be used
        Instantiate(player, transform.position, transform.rotation);
    }
}
