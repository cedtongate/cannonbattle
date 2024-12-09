using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform playerTransform; 
    // private Vector3 offset = new Vector3(-3f, -0.8f, 0);    // Offset to keep camera behind the player
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("PlayerBoat");
        playerTransform = playerObj.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position -  playerTransform.rotation * offset;
            transform.LookAt(playerTransform);
        }
    }
    
}
