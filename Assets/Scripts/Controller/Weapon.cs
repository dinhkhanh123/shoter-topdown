using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float accuracy ;
    [SerializeField] private float _secondBetweenShots;
    [SerializeField] private float numberOfProjectiles;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float kickAmount;
    float secondSinceLateShot;
    void Start()
    {
        secondSinceLateShot = _secondBetweenShots;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        secondSinceLateShot += Time.deltaTime;
    }

   public void Shot(Vector3 targetPostion)
    {
        
        if (secondSinceLateShot >= _secondBetweenShots)
        {
            //Ready to fire
            Reference.levelManager.alarmSounded = true;
            audioSource.Play();
            Reference.screenShake.joltVector = transform.forward * -kickAmount ;

            for (
                int iterationCount = 0; //Declare a variable to keep track of how many iterations we've done
                iterationCount < numberOfProjectiles; //Set a limit for how high this variable can go
                iterationCount++ //Run this after each time we iterate - increase the iteration count
            )
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
                float inaccuracy = Vector3.Distance(transform.position, targetPostion) / accuracy;
                Vector3 inaccuratePosition = targetPostion;
                inaccuratePosition.x += Random.Range(-inaccuracy, inaccuracy);
                inaccuratePosition.z += Random.Range(-inaccuracy, inaccuracy);
                
                newBullet.transform.LookAt(inaccuratePosition);
                secondSinceLateShot = 0;
                //newBullet.name = iterationCount.ToString();
            }


            
        }
    }
}
