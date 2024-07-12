using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float damageExplosion;
    [SerializeField] private float secondsToExit;
    [SerializeField] private GameObject soundObject;
    
    float secondsWeveBeenAlive;

     void Start()
    {
        secondsWeveBeenAlive = 0;
        Instantiate(soundObject,transform.position,transform.rotation); 
    }

     void FixedUpdate()
    {
        secondsWeveBeenAlive += Time.fixedDeltaTime;

        float lifeFraction = secondsWeveBeenAlive / secondsToExit;
        Vector3 maxScale = Vector3.one * 5f;

        transform.localScale = Vector3.Lerp(Vector3.zero,maxScale, lifeFraction);

        if(secondsWeveBeenAlive >= secondsToExit)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
        HealthSystem therHealthSystem= other.gameObject.GetComponent<HealthSystem>();   

        if(therHealthSystem != null)
        {
            therHealthSystem.TakeDamage(damageExplosion);
        }
    }

   
}
