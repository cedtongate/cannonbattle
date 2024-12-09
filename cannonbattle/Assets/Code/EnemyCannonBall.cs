using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBall : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody enemyCannonBody;
    public GameObject EnemyBallPrefab;
    void Start()
    {
        enemyCannonBody = GetComponent<Rigidbody>();
        enemyCannonBody.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyCannonBall(){
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider col){
        if(col.gameObject.CompareTag("PlayerBoat")){
            MeshRenderer cannonBallMesh = GetComponent<MeshRenderer>();
            cannonBallMesh.enabled = false;
            Instantiate(EnemyBallPrefab, transform.position, Quaternion.identity, transform.parent);
            Invoke("DestroyCannonBall", 0.1f);
        }
        // if(col.gameObject.CompareTag("WaterPlane") || col.gameObject.CompareTag("Walls")){
        //     DestroyCannonBall();
        // }
        
    }
    private void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("WaterPlane") || col.gameObject.CompareTag("Walls")){
            DestroyCannonBall();
        }
    }
}
