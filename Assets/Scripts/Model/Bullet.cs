using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _forceSpeed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _secondsUntilDestroy;
    [SerializeField] public float damage;

     private HealthSystem _healthSystem;
   
    void Start()
    { 
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _forceSpeed;
        _healthSystem = GetComponent<HealthSystem>();   

    }

    // Update is called once per frame
   protected virtual void Update()
    {
       _secondsUntilDestroy -= Time.deltaTime;

        if(_secondsUntilDestroy < 0)
        {
            transform.localScale *= _secondsUntilDestroy;
        }
        if( _secondsUntilDestroy < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision otherCollision)
    {
        GameObject therGameObject = otherCollision.gameObject;


        if(therGameObject.GetComponent<EnemyController>() != null)
        {
            _healthSystem = therGameObject.GetComponent<HealthSystem>();
            if (_healthSystem != null)
            {
                _healthSystem.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }

        
      
        
    }
}
