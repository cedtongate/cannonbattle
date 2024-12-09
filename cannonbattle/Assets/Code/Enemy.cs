using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float EnginePower = 0.5f;
    public float ApproachDistance = 3.0f;
    private float lastshot = 0.0f;
    private float ctime;
    public float shotInterval = 5.0f;
    AudioSource asource;
    public AudioClip shotnoise;

    private float currentHealth;
    private float maxHealth = 3f;

    public PlayerHealthBarScript enemyHealthBar;

    private Rigidbody rb;
    private GameObject player;
    public GameObject CannonPrefab;
    private Transform pt;
    private Vector3 OffsetToPlayer => pt.position - transform.position;
    private Vector3 HeadingToPlayer => OffsetToPlayer.normalized;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        asource = GetComponent<AudioSource>();
        player = GameObject.Find("Fisher_Boat");
        pt = player.transform;
        rb.maxLinearVelocity = 2;
        currentHealth = maxHealth;
        enemyHealthBar.updateHealthBar(maxHealth, currentHealth);
    }

    private void FireCannon()
    {
        var enemyCannonTransform = transform.position;
        enemyCannonTransform = new Vector3(enemyCannonTransform.x,enemyCannonTransform.y + 0.5f,enemyCannonTransform.z);
        GameObject enemyShot = Instantiate(CannonPrefab, enemyCannonTransform, Quaternion.identity);
        var crb = enemyShot.GetComponent<Rigidbody>();
        crb.velocity = new Vector3(HeadingToPlayer.x,1.0f,HeadingToPlayer.z) * 3.5f;
        crb.mass = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ctime = Time.time;
        if ((ctime - lastshot) > shotInterval)
        {
            asource.PlayOneShot(shotnoise, 1.0f);
            FireCannon();
            lastshot = ctime;
        }       
        //rb.transform.position = new Vector3(transform.position.x+0.0001f,transform.position.y,transform.position.z);
    }

    void FixedUpdate()
    {
        var offsetToPlayer = OffsetToPlayer;
        var distanceToPlayer = offsetToPlayer.magnitude;
        var controlSign = distanceToPlayer > ApproachDistance ? 1 : -1;
        rb.AddForce(controlSign * (EnginePower / distanceToPlayer) * offsetToPlayer);
        transform.position = new Vector3(transform.position.x,-1.7297f,transform.position.z);
        //var vnorm = rb.velocity.normalized;
        if(rb.velocity.x != 0f || rb.velocity.z != 0f)
        {
            var rad = Mathf.Atan(rb.velocity.x / rb.velocity.z);
            var angle = (rad * Mathf.Rad2Deg) + 90;
            //ROTATE FUNCTION
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerCannon")){
            currentHealth = currentHealth - 1f;
            enemyHealthBar.updateHealthBar(maxHealth, currentHealth);
            if(currentHealth == 0f){
                // SceneSwitch();
                Invoke("SceneSwitch", 1f);
            }
        }
    }
    private void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("WaterPlane")){
            Physics.IgnoreCollision(col.collider, GetComponent<MeshCollider>());
        }
    }

     public void SceneSwitch()
    {
        SceneManager.LoadScene("endWinMenu");
    }
}
