using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    PlayerController playController;

    float zDistance;

    void Awake()
    {
        zDistance = -10f;

        player = GameObject.FindWithTag("Player");
        playController = player.GetComponent<PlayerController>();

        transform.position = new Vector3(player.transform.position.x, transform.position.y, zDistance);
    }

    void LateUpdate()
    {
        if (playController.Contacts == 0 || playController.Contacts == 1)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zDistance);
        else
            transform.position = new Vector3(player.transform.position.x, transform.position.y, zDistance);
    }
}