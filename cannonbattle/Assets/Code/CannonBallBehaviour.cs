using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    
    private Rigidbody cannonBody;
    public GameObject PlayerCannonBallExplosion;
 
    void Start()
    {
        cannonBody = GetComponent<Rigidbody>();
        cannonBody.useGravity = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyCannonBall(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col){
        if(col.gameObject.CompareTag("Enemy")){
            MeshRenderer cannonBallMesh = GetComponent<MeshRenderer>();
            cannonBallMesh.enabled = false;
            Instantiate(PlayerCannonBallExplosion, transform.position, Quaternion.identity, transform.parent);
            Invoke("DestroyCannonBall", 0.1f);
        }
    
    }
    private void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("WaterPlane") || col.gameObject.CompareTag("Walls")){
            DestroyCannonBall();
        }
    }
}
