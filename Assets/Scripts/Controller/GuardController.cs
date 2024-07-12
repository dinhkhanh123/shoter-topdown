using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GuardController : EnemyController
{

    [SerializeField] private float visionRange;
    [SerializeField] private float visionConeAngle;
    [SerializeField] private float turnSpeed;

    [SerializeField] public Light spotLight;
    private bool alerted;
   

    /* This is specifically guard behaviour - anything all enemies do will be handled by our parent, enemybehaviour */

    //Protected: we have to give it the same 'access' type as we did in the parent - just use protected 
    //Override: we know our parent has a version of this function, and we're deciding to over-ride it - use our version INSTEAD
    //base.Start() -- run our parent's version of this


    protected override void Start()
    {
        base.Start();
        alerted = false;
        GoToRamdomNavPoint();
    }

    void GoToRamdomNavPoint()
    {
        //When we give Random.Range float numbers, they can go all the way up to the max
        //But when we give it integers, it will never choose the max
        int ramdomValuePointIndex = Random.Range(0, Reference.navPoints.Count);
        navAgent.destination = Reference.navPoints[ramdomValuePointIndex].transform.position;
    }

    protected override void Update()
    {
 
        

        if (Reference.thePlayer != null)
        {
            Vector3 playerPosition = Reference.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position;
            spotLight.color = Color.white;
            if (alerted)
            {
                spotLight.color = Color.red;
                ChasePlayer();
            }
            else
            {

                if (navAgent.remainingDistance < 0.5f)
                {
                    GoToRamdomNavPoint();
                }


                //Rotate
                Vector3 lateralOffset = transform.right* Time.deltaTime * turnSpeed;
                transform.LookAt(transform.position + transform.forward + lateralOffset);
                //rb.velocity = transform.forward* speed;
                //Checking if we can see the player
                if (Vector3.Distance(transform.position,playerPosition) <= visionRange)
                {
                    if(Vector3.Angle(transform.forward,vectorToPlayer) <= visionConeAngle)
                    {
                        //Raycast(Starting point, direction, distance to check, layermask that only includes things we care about hitting)
                        //This returns true IF we hit something on that layer, when we shoot a laser in that direction for that distance
                        if (Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, Reference.wallLayer) == false) 
                            {
                            //First time we see the player
                            alerted = true;
                            Reference.levelManager.alarmSounded = true;
                        }
                       
                    }
                }
            }
           
        }
       
    }

   
}
