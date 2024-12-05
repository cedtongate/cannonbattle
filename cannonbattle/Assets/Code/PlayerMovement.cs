using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float PitchRange = 45f;
    public float RollRange = 45;
    public float RotationalSpeed = 5f;

    public float MaximumThrust = 20f;
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
        playerRB.velocity = transform.forward*3;
        
    }

    // Update is called once per frame
    internal void FixedUpdate(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float joystickroll = horizontalInput*RollRange;
        
        float verticalInput = Input.GetAxis("Vertical");
        float joystickpitch = verticalInput*PitchRange;
        pitch = Mathf.Lerp(pitch, joystickpitch, 0.01f);
        // Yaw Calculation
        float joystickyaw = yaw + roll * RotationalSpeed * Time.fixedDeltaTime;
        yaw = joystickyaw;
        Quaternion rotate = Quaternion.Euler(pitch, yaw, roll);
        playerRB.MoveRotation(rotate);

        // Applying thrust to the plane
        float thrustInput = Input.GetAxis("Thrust");
        if(thrustInput < 0f){
            thrustInput = 0f;
        }
        thrust = thrustInput * MaximumThrust;
        playerRB.AddForce(transform.forward * thrust);

        
    }
}
