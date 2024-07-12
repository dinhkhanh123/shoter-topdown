using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        Reference.navPoints.Add(this);
    }

    private void OnDisable()
    {
        Reference.navPoints.Remove(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
