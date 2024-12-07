using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private AudioSource boatAudio;

    private Vector3 lastPosition;
    // Start is called before the first frame update
    public float moveSpeed = 5f;        // Speed at which the boat moves forward/backward
    private float turnSpeed = 35f;      // Speed at which the boat turns (rotation speed)

    public float PitchRange = 5f;
    public float RollRange = 5f;
 
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
    void Start()
    {
        boatAudio = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody>();   
        lastPosition = transform.position;
    }

    // Update is called once per frame
    internal void FixedUpdate(){
        
        // Deceleration is not working right now!
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

        float turnInput = Input.GetAxis("Horizontal");  // Typically, "Horizontal" is mapped to the left joystick's horizontal axis
    
        // // Rotate the boat left/right based on the horizontal input
        transform.Rotate(transform.up, turnInput * turnSpeed * Time.deltaTime);

        if(playerRB.velocity.magnitude > 0f){
            if(!boatAudio.isPlaying){
                boatAudio.Play();
            }
        }
        else {
            if(boatAudio.isPlaying){
                boatAudio.Stop();
            }
        }

        if(transform.position == lastPosition){
            if(boatAudio.isPlaying){
                boatAudio.Stop();
            }
        }
        lastPosition = transform.position;

        // float horizontalInput = Input.GetAxis("Horizontal");
        // float joystickroll = horizontalInput*RollRange;
        // roll = Mathf.Lerp(roll, joystickroll, 0.01f);
    
        // // Yaw Calculation
        // float joystickyaw = yaw + roll * turnSpeed * Time.fixedDeltaTime;
        // yaw = joystickyaw;
        // Quaternion rotate = Quaternion.Euler(roll, yaw, pitch);
        // playerRB.MoveRotation(rotate);

    }

    void OnCollisionEnter(Collision col){
        if (col.gameObject.CompareTag("Walls")){
            playerRB.velocity = Vector3.zero;
        }
    }
}
