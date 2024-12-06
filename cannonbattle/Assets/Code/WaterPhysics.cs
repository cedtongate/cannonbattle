using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody playerBody;

    // private float waterDepth = -3.5f;  // The base level of the water (constant)

    private float gravityForce;
    private bool isInWater = false;

    // private float buoyancyFactor = 1f; // Adjust this value to tweak buoyancy strength

    void Start()
    {
        GameObject playerBoat = GameObject.FindWithTag("PlayerBoat");
        playerBody = playerBoat.GetComponent<Rigidbody>();
        gravityForce = playerBody.mass * Physics.gravity.y; // Calculate gravity force for compensation
    }
    
    void Update()
    {
        // Calculate the dynamic water level (constant since displacement is 0)
        // float dynamicWaterLevel = waterDepth + displacement;

        // // Calculate the boat's displacement below the water
        // float displacementBelowWater = playerBody.position.y - dynamicWaterLevel;

        // // Apply buoyancy force only when the boat is below the water level
        // if (displacementBelowWater < 0.5)
        // {
        //     // Apply a buoyancy force proportional to how much of the boat is submerged
        //     float buoyancyForce = Mathf.Abs(displacementBelowWater) * buoyancyFactor;
        //     playerBody.AddForce(Vector3.up * buoyancyForce, ForceMode.Force);
        // }
    }

    // FixedUpdate is used to apply gravity compensation or any other physics forces
    void FixedUpdate()
    {
        if (isInWater)
        {
            // ApplyGravityCompensation();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the boat enters the water
        if (other.CompareTag("PlayerBoat"))
        {
            isInWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Ensure gravity compensation stops when the boat leaves the water
        if (other.CompareTag("PlayerBoat"))
        {
            isInWater = false;
        }
    }

    void ApplyGravityCompensation()
    {
        // Apply upward gravity compensation force to keep the boat from sinking too much
        playerBody.AddForce(Vector3.up * gravityForce, ForceMode.Force);
    }
}
