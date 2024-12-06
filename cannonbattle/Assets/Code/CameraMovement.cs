using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform playerTransform;  // The player's transform
    private Vector3 offset = new Vector3(-3, -0.5f, 0);    // Offset to keep camera behind the player
    
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
            // Rotate camera based on mouse input (Horizontal rotation)
            // float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
            // playerTransform.Rotate(0, horizontalInput, 0);

            // Adjust camera position to be behind and above the player, using the offset
            transform.position = playerTransform.position -  playerTransform.rotation * offset;

            // Ensure camera looks at the player
            transform.LookAt(playerTransform);
        }
    }
    
}
