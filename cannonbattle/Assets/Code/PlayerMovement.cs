using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;        // Speed at which the boat moves forward/backward
    private float turnSpeed = 35f;      // Speed at which the boat turns (rotation speed)

    public float PitchRange = 45f;
    public float RollRange = 10f;
 
    private float maxSpeed = 2f;
    private float Deceleration = 2f;
    public float MaximumThrust = 4f;
    private float yaw;
    /// <summary>
    /// Current pitch (rotation about the X axis)
    /// </summary>
    private float pitch;
    /// <summary>
    /// Current roll (rotation about the Z axis)
    /// </summary>
    private float roll;
    /// <summary>
    /// Current thrust (forward force provided by engines
    /// </summary>
    private float thrust;

    private Rigidbody playerRB;
    private float gravityForce;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    internal void FixedUpdate(){
        
        // float horizontalInput = Input.GetAxis("Horizontal");
        
        
        // float verticalInput = Input.GetAxis("Vertical");
        // float joystickpitch = verticalInput*PitchRange;
        // pitch = Mathf.Lerp(pitch, joystickpitch, 0.01f);
        // // Yaw Calculation
        // float joystickyaw = yaw + roll * RotationalSpeed * Time.fixedDeltaTime;
        // yaw = joystickyaw;
        // Quaternion rotate = Quaternion.Euler(pitch, yaw, roll);
        // playerRB.MoveRotation(rotate);

        // Applying thrust to the plane
        float thrustInput = -1f * Input.GetAxis("Vertical");
        if(thrustInput < 0f){
            thrustInput = 0f;
            if(playerRB.velocity.magnitude > 0){
                Vector3 decelerationForce = -playerRB.velocity.normalized * Deceleration;
                playerRB.AddForce(decelerationForce, ForceMode.Acceleration);
            }
           
        }
        else{
            thrust = thrustInput * MaximumThrust;
            playerRB.AddForce(-transform.right * thrust, ForceMode.Acceleration);
        }

        if (playerRB.velocity.magnitude > maxSpeed){
            playerRB.velocity = playerRB.velocity.normalized * maxSpeed;
        }
        // float moveInput = Input.GetAxis("Vertical");    // Typically, "Vertical" is mapped to the left joystick's vertical axis
        float turnInput = Input.GetAxis("Horizontal");  // Typically, "Horizontal" is mapped to the left joystick's horizontal axis
        
        // float joystickroll = turnInput*RollRange;
        // roll = Mathf.Lerp(roll, joystickroll, 0.01f);
        
        // Quaternion currentRotation = playerRB.rotation;
        // Quaternion rollRotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, roll);

        // Quaternion rotate = Quaternion.Euler(0, 0, roll);
        // playerRB.MoveRotation(rotate);

        // Move the boat forward/backward based on the vertical input
        // transform.Translate(Vector3.left * moveInput * moveSpeed * Time.deltaTime);

        // Rotate the boat left/right based on the horizontal input
        transform.Rotate(transform.up, turnInput * turnSpeed * Time.deltaTime);
    }
}
