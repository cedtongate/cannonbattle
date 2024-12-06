using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerFireSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cannonBallPrefab;

    private Transform playerTransform;
    // private Transform spawnPoint;
    private float maxUpwardForce = 2f;
    private float maxThrowForce = 3f;

    private float upwardForce;
    private float throwForce;
    public KeyCode fireButton;

    private GameObject cannonBall;

    void Start()
    {
        playerTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("fireButton") && cannonBall == null){
            FireCannonBall();
        }

        float distStrength = -1f * Input.GetAxis("AimingRightJoystick");
        if(distStrength < 0f){
            distStrength = 0f;
            SetForwardForce(distStrength);
        }
        else{
            SetForwardForce(distStrength);
        }
    }

    void FireCannonBall(){
        Vector3 spawnPosition = playerTransform.position + (playerTransform.right * -0.5f) + new Vector3(0, 0.2f, 0);
        // Vector3 spawnPosition = new Vector3(1, -10, 3);
        Quaternion spawnRotation = playerTransform.rotation;

        cannonBall = Instantiate(cannonBallPrefab, spawnPosition, spawnRotation);
        Rigidbody cb = cannonBall.GetComponent<Rigidbody>();

        

        Vector3 forwardDirection = -playerTransform.right;
        Vector3 launchDirection = forwardDirection + (Vector3.up * upwardForce);

        cb.AddForce(launchDirection * throwForce, ForceMode.Impulse);

        Destroy(cannonBall, 5f);
    }

    void SetForwardForce(float distStrength){
        
        upwardForce = distStrength * maxUpwardForce;
        throwForce = distStrength * maxThrowForce; 
    }
}
