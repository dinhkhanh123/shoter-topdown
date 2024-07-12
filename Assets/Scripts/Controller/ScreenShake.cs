using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Vector3 normalPosion;
    Vector3 desiredPosion;

    public Vector3 joltVector;
    public float shakeAmount;


    public float joltDecayFactor;
    public float shakeDecayFactor;

    public float maxMoveSpeed;

    private void Awake()
    {
        Reference.screenShake = this;
    }

    void Start()
    {
        normalPosion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 shakeVector = new Vector3(GetRandomShakeAmount(), GetRandomShakeAmount(), GetRandomShakeAmount());
        desiredPosion = normalPosion + joltVector +shakeVector;
        //set our posion to the jolted position
        transform.position = Vector3.MoveTowards(transform.position, desiredPosion, maxMoveSpeed*Time.deltaTime);

        //joltVector decreases
        joltVector *= joltDecayFactor;
        shakeVector *= shakeDecayFactor;
    }

    float GetRandomShakeAmount()
    {
        return Random.Range(-shakeAmount, shakeAmount);
    }
}
