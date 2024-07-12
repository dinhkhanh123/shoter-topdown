using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunBeam : Bullet
{
    public LineRenderer myBeam;


    void Start()
    {
        //Step 1 - fire a laser to see how far we can go before we hit a wall
        //Raycast(Starting point, direction, define a new variable to store info about what happened when the ray hit something, distance to travel, layermask that only includes things we care about hitting)
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, Reference.maxDistanceInALevel, Reference.wallLayer );
        float distanceToWall = hitInfo.distance;

        //Step 2 - fire a new laser, only going that far, but checking for enemies this time
        float beamThickness = 0.3f;
        RaycastHit[] listOfHitInfo = Physics.SphereCastAll(transform.position, beamThickness, transform.forward, distanceToWall, Reference.enemiesLayer);
        foreach (RaycastHit enemyHitInfo in listOfHitInfo)
        {
            HealthSystem theirHealthSystem = enemyHitInfo.collider.GetComponentInParent<HealthSystem>();
            if (theirHealthSystem != null)
            {
                theirHealthSystem.TakeDamage(damage);
            }
        }


        //Step 3 - show the beam
        myBeam.SetPosition(0, transform.position);
        myBeam.SetPosition(1, hitInfo.point);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();//This will handle ticking down our life timer, and killing us 
        //Make our beam fade out over time
        myBeam.endColor = Color.Lerp(myBeam.endColor, Color.clear, 0.1f);
    }
}
