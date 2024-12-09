using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFireSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip cannonSound;
    public GameObject cannonBallPrefab;

    private float maxHealth = 3f;
    private float currentHealth;
    public PlayerHealthBarScript playerHealthbar;
    private Transform playerTransform;
    private float maxUpwardForce = 2f;
    private float maxThrowForce = 3f;

    private float upwardForce;
    private float throwForce;
    private GameObject cannonBall;

    void Start()
    {
        playerTransform = gameObject.transform;
        currentHealth = maxHealth;
        playerHealthbar.updateHealthBar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("fireButton") && cannonBall == null){
            FireCannonBall();
        }

        float distStrength = -1f * Input.GetAxis("AimingRightJoystick");
        // Debug.Log("Stick Position " + distStrength);
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
        Quaternion spawnRotation = playerTransform.rotation;

        cannonBall = Instantiate(cannonBallPrefab, spawnPosition, spawnRotation);
        Rigidbody cb = cannonBall.GetComponent<Rigidbody>();

        Vector3 forwardDirection = -playerTransform.right;
        Vector3 launchDirection = forwardDirection + (Vector3.up * upwardForce);

        cb.AddForce(launchDirection * throwForce, ForceMode.Impulse);
        AudioSource.PlayClipAtPoint(cannonSound, transform.position);

        Destroy(cannonBall, 3f);
    }

    void SetForwardForce(float distStrength){
        
        upwardForce = distStrength * maxUpwardForce;
        throwForce = distStrength * maxThrowForce; 
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.CompareTag("EnemyCannon")){
            currentHealth = currentHealth - 1f;
            playerHealthbar.updateHealthBar(maxHealth, currentHealth);
            if(currentHealth == 0f){
                // SceneSwitch();
                Invoke("SceneSwitch", 1f);
            }
        }
    }
     public void SceneSwitch()
    {
        SceneManager.LoadScene("endLoseMenu");
    }
}
