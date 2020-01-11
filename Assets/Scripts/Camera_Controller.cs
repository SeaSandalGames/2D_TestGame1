using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    GameObject player;

    float offsetX;
    float offsetY;
    float offsetZ;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");

        offsetX = 0f;
        offsetY = 2f;
        offsetZ = 10f;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, -offsetZ);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, -offsetZ);
    }
}
