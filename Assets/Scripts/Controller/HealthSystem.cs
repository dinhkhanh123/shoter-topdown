using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs("health")]//write this to tell Unity not to lose our data when we rename a variable. This was its old name
    [SerializeField] private float maxHealth;
     private float currentHealth;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject deathEffectPrefab;

    HealthBar myHealthBar;
     void Start()
    {
        currentHealth = maxHealth;


       //Create health bar on the canvas Reference.canvas
      GameObject healthBarObject = Instantiate(healthBarPrefab,Reference.canvas.transform);
     myHealthBar = healthBarObject.GetComponent<HealthBar>();   
    }

    public void TakeDamage(float damageAmount)
    {

       if(currentHealth > 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                //This is where we die
                if (deathEffectPrefab != null)
                {
                    Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        //Don't create anything in the ondestroy event - it's only for cleaning up after yourself
       if (myHealthBar != null)
        {
            Destroy(myHealthBar.gameObject);
        }
    }

    private void Update()
    {
        //make health bar reflect our health
        myHealthBar.ShowHealthFraction(currentHealth/maxHealth);
        //Make health bar follow us - 
        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 20;
    }
}
