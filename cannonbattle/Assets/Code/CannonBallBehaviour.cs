using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody cannonBody;
    
    void Start()
    {
        cannonBody = GetComponent<Rigidbody>();
        cannonBody.useGravity = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision){
        Destroy(gameObject);
    }
}
