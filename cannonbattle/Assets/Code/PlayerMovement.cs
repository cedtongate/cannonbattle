using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private AudioSource boatAudio;

    private Vector3 lastPosition;
    private float turnSpeed = 35f;      // Speed at which the boat turns (rotation speed)
    private float maxSpeed = 2f;
    private float Deceleration = 2f;
    public float MaximumThrust = 4f;
    private float thrust;
    private Rigidbody playerRB;
    // Start is called before the first frame update
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
            if(playerRB.velocity.magnitude > 0f){
                Vector3 decelerationForce = -playerRB.velocity.normalized * Deceleration * Time.fixedDeltaTime;
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

        float turnInput = Input.GetAxis("Horizontal"); 
    
        // // Rotate the boat left/right based on the horizontal input
        transform.Rotate(transform.up, turnInput * turnSpeed * Time.fixedDeltaTime);

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
      
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("Walls")){
            playerRB.velocity = Vector3.zero;
        }
    }

    public void SceneSwitch()
    {
        SceneManager.LoadScene("endLoseMenu");
    }
}
