using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public NavMeshAgent navAgent;

    [SerializeField] public Rigidbody rb; 
    [SerializeField] public float speed;
    private HealthSystem healthSystem;


    //private float secondBetweenSpawn;
    //private float secondLastSpawn = 0;

    protected void OnEnable()
    {
        Reference.allEnemies.Add(this);
    }

    protected void OnDisable()
    {
        Reference.allEnemies.Remove(this);

    }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        //secondLastSpawn += Time.deltaTime;
        //if(secondLastSpawn >= secondBetweenSpawn)
        //{
        //    secondLastSpawn = 0;
        //    Instantiate(gameObject);
        //}

        ChasePlayer();

    }

    protected void ChasePlayer()
    {
    if (Reference.thePlayer != null)
    {
          navAgent.destination = Reference.thePlayer.transform.position;
            
       /* Vector3 playerPosition = Reference.thePlayer.transform.position;
        Vector3 vectorToPlayer = playerPosition - transform.position;
        rb.velocity = vectorToPlayer.normalized * speed;
        Vector3 playerPositionAtOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
        transform.LookAt(playerPositionAtOurHeight);
         */   
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        GameObject therGameObject = collision.gameObject;
        if(therGameObject.GetComponent<PlayerController>()!= null)
        {
            healthSystem = therGameObject.GetComponent<HealthSystem>();
            if(healthSystem != null)
            {
                healthSystem.TakeDamage(1);
            }
        }
    }
}
